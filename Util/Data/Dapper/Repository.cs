using MySql.Data.MySqlClient;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Util.Data.Dapper.Interface;
using Util.Data.Repository;
using System.Linq;

namespace Util.Data.Dapper
{
    public class Repository<TEntity> : Interface.IRepository<TEntity> where TEntity : class,IEntity
    {
        private string _connectionString;
        public UOW _unitOfWork;

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
            GetUnitOfWork();
        }

        public MySqlConnection GetMySqlConnection()
        {
            return _unitOfWork.MySqlConnection;
        }

        private UOW GetUnitOfWork()
        {
            _unitOfWork = new UOW
            {
                MySqlConnection = new MySqlConnection(_connectionString)
            };
            return _unitOfWork;
        }
        #region 插入操作
        public int InsertOne(TEntity entity)
        {
            if (entity==null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = SQLString.InsertSQLString(entity);
            return GetMySqlConnection().Execute(sql, entity);
        }
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
            var sql= SQLString.InsertSQLString(entity);
            return GetMySqlConnection().Execute(sql, entities);
        }
        public async Task<int> InsertOneAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = SQLString.InsertSQLString(entity);
            return await GetMySqlConnection().ExecuteAsync(sql, entity).ConfigureAwait(true);
        }
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
            var sql = SQLString.InsertSQLString(entity);
            return await GetMySqlConnection().ExecuteAsync(sql, entities).ConfigureAwait(true);
        }
        #endregion
        #region 更新操作
        public int UpdateOne(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = SQLString.UpdateSQLString(entity);
            return GetMySqlConnection().Execute(sql, entity);
        }
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
            var sql = SQLString.UpdateSQLString(entities.First());
            return GetMySqlConnection().Execute(sql, entities);
        }
        /// <summary>
        /// 批量更新指定字段，根据每条记录的Id更新每条记录
        /// </summary>
        /// <param name="entities">更新的数据</param>
        /// <param name="updateFields">更新的字段</param>
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
            var sql = SQLString.UpdateSQLString<TEntity>(updateFields);
            return GetMySqlConnection().Execute(sql, entities);
        }
        /// <summary>
        /// 批量更新符合条件的记录的指定字段
        /// </summary>
        /// <param name="entity">更新的数据</param>
        /// <param name="updateFields">更新的字段</param>
        /// <param name="conditionString">条件</param>
        /// <returns></returns>
        public int UpdateMany(TEntity entity, object updateFields, string conditionString)
        {
            if (entity == null || updateFields == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = SQLString.UpdateSQLString<TEntity>(updateFields, conditionString);
            return GetMySqlConnection().Execute(sql, entity);
        }
        public async Task<int> UpdateOneAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = SQLString.UpdateSQLString(entity);
            return await GetMySqlConnection().ExecuteAsync(sql, entity).ConfigureAwait(true);
        }
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
            var sql = SQLString.UpdateSQLString(entities.First());
            return await GetMySqlConnection().ExecuteAsync(sql, entities).ConfigureAwait(true);
        }
        /// <summary>
        /// 异步批量更新指定字段，根据每条记录的Id更新每条记录
        /// </summary>
        /// <param name="entities">更新的数据</param>
        /// <param name="updateFields">更新的字段</param>
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
            var sql = SQLString.UpdateSQLString<TEntity>(updateFields);
            return await GetMySqlConnection().ExecuteAsync(sql, entities).ConfigureAwait(true);
        }
        /// <summary>
        /// 异步批量更新符合条件的记录的指定字段
        /// </summary>
        /// <param name="entity">更新的数据</param>
        /// <param name="updateFields">更新的字段</param>
        /// <param name="conditionString">条件</param>
        /// <returns></returns>
        public async Task<int> UpdateManyAsync(TEntity entity, object updateFields, string conditionString)
        {
            if (entity == null || updateFields == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = SQLString.UpdateSQLString<TEntity>(updateFields, conditionString);
            return await GetMySqlConnection().ExecuteAsync(sql, entity).ConfigureAwait(true);
        }
        #endregion
        #region 查询操作
        public TEntity FindOne(string conditionString)
        {
            var sql = SQLString.QuerySQLString<TEntity>(conditionString);
            return GetMySqlConnection().QueryFirst<TEntity>(sql);
        }
        public IEnumerable<TEntity> FindMany(string conditionString, string sequenceField = "Id", EnumSequence sequence = EnumSequence.Ascending, int pageInde = 1, int pageSize = 10)
        {
            var sql = SQLString.QuerySQLString<TEntity>(conditionString, sequenceField,
                sequence, pageInde, pageSize);
            return GetMySqlConnection().Query<TEntity>(sql);
        }
        public IEnumerable<TEntity> FindAll()
        {
            var sql = SQLString.QuerySQLString<TEntity>();
            return GetMySqlConnection().Query<TEntity>(sql);
        }
        public IEnumerable<TEntity> FindAll(string conditionString)
        {
            var sql = SQLString.QuerySQLString<TEntity>(conditionString);
            return GetMySqlConnection().Query<TEntity>(sql);
        }

        public async Task<TEntity> FindOneAsync(string conditionString)
        {
            var sql = SQLString.QuerySQLString<TEntity>(conditionString);
            return await GetMySqlConnection().QueryFirstAsync<TEntity>(sql).ConfigureAwait(true);
        }
        public async Task<IEnumerable<TEntity>> FindManyAsync(string conditionString, string sequenceField = "Id", EnumSequence sequence = EnumSequence.Ascending, int pageInde = 1, int pageSize = 10)
        {
            var sql = SQLString.QuerySQLString<TEntity>(conditionString, sequenceField,
                sequence, pageInde, pageSize);
            return await GetMySqlConnection().QueryAsync<TEntity>(sql).ConfigureAwait(true);
        }
        public async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            var sql = SQLString.QuerySQLString<TEntity>();
            return await GetMySqlConnection().QueryAsync<TEntity>(sql).ConfigureAwait(true);
        }
        public async Task<IEnumerable<TEntity>> FindAllAsync(string conditionString)
        {
            var sql = SQLString.QuerySQLString<TEntity>(conditionString);
            return await GetMySqlConnection().QueryAsync<TEntity>(sql).ConfigureAwait(true);
        }
        #endregion
        #region 删除操作
        public int DeleteOne(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = SQLString.DeleteSQLString<TEntity>();
            return GetMySqlConnection().Execute(sql, entity);
        }
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
            var entity = entities.First();
            var sql = SQLString.DeleteSQLString<TEntity>();
            return GetMySqlConnection().Execute(sql, entities);
        }
        public int DeleteMany(string conditionString)
        {
            if (string.IsNullOrEmpty(conditionString))
            {
                throw new ArgumentException(StringResource._2DFFD5DF_292D_4E3F_9CA2_0B238B2258E7);
            }
            var sql = SQLString.DeleteSQLString<TEntity>(conditionString);
            return GetMySqlConnection().Execute(sql);
        }
        public async Task<int> DeleteOneAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = SQLString.DeleteSQLString<TEntity>();
            return await GetMySqlConnection().ExecuteAsync(sql, entity).ConfigureAwait(true);
        }
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
            var sql = SQLString.DeleteSQLString<TEntity>();
            return await GetMySqlConnection().ExecuteAsync(sql, entities).ConfigureAwait(true);
        }
        public async Task<int> DeleteManyAsync(string conditionString)
        {
            if (string.IsNullOrEmpty(conditionString))
            {
                throw new ArgumentException(StringResource._2DFFD5DF_292D_4E3F_9CA2_0B238B2258E7);
            }
            var sql = SQLString.DeleteSQLString<TEntity>(conditionString);
            return await GetMySqlConnection().ExecuteAsync(sql).ConfigureAwait(true);
        }
        #endregion
        #region 标记为删除
        public int MarkDeleteOne(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = SQLString.MarkDeleteSQLString<TEntity>();
            return GetMySqlConnection().Execute(sql, entity);
        }
        public int MarkDeleteMany(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            var sql = SQLString.MarkDeleteSQLString<TEntity>();
            return GetMySqlConnection().Execute(sql, entities);
        }
        public int MarkDeleteMany(string conditionString)
        {
            if (string.IsNullOrEmpty(conditionString))
            {
                throw new ArgumentException(StringResource._2DFFD5DF_292D_4E3F_9CA2_0B238B2258E7);
            }
            var sql = SQLString.MarkDeleteSQLString<TEntity>(conditionString);
            return GetMySqlConnection().Execute(sql, conditionString);
        }
        public async Task<int> MarkDeleteOneAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = SQLString.MarkDeleteSQLString<TEntity>();
            return await GetMySqlConnection().ExecuteAsync(sql, entity).ConfigureAwait(true);
        }
        public async Task<int> MarkDeleteManyAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            var sql = SQLString.MarkDeleteSQLString<TEntity>();
            return await GetMySqlConnection().ExecuteAsync(sql, entities).ConfigureAwait(true);
        }
        public async Task<int> MarkDeleteManyAsync(string conditionString)
        {
            if (string.IsNullOrEmpty(conditionString))
            {
                throw new ArgumentException(StringResource._2DFFD5DF_292D_4E3F_9CA2_0B238B2258E7);
            }
            var sql = SQLString.MarkDeleteSQLString<TEntity>(conditionString);
            return await GetMySqlConnection().ExecuteAsync(sql, conditionString).ConfigureAwait(true);
        }
        #endregion
        #region 标记为未删除
        public int MarkUnDeleteOne(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = SQLString.MarkUnDeleteSQLString<TEntity>();
            return GetMySqlConnection().Execute(sql, entity);
        }
        public int MarkUnDeleteMany(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            var sql = SQLString.MarkUnDeleteSQLString<TEntity>();
            return GetMySqlConnection().Execute(sql, entities);
        }
        public int MarkUnDeleteMany(string conditionString)
        {
            if (string.IsNullOrEmpty(conditionString))
            {
                throw new ArgumentException(StringResource._2DFFD5DF_292D_4E3F_9CA2_0B238B2258E7);
            }
            var sql = SQLString.MarkUnDeleteSQLString<TEntity>(conditionString);
            return GetMySqlConnection().Execute(sql, conditionString);
        }
        public async Task<int> MarkUnDeleteOneAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = SQLString.MarkUnDeleteSQLString<TEntity>();
            return await GetMySqlConnection().ExecuteAsync(sql, entity).ConfigureAwait(true);
        }
        public async Task<int> MarkUnDeleteManyAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            var sql = SQLString.MarkUnDeleteSQLString<TEntity>();
            return await GetMySqlConnection().ExecuteAsync(sql, entities).ConfigureAwait(true);
        }
        public async Task<int> MarkUnDeleteManyAsync(string conditionString)
        {
            if (string.IsNullOrEmpty(conditionString))
            {
                throw new ArgumentException(StringResource._2DFFD5DF_292D_4E3F_9CA2_0B238B2258E7);
            }
            var sql = SQLString.MarkUnDeleteSQLString<TEntity>(conditionString);
            return await GetMySqlConnection().ExecuteAsync(sql, conditionString).ConfigureAwait(true);
        }
        #endregion
    }
}
