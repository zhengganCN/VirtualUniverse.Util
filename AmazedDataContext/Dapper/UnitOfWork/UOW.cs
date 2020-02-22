using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace AmazedDataContext.Dapper.UnitOfWork
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class UOW
    {
        /// <summary>
        /// SQL连接
        /// </summary>
        public DbConnection DbConnection { get; private set; }
        /// <summary>
        /// 事务
        /// </summary>
        private DbTransaction DbTransaction { get; set; }
        /// <summary>
        /// 设置数据库连接
        /// </summary>
        /// <param name="dbConnection"></param>
        public void SetSqlConnection(DbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            DbTransaction.Commit();
            Close();
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            DbTransaction.Rollback();
            Close();
        }
        /// <summary>
        /// 创建事务
        /// </summary>
        public void Transaction()
        {
            DbConnection.Open();
            DbTransaction = DbConnection.BeginTransaction();
        }
        /// <summary>
        /// 手动关闭连接
        /// </summary>
        public void Close()
        {
            DbConnection.Close();
        }
    }
}
