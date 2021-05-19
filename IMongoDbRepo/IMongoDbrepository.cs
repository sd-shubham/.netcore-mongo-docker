using MongoDB.Driver;
namespace MongoWebApiCore.IMongoDbRepo
{
   public interface IMongoDbrepository<T> where T: class
    {
        IMongoCollection<T> Collections();
        FilterDefinitionBuilder<T> ApplyFilter();
    }
}
