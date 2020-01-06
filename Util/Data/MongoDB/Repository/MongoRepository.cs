using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Util.Data.MongoDB.Interface;
using Util.Data.UOW.MongoDBUOW;

namespace Util.Data.MongoDB.Repository
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        public UnitOfWork.UOW UOW { get;private set; }
        /// <summary>
        /// 构造函数，初始化上下文
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="databaseName"></param>
        public MongoRepository(string connectionString, string databaseName)
        {
            UOW = new UnitOfWork.UOW(connectionString, databaseName);
        }

        /// <summary>
        /// 获取mongodb的集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns></returns>
        public IMongoCollection<T> GetMongoCollection<T>()
        {
            return UOW.Database.GetCollection<T>(typeof(T).Name);
        }
        #region 统计
        /// <summary>
        /// 异步统计
        /// </summary>
        /// <param name="condition">条件表达式</param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> condition)
        {
            var result = GetMongoCollection<TEntity>().AsQueryable().Count(condition);
            return result;
        }
        /// <summary>
        /// 异步统计
        /// </summary>
        /// <param name="condition">条件表达式</param>
        /// <returns></returns>
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> condition)
        {
            var result = await GetMongoCollection<TEntity>().AsQueryable().CountAsync(condition).ConfigureAwait(true);
            return result;
        }
        #endregion
        #region 删除
        /// <summary>
        /// 删除一条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int Delete(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
            var result = GetMongoCollection<TEntity>().DeleteOne(filter);
            return (int)result.DeletedCount;
        }
        /// <summary>
        /// 删除多条实体
        /// </summary>
        /// <param name="entities">实体</param>
        /// <returns></returns>
        public int Delete(IList<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            var filters = new List<FilterDefinition<TEntity>>();
            foreach (var entity in entities)
            {
                filters.Add(Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity)));
            }
            var result = GetMongoCollection<TEntity>().DeleteMany(Builders<TEntity>.Filter.Or(filters));
            return (int)result.DeletedCount;
        }
        /// <summary>
        /// 异步删除一条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
            var result = await GetMongoCollection<TEntity>().DeleteOneAsync(filter).ConfigureAwait(true);
            return (int)result.DeletedCount;
        }
        /// <summary>
        /// 异步删除多条实体
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(IList<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            var filters = new List<FilterDefinition<TEntity>>();
            foreach (var entity in entities)
            {
                filters.Add(Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity)));
            }
            var result = await GetMongoCollection<TEntity>().DeleteManyAsync(Builders<TEntity>.Filter.Or(filters)).ConfigureAwait(true);
            return (int)result.DeletedCount;
        }
        #endregion
        #region 查询
        ///// <summary>
        ///// 查询，多主键有待验证
        ///// </summary>
        ///// <param name="primaryKey">主键</param>
        ///// <returns></returns>
        public TEntity Find(params object[] primaryKey)
        {
            var filters = new List<FilterDefinition<TEntity>>();
            foreach (var key in primaryKey)
            {
                var filter = Builders<TEntity>.Filter
                    .Eq("Id", key);
                filters.Add(filter);
            }
            return GetMongoCollection<TEntity>().Find(Builders<TEntity>.Filter.And(filters)).Single();
        }
        /// <summary>
        /// 查询多条实体
        /// </summary>
        /// <param name="condition">条件表达式条件</param>
        /// <param name="keySelector">排序关键字（根据某个关键字排序）</param>
        /// <param name="sortMode">顺序/倒序（默认顺序）</param>
        /// <param name="pageIndex">索引页数</param>
        /// <param name="pageSize">索引页大小</param>
        /// <returns></returns>
        public IList<TEntity> Find(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, object>> keySelector, EnumSequence sortMode = EnumSequence.Ascending, int pageIndex = 1, int pageSize = 10)
        {
            var query = GetMongoCollection<TEntity>().AsQueryable()
                 .Where(condition)
                 .Skip((pageIndex - 1) * pageSize)
                 .Take(pageSize);
            switch (sortMode)
            {
                case EnumSequence.Ascending:
                    return query.OrderBy(keySelector).ToList();
                case EnumSequence.Descending:
                    return query.OrderByDescending(keySelector).ToList();
                default:
                    return null;
            };
        }
        ///// <summary>
        ///// 异步查询，多主键有待验证
        ///// </summary>
        ///// <param name="primaryKey">主键</param>
        public async Task<TEntity> FindAsync(params object[] primaryKey)
        {
            var filters = new List<FilterDefinition<TEntity>>();
            foreach (var key in primaryKey)
            {
                var filter = Builders<TEntity>.Filter
                    .Eq("Id", key);
                filters.Add(filter);
            }
            return (await GetMongoCollection<TEntity>().FindAsync(Builders<TEntity>.Filter.And(filters)).ConfigureAwait(true)).Single();
        }
        /// <summary>
        /// 异步查询多条实体
        /// </summary>
        /// <param name="condition">条件表达式</param>
        /// <param name="keySelector">排序关键字（根据某个关键字排序）</param>
        /// <param name="sortMode">排序方式，默认顺序</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        public async Task<IList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, object>> keySelector, EnumSequence sortMode = EnumSequence.Ascending, int pageIndex = 1, int pageSize = 10)
        {
            var query = GetMongoCollection<TEntity>().AsQueryable()
                 .Where(condition)
                 .Skip((pageIndex - 1) * pageSize)
                 .Take(pageSize);
            switch (sortMode)
            {
                case EnumSequence.Ascending:
                    return await query.OrderBy(keySelector).ToListAsync().ConfigureAwait(true);
                case EnumSequence.Descending:
                    return await query.OrderByDescending(keySelector).ToListAsync().ConfigureAwait(true);
                default:
                    return null;
            };
        }
        /// <summary>
        /// 查询所有的实体
        /// </summary>
        /// <param name="condition">条件表达式</param>
        /// <param name="keySelector">排序关键字（根据某个关键字排序）</param>
        /// <param name="sortMode">排序方式，默认顺序</param>
        /// <returns></returns>
        public IList<TEntity> FindAll(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, object>> keySelector, EnumSequence sortMode = EnumSequence.Ascending)
        {
            var quesy = GetMongoCollection<TEntity>().AsQueryable().Where(condition);
            switch (sortMode)
            {
                case EnumSequence.Ascending:
                    return quesy.OrderBy(keySelector).ToList();
                case EnumSequence.Descending:
                    return quesy.OrderByDescending(keySelector).ToList();
                default:
                    return null;
            }
        }
        /// <summary>
        /// 异步查询所有的实体
        /// </summary>
        /// <param name="condition">条件表达式</param>
        /// <param name="keySelector">排序关键字（根据某个关键字排序）</param>
        /// <param name="sortMode">排序方式，默认顺序</param>
        /// <returns></returns>
        public async Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, object>> keySelector, EnumSequence sortMode = EnumSequence.Ascending)
        {
            var quesy = GetMongoCollection<TEntity>().AsQueryable().Where(condition);
            switch (sortMode)
            {
                case EnumSequence.Ascending:
                    return await quesy.OrderBy(keySelector).ToListAsync().ConfigureAwait(true);
                case EnumSequence.Descending:
                    return await quesy.OrderByDescending(keySelector).ToListAsync().ConfigureAwait(true);
                default:
                    return null;
            }
        }
        #endregion
        #region 插入
        /// <summary>
        /// 插入一条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int Insert(TEntity entity)
        {
            GetMongoCollection<TEntity>().InsertOne(entity);
            return 1;
        }
        /// <summary>
        /// 插入多条实体
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public int Insert(IList<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            GetMongoCollection<TEntity>().InsertMany(entities);
            return entities.Count;
        }
        /// <summary>
        /// 异步插入一条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> InsertAsync(TEntity entity)
        {
            await GetMongoCollection<TEntity>().InsertOneAsync(entity).ConfigureAwait(true);
            return 1;
        }
        /// <summary>
        /// 异步插入多条实体
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public async Task<int> InsertAsync(IList<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            await GetMongoCollection<TEntity>().InsertManyAsync(entities).ConfigureAwait(true);
            return entities.Count;
        }
        #endregion
        #region 标识删除
        /// <summary>
        /// 修改一条实体的删除标识，改为true
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int MarkDelete(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
            var update = Builders<TEntity>.Update
                .Set("IsDeleted", true)
                .Set("DeleteTime", DateTime.Now);
            return (int)GetMongoCollection<TEntity>().UpdateOne(filter, update).ModifiedCount;
        }
        /// <summary>
        /// 修改多条实体的删除标识，改为true
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public int MarkDelete(IList<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            var update = Builders<TEntity>.Update
                .Set("IsDeleted", true)
                .Set("DeleteTime", DateTime.Now);
            int count = 0;
            foreach (var entity in entities)
            {
                var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
                count += (int)GetMongoCollection<TEntity>().UpdateOne(filter, update).ModifiedCount;
            }
            return count;
        }
        /// <summary>
        /// 异步修改一条实体的删除标识，改为true
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> MarkDeleteAsync(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
            var update = Builders<TEntity>.Update
                .Set("IsDeleted", true)
                .Set("DeleteTime", DateTime.Now);
            var result = await GetMongoCollection<TEntity>().UpdateOneAsync(filter, update).ConfigureAwait(true);
            return (int)result.ModifiedCount;
        }
        /// <summary>
        /// 异步修改多条实体的删除标识，改为true
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public async Task<int> MarkDeleteAsync(IList<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            var update = Builders<TEntity>.Update
                .Set("IsDeleted", true)
                .Set("DeleteTime", DateTime.Now);
            int count = 0;
            foreach (var entity in entities)
            {
                var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
                count += (int)(await GetMongoCollection<TEntity>().UpdateOneAsync(filter, update).ConfigureAwait(true)).ModifiedCount;
            }
            return count;
        }
        #endregion
        #region 标识未删除
        /// <summary>
        /// 修改一条实体的删除标识，改为false
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int UnmarkDelete(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
            var update = Builders<TEntity>.Update
                .Set("IsDeleted", false)
                .Set<TEntity, DateTime?>("DeleteTime", null);
            return (int)GetMongoCollection<TEntity>().UpdateOne(filter, update).ModifiedCount;
        }
        /// <summary>
        /// 修改多条实体的删除标识，改为false
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public int UnmarkDelete(IList<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            var update = Builders<TEntity>.Update
                .Set("IsDeleted", false)
                .Set<TEntity, DateTime?>("DeleteTime", null);
            int count = 0;
            foreach (var entity in entities)
            {
                var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
                count += (int)GetMongoCollection<TEntity>().UpdateOne(filter, update).ModifiedCount;
            }
            return count;
        }
        /// <summary>
        /// 异步修改一条实体的删除标识，改为false
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> UnmarkDeleteAsync(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
            var update = Builders<TEntity>.Update
                .Set("IsDeleted", false)
                .Set<TEntity, DateTime?>("DeleteTime", null);
            return (int)(await GetMongoCollection<TEntity>().UpdateOneAsync(filter, update).ConfigureAwait(true)).ModifiedCount;
        }
        /// <summary>
        /// 异步修改多条实体的删除标识，改为false
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public async Task<int> UnmarkDeleteAsync(IList<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            var update = Builders<TEntity>.Update
                .Set("IsDeleted", false)
                .Set<TEntity, DateTime?>("DeleteTime", null);
            int count = 0;
            foreach (var entity in entities)
            {
                var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
                count += (int)(await GetMongoCollection<TEntity>().UpdateOneAsync(filter, update).ConfigureAwait(true)).ModifiedCount;
            }
            return count;
        }
        #endregion
        #region 更新
        /// <summary>
        /// 更新单条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int Update(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
            return (int)GetMongoCollection<TEntity>().ReplaceOne(filter, entity).ModifiedCount;
        }
        /// <summary>
        /// 更新多条实体
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public int Update(IList<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            int count = 0;
            foreach (var entity in entities)
            {
                var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
                count += (int)GetMongoCollection<TEntity>().ReplaceOne(filter, entity).ModifiedCount;
            }
            return count;
        }
        /// <summary>
        /// 异步更新单条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
            return (int)(await GetMongoCollection<TEntity>().ReplaceOneAsync(filter, entity).ConfigureAwait(true)).ModifiedCount;
        }
        /// <summary>
        /// 异步更新多条实体
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(IList<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            int count = 0;
            foreach (var entity in entities)
            {
                var filter = Builders<TEntity>.Filter.Eq("Id", typeof(TEntity).GetProperty("Id").GetValue(entity));
                count += (int)(await GetMongoCollection<TEntity>().ReplaceOneAsync(filter, entity).ConfigureAwait(true)).ModifiedCount;
            }
            return count;
        }
        #endregion
    }
}
