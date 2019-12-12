using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Data.UOW.MongoDBUOW
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        public readonly MongoClient client;
        public readonly IMongoDatabase database;
        private IClientSessionHandle clientSessionHandle;

        public UnitOfWork(DbContext context)
        {
            client =  context.MongoClientConfiguration();
            database = context.MongoDatabaseConfiguration();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Commit()
        {
            clientSessionHandle.CommitTransaction();
        }
        /// <summary>
        /// 释放与事务有关的资源
        /// </summary>
        public void Rollback()
        {
            clientSessionHandle.AbortTransaction();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Transaction()
        {
            clientSessionHandle = client.StartSession();
            clientSessionHandle.StartTransaction();
        }
    }
}
