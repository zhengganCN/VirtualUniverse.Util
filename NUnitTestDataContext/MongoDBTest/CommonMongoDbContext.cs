using AmazedDataContext.MongoDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestAmazedDataContext.MongoDBTest
{
    class CommonMongoDbContext : MongoDbContext
    {
        public MongoDbSet<TbStudent> TbStudent { get; set; }

        public override void OnConfiguring(MongoDbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMongoDb(StaticConfigurationValues.MongoDBConnectionString, StaticConfigurationValues.MongoDBDatabase);
        }
    }
}
