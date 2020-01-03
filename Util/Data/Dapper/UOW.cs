using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using Util.Data.Dapper.Interface;

namespace Util.Data.Dapper
{
    public class UOW : IUOW
    {
        public MySqlConnection MySqlConnection { get; set; }

        private MySqlTransaction MySqlTransaction { get; set; }

        public void Commit()
        {
            MySqlTransaction.Commit();
            Close();
        }

        public void Rollback()
        {
            MySqlTransaction.Rollback();
            Close();
        }

        public void Transaction()
        {
            MySqlConnection.Open();
            MySqlTransaction = MySqlConnection.BeginTransaction();
        }

        public void Close()
        {
            MySqlConnection.Close();
        }
    }
}
