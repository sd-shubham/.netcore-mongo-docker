using MongoDB.Driver;
using MongoWebApiCore.IMongoDbRepo;
using MongoWebApiCore.MongoDbRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoWebApiCore.MongoDbRepo
{
    public class MongoDbrepository<T>: IMongoDbrepository<T> where T:class
    {
        private readonly MongoDBContext _mongoDBContext;
        public MongoDbrepository(MongoDBContext mongoDBContext)
        {
            _mongoDBContext = mongoDBContext;
        }

        public IMongoCollection<T> Collections()
        {
            return _mongoDBContext.DataBaseName().GetCollection<T>($"{typeof(T).Name}s");
        }
        public FilterDefinitionBuilder<T> ApplyFilter()
        {
            return Builders<T>.Filter;
        }
    }
}
