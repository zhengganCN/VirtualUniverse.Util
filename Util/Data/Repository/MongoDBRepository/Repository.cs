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
    /// 仓储基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity:class,new()
    {
        private readonly DbContext context;
        /// <summary>
        /// 构造函数，初始化上下文
        /// </summary>
        /// <param name="context">mongodb上下文</param>
        public Repository(DbContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// 获取mongodb的集合
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        public virtual IMongoCollection<TEntity> GetMongoCollection<TEntity>(UnitOfWork uow)
        {
            if (uow == null)
            {
                throw new NullReferenceException();
            }
            return uow.database.GetCollection<TEntity>(typeof(TEntity).Name);
        }
        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            var uow = new UnitOfWork(context);
            var result = GetMongoCollection<TEntity>(uow).AsQueryable().Count(predicate);
            return result;
        }
        /// <summary>
        /// 异步统计
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var uow = new UnitOfWork(context);
            var result =await GetMongoCollection<TEntity>(uow).AsQueryable().CountAsync(predicate);
            return result;
        }
        /// <summary>
        /// 删除单条
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Delete(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
            var result = GetMongoCollection<TEntity>(uow).DeleteOne(filter);
            return (int)result.DeletedCount;
        }
        /// <summary>
        /// 删除多条
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 异步删除单条
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
            var result =await GetMongoCollection<TEntity>(uow).DeleteOneAsync(filter);
            return (int)result.DeletedCount;
        }
        /// <summary>
        /// 异步删除多条
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 查询，多主键有待验证
        /// </summary>
        /// <param name="primaryKey">主键</param>
        /// <returns></returns>
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
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="keySelector">排序关键字（根据某个关键字排序）</param>
        /// <param name="asc">顺序/倒序（默认顺序）</param>
        /// <param name="pageIndex">索引页数</param>
        /// <param name="pageSize">索引页大小</param>
        /// <returns></returns>
        public virtual IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> keySelector, bool asc = true, int pageIndex = 1, int pageSize = 10)
        {
            var uow = new UnitOfWork(context);
            var query = GetMongoCollection<TEntity>(uow).AsQueryable()
                 .Where(predicate)
                 .Skip((pageIndex - 1) * pageSize)
                 .Take(pageSize);
            return asc ? query.OrderBy(keySelector).ToList() : query.OrderByDescending(keySelector).ToList();
        }
        /// <summary>
        /// 异步查询，多主键有待验证
        /// </summary>
        /// <param name="primaryKey">主键</param>
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
        /// <summary>
        /// 插入单条
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public virtual int Insert(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            GetMongoCollection<TEntity>(uow).InsertOne(entity);
            return 1;
        }
        /// <summary>
        /// 插入多条
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        public virtual int Insert(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            GetMongoCollection<TEntity>(uow).InsertMany(entities);
            return entities.Count;
        }
        /// <summary>
        /// 异步插入单条
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public virtual async Task<int> InsertAsync(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            await GetMongoCollection<TEntity>(uow).InsertOneAsync(entity);
            return 1;
        }
        /// <summary>
        /// 异步插入多条
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        public virtual async Task<int> InsertAsync(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            await GetMongoCollection<TEntity>(uow).InsertManyAsync(entities);
            return entities.Count;
        }
        /// <summary>
        /// 设置单个实体的删除标记为true
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public virtual int MarkDelete(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
            var update = Builders<TEntity>.Update
                .AddToSet("IsDeleted", true)
                .AddToSet("DeleteTime", DateTime.Now);
            return (int)GetMongoCollection<TEntity>(uow).UpdateOne(filter, update).ModifiedCount;
        }
        /// <summary>
        /// 设置多个实体的删除标记为true
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
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
        /// <summary>
        /// 异步设置单个实体的删除标记为true
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
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
        /// <summary>
        /// 异步设置多个实体的删除标记为true
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
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
        /// <summary>
        /// 设置单个实体的删除标记为false
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public virtual int UnmarkDelete(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
            var update = Builders<TEntity>.Update
                .AddToSet("IsDeleted", false)
                .AddToSet("DeleteTime", "");
            return (int)GetMongoCollection<TEntity>(uow).UpdateOne(filter, update).ModifiedCount;
        }
        /// <summary>
        /// 设置多个实体的删除标记为false
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
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
        /// <summary>
        /// 异步设置单个实体的删除标记为false
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public virtual async Task<int> UnmarkDeleteAsync(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
            var update = Builders<TEntity>.Update
                .AddToSet("IsDeleted", false)
                .AddToSet("DeleteTime", "");
            return (int)(await GetMongoCollection<TEntity>(uow).UpdateOneAsync(filter, update)).ModifiedCount;
        }
        /// <summary>
        /// 异步设置多个实体的删除标记为false
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
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
        /// <summary>
        /// 更新单条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public virtual int Update(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
            return (int)GetMongoCollection<TEntity>(uow).ReplaceOne(filter, entity).ModifiedCount;
        }
        /// <summary>
        /// 更新多条实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
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
        /// <summary>
        /// 异步更新单条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
            return (int)(await GetMongoCollection<TEntity>(uow).ReplaceOneAsync(filter, entity)).ModifiedCount;
        }
        /// <summary>
        /// 异步更新多条实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
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
