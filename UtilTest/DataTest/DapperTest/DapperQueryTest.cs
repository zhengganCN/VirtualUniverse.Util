
using Dapper;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtilTest.DataTest.DapperTest
{
    public class DapperQueryTest
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void Find()
        {
            ADSL();
        }


        public class TdD
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int DeptId { get; set; }
            public double Salary { get; set; }
        }


        public static void ADSL()
        {
            string sqlOrderDetails = "SELECT * FROM TdD;";
            string sqlOrderDetail = "SELECT * FROM TdD WHERE Id = @Id;";
            string sqlCustomerInsert = "INSERT INTO TdD (Name) Values (@Name);";

            using var connection = new MySqlConnection(StaticConfigurationValues.MySQLConnectionString);
            connection.Open();

            var invoices = connection.Query<TdD>(sqlOrderDetails).ToList();
        }
    }
}
