//using Dapper;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using AmazedDataContext.Dapper.Interface;
//using AmazedDataContext.Dapper.SQLString;
//using AmazedDataContext.Dapper.UnitOfWork;
//using Microsoft.Data.SqlClient;

//namespace AmazedDataContext.Dapper.Repository
//{
//    /// <summary>
//    /// MSSQL仓储模式
//    /// </summary>
//    /// <typeparam name="TEntity"></typeparam>
//    public class MSSQLRepository<TEntity> : AbstractRepository<TEntity> where TEntity : class
//    {
//        /// <summary>
//        /// 工作单元
//        /// </summary> 
//        private readonly UnitOfWork.UOW _unitOfWork;
//        /// <summary>
//        /// 构造函数
//        /// </summary>
//        /// <param name="connectionString"></param>
//        public MSSQLRepository(string connectionString)
//        {
//            _unitOfWork = new UnitOfWork.UOW();
//            _unitOfWork.SetSqlConnection(new SqlConnection(connectionString));
//        }
//        /// <summary>
//        /// 获取SQL连接
//        /// </summary>
//        /// <returns></returns>
//        public override IDbConnection GetSqlConnection()
//        {
//            return _unitOfWork.DbConnection;
//        }
//        /// <summary>
//        /// 获取SQLString帮助
//        /// </summary>
//        /// <returns></returns>
//        public override ISQLString GetSqlString()
//        {
//            return new MSSQLString();
//        }
//        /// <summary>
//        /// 获取工作单元
//        /// </summary>
//        /// <returns></returns>
//        public override UnitOfWork.UOW GetUnitOfWork()
//        {
//            return _unitOfWork;
//        }
//    }
//}
