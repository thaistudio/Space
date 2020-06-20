using MongoDB.Driver;
using SpaceServices.Data;
using SpaceServices.Data.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpaceServices.Links
{
    public class LinkStore
    {
        private readonly IMongoCollection<Link> links;

        public LinkStore(IMyDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            links = database.GetCollection<Link>(settings.LinksCollectionName);
        }

        public async Task<List<Link>> GetAsync() => await links.Find(link => true).ToListAsync();

        public async Task<Link> GetAsync(string id) => await links.Find<Link>(link => link.Id == id).FirstOrDefaultAsync();

        public async Task<Link> CreateAsync(Link link)
        {
            await links.InsertOneAsync(link);
            return link;
        }

        public async Task<List<Link>> GetAsync(List<string> linkIds)
        {
            var links = new List<Link>();
            foreach (var id in linkIds)
            {
                var link = await GetAsync(id);
                links.Add(link);
            }

            return links;
        }
    }
}
