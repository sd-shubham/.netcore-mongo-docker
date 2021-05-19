using MongoDB.Driver;

namespace MongoWebApiCore.MongoDbRepository
{
    public class MongoDBContext
    {
        private const string dataBaseName = "Catalog";
        private readonly IMongoClient _mongoClient;
        public MongoDBContext(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }
        public IMongoDatabase DataBaseName()
        {
            return _mongoClient.GetDatabase(dataBaseName);
        }
    }
}
