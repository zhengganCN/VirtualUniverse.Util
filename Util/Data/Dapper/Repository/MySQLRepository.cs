using MySql.Data.MySqlClient;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Util.Data.Dapper.Interface;
using System.Linq;

namespace Util.Data.Dapper.Repository
{
    /// <summary>
    /// MySQL仓储模式
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public class MySQLRepository<TEntity> : IRepository<TEntity> where TEntity : class,IEntity
    {
        /// <summary>
        /// 工作单元
        /// </summary> 
        private readonly MySQLUOW _unitOfWork;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString"></param>
        public MySQLRepository(string connectionString)
        {
            _unitOfWork = new MySQLUOW
            {
                MySqlConnection = new MySqlConnection(connectionString)
            };
        }
        /// <summary>
        /// 获取MySQL连接
        /// </summary>
        /// <returns></returns>
        public MySqlConnection GetMySqlConnection()
        {
            return _unitOfWork.MySqlConnection;
        }
        /// <summary>
        /// 获取工作单元
        /// </summary>
        /// <returns></returns>
        public MySQLUOW GetUnitOfWork()
        {
            return _unitOfWork;
        }
        #region 插入操作
        /// <summary>
        /// 插入一条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int InsertOne(TEntity entity)
        {
            if (entity==null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = MySQLString.InsertSQLString(entity);
            return GetMySqlConnection().Execute(sql, entity);
        }
        /// <summary>
        /// 批量插入实体集
        /// </summary>
        /// <param name="entities">实体集</param>
        /// <returns></returns>
        public int InsertMany(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            if (!entities.Any())
            {
                throw new ArgumentException(StringResource._2DFFD5DF_292D_4E3F_9CA2_0B238B2258E7);
            }
            var entity = entities.First();
            var sql= MySQLString.InsertSQLString(entity);
            return GetMySqlConnection().Execute(sql, entities);
        }
        /// <summary>
        /// 异步插入一条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> InsertOneAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = MySQLString.InsertSQLString(entity);
            return await GetMySqlConnection().ExecuteAsync(sql, entity).ConfigureAwait(true);
        }
        /// <summary>
        /// 异步批量插入实体集
        /// </summary>
        /// <param name="entities">实体集</param>
        /// <returns></returns>
        public async Task<int> InsertManyAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            if (!entities.Any())
            {
                throw new ArgumentException(StringResource._2DFFD5DF_292D_4E3F_9CA2_0B238B2258E7);
            }
            var entity = entities.First();
            var sql = MySQLString.InsertSQLString(entity);
            return await GetMySqlConnection().ExecuteAsync(sql, entities).ConfigureAwait(true);
        }
        #endregion
        #region 更新操作
        /// <summary>
        /// 更新所有字段，根据实体的Id更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int UpdateOne(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = MySQLString.UpdateSQLString(entity);
            return GetMySqlConnection().Execute(sql, entity);
        }
        /// <summary>
        /// 批量更新所有字段，根据每条实体的Id更新每条实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        public int UpdateMany(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            if (!entities.Any())
            {
                throw new ArgumentException(StringResource._2DFFD5DF_292D_4E3F_9CA2_0B238B2258E7);
            }
            var sql = MySQLString.UpdateSQLString(entities.First());
            return GetMySqlConnection().Execute(sql, entities);
        }
        /// <summary>
        /// 批量更新指定字段，根据每条实体的Id更新每条实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <param name="updateFields">更新字段的匿名对象</param>
        /// <returns></returns>
        public int UpdateMany(IEnumerable<TEntity> entities, object updateFields)
        {
            if (entities == null || updateFields == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            if (!entities.Any())
            {
                throw new ArgumentException(StringResource._2DFFD5DF_292D_4E3F_9CA2_0B238B2258E7);
            }
            var sql = MySQLString.UpdateSQLString<TEntity>(updateFields);
            return GetMySqlConnection().Execute(sql, entities);
        }
        /// <summary>
        /// 批量更新符合条件的实体的指定字段
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="updateFields">更新字段的匿名对象</param>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public int UpdateMany(TEntity entity, object updateFields, string conditionString)
        {
            if (entity == null || updateFields == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = MySQLString.UpdateSQLString<TEntity>(updateFields, conditionString);
            return GetMySqlConnection().Execute(sql, entity);
        }
        /// <summary>
        /// 异步更新所有字段，根据实体的Id更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> UpdateOneAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = MySQLString.UpdateSQLString(entity);
            return await GetMySqlConnection().ExecuteAsync(sql, entity).ConfigureAwait(true);
        }
        /// <summary>
        /// 异步批量更新所有字段，根据每条实体的Id更新每条实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        public async Task<int> UpdateManyAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            if (!entities.Any())
            {
                throw new ArgumentException(StringResource._2DFFD5DF_292D_4E3F_9CA2_0B238B2258E7);
            }
            var sql = MySQLString.UpdateSQLString(entities.First());
            return await GetMySqlConnection().ExecuteAsync(sql, entities).ConfigureAwait(true);
        }
        /// <summary>
        /// 异步批量更新指定字段，根据每条实体的Id更新每条实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <param name="updateFields">更新字段的匿名对象</param>
        /// <returns></returns>
        public async Task<int> UpdateManyAsync(IEnumerable<TEntity> entities, object updateFields)
        {
            if (entities == null || updateFields == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            if (!entities.Any())
            {
                throw new ArgumentException(StringResource._2DFFD5DF_292D_4E3F_9CA2_0B238B2258E7);
            }
            var sql = MySQLString.UpdateSQLString<TEntity>(updateFields);
            return await GetMySqlConnection().ExecuteAsync(sql, entities).ConfigureAwait(true);
        }
        /// <summary>
        /// 异步批量更新符合条件的实体的指定字段
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="updateFields">更新字段的匿名对象</param>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public async Task<int> UpdateManyAsync(TEntity entity, object updateFields, string conditionString)
        {
            if (entity == null || updateFields == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = MySQLString.UpdateSQLString<TEntity>(updateFields, conditionString);
            return await GetMySqlConnection().ExecuteAsync(sql, entity).ConfigureAwait(true);
        }
        #endregion
        #region 查询操作
        /// <summary>
        /// 根据条件查询返回一条符合的数据
        /// </summary>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public TEntity FindOne(string conditionString)
        {
            var sql = MySQLString.QuerySQLString<TEntity>(conditionString);
            return GetMySqlConnection().QueryFirst<TEntity>(sql);
        }
        /// <summary>
        /// 查询所有符合条件的实体，并根据分页条件返回限定数量的数据
        /// </summary>
        /// <param name="conditionString">条件字符串</param>
        /// <param name="sequenceField">排序字段</param>
        /// <param name="sequence">排序方式</param>
        /// <param name="pageInde">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindMany(string conditionString, string sequenceField = "Id", EnumSequence sequence = EnumSequence.Ascending, int pageInde = 1, int pageSize = 10)
        {
            var sql = MySQLString.QuerySQLString<TEntity>(conditionString, sequenceField,
                sequence, pageInde, pageSize);
            return GetMySqlConnection().Query<TEntity>(sql);
        }
        /// <summary>
        /// 查询所有实体
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAll()
        {
            var sql = MySQLString.QuerySQLString<TEntity>();
            return GetMySqlConnection().Query<TEntity>(sql);
        }
        /// <summary>
        /// 查询所有符合条件的实体
        /// </summary>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAll(string conditionString)
        {
            var sql = MySQLString.QuerySQLString<TEntity>(conditionString);
            return GetMySqlConnection().Query<TEntity>(sql);
        }
        /// <summary>
        /// 根据条件异步查询返回一条符合的数据
        /// </summary>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public async Task<TEntity> FindOneAsync(string conditionString)
        {
            var sql = MySQLString.QuerySQLString<TEntity>(conditionString);
            return await GetMySqlConnection().QueryFirstAsync<TEntity>(sql).ConfigureAwait(true);
        }
        /// <summary>
        /// 异步查询所有符合条件的实体，并根据分页条件返回限定数量的数据
        /// </summary>
        /// <param name="conditionString">条件字符串</param>
        /// <param name="sequenceField">排序字段</param>
        /// <param name="sequence">排序方式</param>
        /// <param name="pageInde">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> FindManyAsync(string conditionString, string sequenceField = "Id", EnumSequence sequence = EnumSequence.Ascending, int pageInde = 1, int pageSize = 10)
        {
            var sql = MySQLString.QuerySQLString<TEntity>(conditionString, sequenceField,
                sequence, pageInde, pageSize);
            return await GetMySqlConnection().QueryAsync<TEntity>(sql).ConfigureAwait(true);
        }
        /// <summary>
        /// 异步查询所有实体
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            var sql = MySQLString.QuerySQLString<TEntity>();
            return await GetMySqlConnection().QueryAsync<TEntity>(sql).ConfigureAwait(true);
        }
        /// <summary>
        /// 异步查询所有符合条件的实体
        /// </summary>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> FindAllAsync(string conditionString)
        {
            var sql = MySQLString.QuerySQLString<TEntity>(conditionString);
            return await GetMySqlConnection().QueryAsync<TEntity>(sql).ConfigureAwait(true);
        }
        #endregion
        #region 删除操作
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int DeleteOne(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = MySQLString.DeleteSQLString<TEntity>();
            return GetMySqlConnection().Execute(sql, entity);
        }
        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        public int DeleteMany(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            if (!entities.Any())
            {
                throw new ArgumentException(StringResource._2DFFD5DF_292D_4E3F_9CA2_0B238B2258E7);
            }
            var sql = MySQLString.DeleteSQLString<TEntity>();
            return GetMySqlConnection().Execute(sql, entities);
        }
        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public int DeleteMany(string conditionString)
        {
            if (string.IsNullOrEmpty(conditionString))
            {
                throw new ArgumentException(StringResource._2DFFD5DF_292D_4E3F_9CA2_0B238B2258E7);
            }
            var sql = MySQLString.DeleteSQLString<TEntity>(conditionString);
            return GetMySqlConnection().Execute(sql);
        }
        /// <summary>
        /// 异步删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> DeleteOneAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = MySQLString.DeleteSQLString<TEntity>();
            return await GetMySqlConnection().ExecuteAsync(sql, entity).ConfigureAwait(true);
        }
        /// <summary>
        /// 异步批量删除实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        public async Task<int> DeleteManyAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            if (!entities.Any())
            {
                throw new ArgumentException(StringResource._2DFFD5DF_292D_4E3F_9CA2_0B238B2258E7);
            }
            var sql = MySQLString.DeleteSQLString<TEntity>();
            return await GetMySqlConnection().ExecuteAsync(sql, entities).ConfigureAwait(true);
        }
        /// <summary>
        /// 根据条件异步删除实体
        /// </summary>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public async Task<int> DeleteManyAsync(string conditionString)
        {
            if (string.IsNullOrEmpty(conditionString))
            {
                throw new ArgumentException(StringResource._2DFFD5DF_292D_4E3F_9CA2_0B238B2258E7);
            }
            var sql = MySQLString.DeleteSQLString<TEntity>(conditionString);
            return await GetMySqlConnection().ExecuteAsync(sql).ConfigureAwait(true);
        }
        #endregion
        #region 标记为删除
        /// <summary>
        /// 更新删除标记为已删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int MarkDeleteOne(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = MySQLString.MarkDeleteSQLString<TEntity>();
            return GetMySqlConnection().Execute(sql, entity);
        }
        /// <summary>
        /// 批量更新删除标记为已删除
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        public int MarkDeleteMany(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            var sql = MySQLString.MarkDeleteSQLString<TEntity>();
            return GetMySqlConnection().Execute(sql, entities);
        }
        /// <summary>
        /// 根据条件更新删除标记为已删除
        /// </summary>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public int MarkDeleteMany(string conditionString)
        {
            if (string.IsNullOrEmpty(conditionString))
            {
                throw new ArgumentException(StringResource._2DFFD5DF_292D_4E3F_9CA2_0B238B2258E7);
            }
            var sql = MySQLString.MarkDeleteSQLString<TEntity>(conditionString);
            return GetMySqlConnection().Execute(sql, conditionString);
        }
        /// <summary>
        /// 异步更新删除标记为已删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> MarkDeleteOneAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = MySQLString.MarkDeleteSQLString<TEntity>();
            return await GetMySqlConnection().ExecuteAsync(sql, entity).ConfigureAwait(true);
        }
        /// <summary>
        /// 异步批量更新删除标记为已删除
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        public async Task<int> MarkDeleteManyAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            var sql = MySQLString.MarkDeleteSQLString<TEntity>();
            return await GetMySqlConnection().ExecuteAsync(sql, entities).ConfigureAwait(true);
        }
        /// <summary>
        /// 根据条件异步更新删除标记为已删除
        /// </summary>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public async Task<int> MarkDeleteManyAsync(string conditionString)
        {
            if (string.IsNullOrEmpty(conditionString))
            {
                throw new ArgumentException(StringResource._2DFFD5DF_292D_4E3F_9CA2_0B238B2258E7);
            }
            var sql = MySQLString.MarkDeleteSQLString<TEntity>(conditionString);
            return await GetMySqlConnection().ExecuteAsync(sql, conditionString).ConfigureAwait(true);
        }
        #endregion
        #region 标记为未删除
        /// <summary>
        /// 更新删除标记为未删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int MarkUnDeleteOne(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = MySQLString.MarkUnDeleteSQLString<TEntity>();
            return GetMySqlConnection().Execute(sql, entity);
        }
        /// <summary>
        /// 批量更新删除标记为未删除
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        public int MarkUnDeleteMany(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            var sql = MySQLString.MarkUnDeleteSQLString<TEntity>();
            return GetMySqlConnection().Execute(sql, entities);
        }
        /// <summary>
        /// 根据条件更新删除标记为未删除
        /// </summary>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public int MarkUnDeleteMany(string conditionString)
        {
            if (string.IsNullOrEmpty(conditionString))
            {
                throw new ArgumentException(StringResource._2DFFD5DF_292D_4E3F_9CA2_0B238B2258E7);
            }
            var sql = MySQLString.MarkUnDeleteSQLString<TEntity>(conditionString);
            return GetMySqlConnection().Execute(sql, conditionString);
        }
        /// <summary>
        /// 异步更新删除标记为未删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> MarkUnDeleteOneAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = MySQLString.MarkUnDeleteSQLString<TEntity>();
            return await GetMySqlConnection().ExecuteAsync(sql, entity).ConfigureAwait(true);
        }
        /// <summary>
        /// 异步批量更新删除标记为未删除
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        public async Task<int> MarkUnDeleteManyAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            var sql = MySQLString.MarkUnDeleteSQLString<TEntity>();
            return await GetMySqlConnection().ExecuteAsync(sql, entities).ConfigureAwait(true);
        }
        /// <summary>
        /// 根据条件异步更新删除标记为未删除
        /// </summary>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public async Task<int> MarkUnDeleteManyAsync(string conditionString)
        {
            if (string.IsNullOrEmpty(conditionString))
            {
                throw new ArgumentException(StringResource._2DFFD5DF_292D_4E3F_9CA2_0B238B2258E7);
            }
            var sql = MySQLString.MarkUnDeleteSQLString<TEntity>(conditionString);
            return await GetMySqlConnection().ExecuteAsync(sql, conditionString).ConfigureAwait(true);
        }
        #endregion
    }
}
