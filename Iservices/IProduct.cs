using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoWebApiCore.Entity;

namespace MongoWebApiCore.Iservices
{
    public interface IProduct
    {
       Task<IEnumerable<Product>> GetAsync();
       Task CreateAsync(Product product);
        Task<long> DeleteAllAsync();
       Task<Product> GetbyIdAsync(Guid id);

    }
}