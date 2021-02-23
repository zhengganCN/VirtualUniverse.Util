using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/2/23 10:36:38；更新时间：
************************************************************************************/
namespace VirtualUniverse.Repository.Mongo
{
    /// <summary>
    /// 类 描 述：Db配置
    /// </summary>
    public class DbContextOptionsBuilder
    {
        public string ConnectionString { get;private set; }
        public string DatabaseName { get; private set; }
        public MongoDatabaseSettings MongoDatabaseSettings { get; private set; }
        public DbContextOptionsBuilder AddConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
            return this;
        }

        public DbContextOptionsBuilder AddDatabase(string databaseName, MongoDatabaseSettings settings =null)
        {
            DatabaseName = databaseName;
            MongoDatabaseSettings = settings;
            return this;
        }
    }
}
