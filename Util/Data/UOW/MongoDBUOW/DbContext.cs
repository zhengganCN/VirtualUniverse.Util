using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Data.UOW.MongoDBUOW
{
    public abstract class DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract MongoClient MongoClientConfiguration();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract IMongoDatabase MongoDatabaseConfiguration();
    }
}
