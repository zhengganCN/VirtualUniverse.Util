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

        public TEntity Find(string sql)
        {
            GetMySqlConnection().Open();
            return GetMySqlConnection().QueryFirst<TEntity>(sql);
        }

        public int Insert(TEntity entity)
        {
            if (entity==null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = SQLString.InsertSQLString(entity);
            return GetMySqlConnection().Execute(sql, entity);
        }
        public int Insert(IList<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            var entity = entities[0];
            var sql= SQLString.InsertSQLString(entity);
            return GetMySqlConnection().Execute(sql, entities);
        }
        public async Task<int> InsertAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = SQLString.InsertSQLString(entity);
            return await GetMySqlConnection().ExecuteAsync(sql, entity).ConfigureAwait(true);
        }
        public async Task<int> InsertAsync(IList<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            var entity = entities[0];
            var sql = SQLString.InsertSQLString(entity);
            return await GetMySqlConnection().ExecuteAsync(sql, entities).ConfigureAwait(true);
        }
        public int Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = SQLString.UpdateSQLString(entity);
            return GetMySqlConnection().Execute(sql, entity);
        }
        public int Update(TEntity entity, string conditionString)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var sql = SQLString.UpdateSQLString(entity, conditionString);
            return GetMySqlConnection().Execute(sql, entity);
        }
        

        public int Update(IList<TEntity> entities, string conditionString)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            var entity = entities[0];
            var sql = SQLString.UpdateSQLString(entity, conditionString);
            return GetMySqlConnection().Execute(sql, entities);
        }
    }
}
