using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using Util.Data.Dapper.Interface;

namespace Util.Data.Dapper
{
    /// <summary>
    /// MySQL工作单元
    /// </summary>
    public class MySQLUOW : IUOW
    {
        /// <summary>
        /// MySQL连接
        /// </summary>
        public MySqlConnection MySqlConnection { get; set; }

        private MySqlTransaction MySqlTransaction { get; set; }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            MySqlTransaction.Commit();
            Close();
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            MySqlTransaction.Rollback();
            Close();
        }
        /// <summary>
        /// 创建事务
        /// </summary>
        public void Transaction()
        {
            MySqlConnection.Open();
            MySqlTransaction = MySqlConnection.BeginTransaction();
        }
        /// <summary>
        /// 手动关闭连接
        /// </summary>
        public void Close()
        {
            MySqlConnection.Close();
        }
    }
}
