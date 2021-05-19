using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoWebApiCore.Entity;
using MongoWebApiCore.IMongoDbRepo;
using MongoWebApiCore.Iservices;

namespace MongoWebApiCore.Services
{
    public class ProductService : IProduct
    {
        private readonly IMongoDbrepository<Product> _mongoDbrepository;
        public ProductService(IMongoDbrepository<Product> mongoDbrepository)
        {
            _mongoDbrepository = mongoDbrepository;
        }
        public async Task CreateAsync(Product product)
        {
           await  _mongoDbrepository.Collections().InsertOneAsync(product);
        }

        public async Task<IEnumerable<Product>> GetAsync()
        {
            return await _mongoDbrepository.Collections().AsQueryable().ToListAsync();
        }
        public async Task<long> DeleteAllAsync()
        {
            var result = await _mongoDbrepository.Collections().DeleteManyAsync(new BsonDocument());
            return result.DeletedCount;
        }
        public async Task<Product> GetbyIdAsync(Guid id)
        {
            var filter = _mongoDbrepository.ApplyFilter().Eq(item => item.Id, id);
            var collection = await _mongoDbrepository.Collections().FindAsync(filter);
            return await collection.FirstOrDefaultAsync();
        }
    }
}