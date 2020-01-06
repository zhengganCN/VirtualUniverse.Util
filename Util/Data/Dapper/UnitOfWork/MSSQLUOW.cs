using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Util.Data.Dapper.Interface;

namespace Util.Data.Dapper.UnitOfWork
{
    /// <summary>
    /// MSSQL工作单元
    /// </summary>
    public class MSSQLUOW : IUOW
    {
        /// <summary>
        /// MSSQL连接
        /// </summary>
        public DbConnection SqlConnection { get; private set; }

        private DbTransaction SqlTransaction { get; set; }
        public void SetSqlConnection(DbConnection dbConnection)
        {
            SqlConnection = dbConnection;
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            SqlTransaction.Commit();
            Close();
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            SqlTransaction.Rollback();
            Close();
        }
        /// <summary>
        /// 创建事务
        /// </summary>
        public void Transaction()
        {
            SqlConnection.Open();
            SqlTransaction= SqlConnection.BeginTransaction();
        }

        /// <summary>
        /// 手动关闭连接
        /// </summary>
        public void Close()
        {
            SqlConnection.Close();
        }
    }
}
