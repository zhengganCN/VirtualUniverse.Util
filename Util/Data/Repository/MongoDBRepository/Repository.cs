using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Util.Data.UOW.MongoDBUOW;

namespace Util.Data.Repository.MongoDBRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity:class,new()
    {
        private readonly DbContext context;
        public Repository(DbContext context)
        {
            this.context = context;
        }
        public virtual IMongoCollection<TEntity> GetMongoCollection<TEntity>(UnitOfWork uow)
        {
            if (uow == null)
            {
                throw new NullReferenceException();
            }
            var s= typeof(TEntity).Name;
            return uow.database.GetCollection<TEntity>(typeof(TEntity).Name);
        }
        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            var uow = new UnitOfWork(context);
            var result = GetMongoCollection<TEntity>(uow).AsQueryable().Count(predicate);
            return result;
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var uow = new UnitOfWork(context);
            var result =await GetMongoCollection<TEntity>(uow).AsQueryable().CountAsync(predicate);
            return result;
        }

        public virtual int Delete(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
            var result = GetMongoCollection<TEntity>(uow).DeleteOne(filter);
            return (int)result.DeletedCount;
        }

        public virtual int Delete(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            var filters = new List<FilterDefinition<TEntity>>();
            foreach (var entity in entities)
            {
                filters.Add(Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity)));
            }
            var result = GetMongoCollection<TEntity>(uow).DeleteMany(Builders<TEntity>.Filter.Or(filters));
            return (int)result.DeletedCount;
        }

        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
            var result =await GetMongoCollection<TEntity>(uow).DeleteOneAsync(filter);
            return (int)result.DeletedCount;
        }

        public virtual async Task<int> DeleteAsync(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            var filters = new List<FilterDefinition<TEntity>>();
            foreach (var entity in entities)
            {
                filters.Add(Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity)));
            }
            var result =await GetMongoCollection<TEntity>(uow).DeleteManyAsync(Builders<TEntity>.Filter.Or(filters));
            return (int)result.DeletedCount;
        }

        public virtual TEntity Find(params object[] primaryKey)
        {
            var uow = new UnitOfWork(context);
            var filters = new List<FilterDefinition<TEntity>>();
            foreach (var key in primaryKey)
            {
                var filter = Builders<TEntity>.Filter
                    .Eq("Id", key);
                filters.Add(filter);
            }
            return GetMongoCollection<TEntity>(uow).Find(Builders<TEntity>.Filter.And(filters)).Single();
        }

        public virtual IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> keySelector, bool asc = true, int pageIndex = 1, int pageSize = 10)
        {
            var uow = new UnitOfWork(context);
            var query = GetMongoCollection<TEntity>(uow).AsQueryable()
                 .Where(predicate)
                 .Skip((pageIndex - 1) * pageSize)
                 .Take(pageSize);
            return asc ? query.OrderBy(keySelector).ToList() : query.OrderByDescending(keySelector).ToList();
        }

        public virtual async Task<TEntity> FindAsync(params object[] primaryKey)
        {
            var uow = new UnitOfWork(context);
            var filters = new List<FilterDefinition<TEntity>>();
            foreach (var key in primaryKey)
            {
                var filter = Builders<TEntity>.Filter
                    .Eq("Id",key);
                filters.Add(filter);
            }
            return (await GetMongoCollection<TEntity>(uow).FindAsync(Builders<TEntity>.Filter.And(filters))).Single();
        }

        public virtual int Insert(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            GetMongoCollection<TEntity>(uow).InsertOne(entity);
            return 1;
        }

        public virtual int Insert(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            GetMongoCollection<TEntity>(uow).InsertMany(entities);
            return entities.Count;
        }

        public virtual async Task<int> InsertAsync(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            await GetMongoCollection<TEntity>(uow).InsertOneAsync(entity);
            return 1;
        }

        public virtual async Task<int> InsertAsync(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            await GetMongoCollection<TEntity>(uow).InsertManyAsync(entities);
            return entities.Count;
        }

        public virtual int MarkDelete(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
            var update = Builders<TEntity>.Update
                .AddToSet("IsDeleted", true)
                .AddToSet("DeleteTime", DateTime.Now);
            return (int)GetMongoCollection<TEntity>(uow).UpdateOne(filter, update).ModifiedCount;
        }

        public virtual int MarkDelete(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            var update = Builders<TEntity>.Update
                .AddToSet("IsDeleted", true)
                .AddToSet("DeleteTime", DateTime.Now);
            int count = 0;
            foreach (var entity in entities)
            {
                var filter= Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
                count+=(int)GetMongoCollection<TEntity>(uow).UpdateOne(filter, update).ModifiedCount;
            }
            return count;
        }

        public virtual async Task<int> MarkDeleteAsync(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
            var update = Builders<TEntity>.Update
                .AddToSet("IsDeleted", true)
                .AddToSet("DeleteTime", DateTime.Now);
            var result = await GetMongoCollection<TEntity>(uow).UpdateOneAsync(filter, update);
            return (int)result.ModifiedCount;
        }

        public virtual async Task<int> MarkDeleteAsync(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            var update = Builders<TEntity>.Update
                .AddToSet("IsDeleted", true)
                .AddToSet("DeleteTime", DateTime.Now);
            int count = 0;
            foreach (var entity in entities)
            {
                var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
                count += (int)(await GetMongoCollection<TEntity>(uow).UpdateOneAsync(filter, update)).ModifiedCount;
            }
            return count;
        }

        public virtual int UnmarkDelete(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
            var update = Builders<TEntity>.Update
                .AddToSet("IsDeleted", false)
                .AddToSet("DeleteTime", "");
            return (int)GetMongoCollection<TEntity>(uow).UpdateOne(filter, update).ModifiedCount;
        }

        public virtual int UnmarkDelete(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            var update = Builders<TEntity>.Update
                .AddToSet("IsDeleted", false)
                .AddToSet("DeleteTime", "");
            int count = 0;
            foreach (var entity in entities)
            {
                var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
                count += (int)GetMongoCollection<TEntity>(uow).UpdateOne(filter, update).ModifiedCount;
            }
            return count;
        }

        public virtual async Task<int> UnmarkDeleteAsync(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
            var update = Builders<TEntity>.Update
                .AddToSet("IsDeleted", false)
                .AddToSet("DeleteTime", "");
            return (int)(await GetMongoCollection<TEntity>(uow).UpdateOneAsync(filter, update)).ModifiedCount;
        }

        public virtual async Task<int> UnmarkDeleteAsync(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            var update = Builders<TEntity>.Update
                .AddToSet("IsDeleted", false)
                .AddToSet("DeleteTime", "");
            int count = 0;
            foreach (var entity in entities)
            {
                var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
                count += (int)(await GetMongoCollection<TEntity>(uow).UpdateOneAsync(filter, update)).ModifiedCount;
            }
            return count;
        }

        public virtual int Update(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
            return (int)GetMongoCollection<TEntity>(uow).ReplaceOne(filter, entity).ModifiedCount;
        }

        public virtual int Update(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            int count = 0;
            foreach (var entity in entities)
            {
                var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
                count += (int)GetMongoCollection<TEntity>(uow).ReplaceOne(filter, entity).ModifiedCount;
            }
            return count;
        }

        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
            return (int)(await GetMongoCollection<TEntity>(uow).ReplaceOneAsync(filter, entity)).ModifiedCount;
        }

        public virtual async Task<int> UpdateAsync(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            int count = 0;
            foreach (var entity in entities)
            {
                var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
                count += (int)(await GetMongoCollection<TEntity>(uow).ReplaceOneAsync(filter, entity)).ModifiedCount;
            }
            return count;
        }
    }
}
