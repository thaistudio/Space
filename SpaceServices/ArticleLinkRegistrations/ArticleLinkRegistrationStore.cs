using MongoDB.Driver;
using SpaceServices.Data;
using SpaceServices.Data.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceServices.ArticleLinkRegistrations
{
    public class ArticleLinkRegistrationStore
    {
        private readonly IMongoCollection<ArticleLinkRegistration> articleLinkRegistration;

        public ArticleLinkRegistrationStore(IMyDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            articleLinkRegistration = database.GetCollection<ArticleLinkRegistration>(settings.ArticleLinkRegistrationsCollectionName);
        }

        public async Task<List<ArticleLinkRegistration>> GetAsync() => await articleLinkRegistration.Find(r => true).ToListAsync();

        public async Task<ArticleLinkRegistration> GetByArticleAsync(string articleId) 
            => await articleLinkRegistration.Find(r => r.ArticleId == articleId).FirstOrDefaultAsync();

        public async Task<ArticleLinkRegistration> GetByLinkAsync(string linkId)
            => await articleLinkRegistration.Find<ArticleLinkRegistration>(r => r.LinkId == linkId).FirstOrDefaultAsync();

        public async Task<ArticleLinkRegistration> CreateAsync(ArticleLinkRegistration registration)
        {
            await articleLinkRegistration.InsertOneAsync(registration);
            return registration;
        }

        public async Task<Data.Domain.ArticleLinkRegistrations> CreateMultipleAsync(Data.Domain.ArticleLinkRegistrations registrations)
        {
            await articleLinkRegistration.InsertManyAsync(registrations.Registrations);
            return registrations;
        }

        public async Task<Data.Domain.ArticleLinkRegistrations> UpdateMultipleAsync(Data.Domain.ArticleLinkRegistrations registrations)
        {
            var articleIds = registrations.Registrations.Select(r => r.ArticleId).ToArray();
            var linkIds = registrations.Registrations.Select(r => r.LinkId).ToArray();
            await articleLinkRegistration.DeleteManyAsync(r => articleIds.Contains(r.ArticleId) && linkIds.Contains(r.LinkId));
            await articleLinkRegistration.InsertManyAsync(registrations.Registrations);

            return registrations;
        }

        public async Task<List<string>> GetLinkIdsByArticleAsync(string articleId)
        {
            var registrations = await articleLinkRegistration.Find(r => r.ArticleId == articleId).ToListAsync();
            var linkIds = registrations.Select(r => r.LinkId).ToList();
            return linkIds;
        }
    }
}
