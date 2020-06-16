using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazedDataContext.MongoDB
{
    public class MongoRepositoryBase<T> : IRepository<T>
    {
        public IMongoDatabase MongoDatabase;
        public void SetMongoDatabase(IMongoDatabase MongoDatabase)
        {
            this.MongoDatabase = MongoDatabase;
        }
        public virtual IList<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public virtual Task<IList<T>> FindAllAsync()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T FindOne(object id)
        {
            var collection = MongoDatabase.GetCollection<T>(nameof(T));
            var filter = new FilterDefinitionBuilder<T>().Eq("Id", id);
            var entity = collection.Find(filter).FirstOrDefault();
            return entity;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> FindOneAsync(object id)
        {
            var collection = MongoDatabase.GetCollection<T>(nameof(T));
            var filter = new FilterDefinitionBuilder<T>().Eq("Id", id);
            var entity =await collection.FindAsync(filter).ConfigureAwait(false);
            return entity.FirstOrDefault();
        }

        public int InsertMany(IList<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertManyAsync(IList<T> entities)
        {
            throw new NotImplementedException();
        }

        public int InsertOne(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertOneAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public int LogicalDeleteMany(IList<object> ids)
        {
            throw new NotImplementedException();
        }

        public Task<int> LogicalDeleteManyAsync(IList<object> ids)
        {
            throw new NotImplementedException();
        }

        public int LogicalDeleteOne(object id)
        {
            throw new NotImplementedException();
        }

        public Task<int> LogicalDeleteOneAsync(object id)
        {
            throw new NotImplementedException();
        }

        public int PhysicalDeleteMany(IList<object> ids)
        {
            throw new NotImplementedException();
        }

        public Task<int> PhysicalDeleteManyAsync(IList<object> ids)
        {
            throw new NotImplementedException();
        }

        public int PhysicalDeleteOne(object id)
        {
            throw new NotImplementedException();
        }

        public Task<int> PhysicalDeleteOneAsync(object id)
        {
            throw new NotImplementedException();
        }

        public int UpdateMany(IList<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateManyAsync(IList<T> entities)
        {
            throw new NotImplementedException();
        }

        public int UpdateOne(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateOneAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
