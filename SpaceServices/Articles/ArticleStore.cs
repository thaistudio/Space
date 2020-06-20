using MongoDB.Driver;
using SpaceServices.Data;
using SpaceServices.Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpaceServices.Articles
{
    public class ArticleStore
    {
        private readonly IMongoCollection<Article> articles;

        public ArticleStore(IMyDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            articles = database.GetCollection<Article>(settings.ArticlesCollectionName);
        }

        public async Task<List<Article>> GetAsync(Pagination pagination = null)
        {
            if (pagination is null)
                return await articles.Find(article => true).ToListAsync();

            var skip = (pagination.PageNumber - 1) * pagination.PageSize; 
            return await articles.Find(article => true).Skip(skip).Limit(pagination.PageSize).ToListAsync();
        }

        public async Task<Article> GetAsync(string id) => await articles.Find<Article>(article => article.Id == id).FirstOrDefaultAsync();

        public async Task<Article> CreateAsync(Article article)
        {
            await articles.InsertOneAsync(article);
            return article;
        }

        public async Task<int> CountAsync()
        {
            var count = await articles.Find(article => true).CountDocumentsAsync();
            return Convert.ToInt32(count);
        }
    }
}
