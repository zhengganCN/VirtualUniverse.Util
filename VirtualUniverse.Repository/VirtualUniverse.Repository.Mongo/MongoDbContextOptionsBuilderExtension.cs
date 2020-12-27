//using MongoDB.Driver;
//using System;
//using System.Collections.Generic;
//using System.Runtime.CompilerServices;
//using System.Text;

//namespace AmazedDataContext.MongoDB
//{
//    public static class MongoDbContextOptionsBuilderExtension
//    {
//        public static MongoDbContextOptionsBuilder UseMongoDb(this MongoDbContextOptionsBuilder optionsBuilder,string connectionString,string database)
//        {
//            optionsBuilder.MongoClient = new MongoClient(connectionString);
//            optionsBuilder.MongoDatabase = optionsBuilder.MongoClient.GetDatabase(database);
//            return optionsBuilder;
//        }
//    }
//}
