using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Data.Dapper.Interface;
using Util.Data.Dapper.SQLString;
using Util.Data.Dapper.UnitOfWork;

namespace Util.Data.Dapper.Repository
{
    /// <summary>
    /// MSSQL仓储模式
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class MSSQLRepository<TEntity> : AbstractRepository<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// 工作单元
        /// </summary> 
        private readonly UnitOfWork.UOW _unitOfWork;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString"></param>
        public MSSQLRepository(string connectionString)
        {
            _unitOfWork = new UnitOfWork.UOW();
            _unitOfWork.SetSqlConnection(new SqlConnection(connectionString));
        }
        /// <summary>
        /// 获取SQL连接
        /// </summary>
        /// <returns></returns>
        public override IDbConnection GetSqlConnection()
        {
            return _unitOfWork.DbConnection;
        }
        /// <summary>
        /// 获取SQLString帮助
        /// </summary>
        /// <returns></returns>
        public override ISQLString GetSqlString()
        {
            return new MSSQLString();
        }
        /// <summary>
        /// 获取工作单元
        /// </summary>
        /// <returns></returns>
        public override UnitOfWork.UOW GetUnitOfWork()
        {
            return _unitOfWork;
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
        public override IEnumerable<TEntity> FindMany(string conditionString, string sequenceField = "Id", EnumSequence sequence = EnumSequence.Ascending, int pageInde = 1, int pageSize = 10)
        {
            var sql = GetSqlString().QuerySQLString<TEntity>(conditionString, sequenceField,
                sequence, pageInde, pageSize);
            return GetSqlConnection().Query<TEntity>(sql);
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
        public override async Task<IEnumerable<TEntity>> FindManyAsync(string conditionString, string sequenceField = "Id", EnumSequence sequence = EnumSequence.Ascending, int pageInde = 1, int pageSize = 10)
        {
            var sql = GetSqlString().QuerySQLString<TEntity>(conditionString, sequenceField,
                sequence, pageInde, pageSize);
            return await GetSqlConnection().QueryAsync<TEntity>(sql).ConfigureAwait(true);
        }
    }
}
