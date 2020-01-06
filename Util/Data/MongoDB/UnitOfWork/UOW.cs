using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using Util.Data.MongoDB.Interface;

namespace Util.Data.MongoDB.UnitOfWork
{
    /// <summary>
    /// mongo工作单元
    /// </summary>
    public class UOW : IUOW
    {
        /// <summary>
        /// Mongo客户端
        /// </summary>
        public MongoClient Client { get; private set; }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public IMongoDatabase Database { get; private set; }
        private IClientSessionHandle clientSessionHandle;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectString">连接字符串</param>
        /// <param name="databaseName">数据库名称</param>
        public UOW(string connectString, string databaseName)
        {
            Client = new MongoClient(connectString);
            Database = Client.GetDatabase(databaseName);
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            clientSessionHandle.CommitTransaction();
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            clientSessionHandle.AbortTransaction();
        }
        /// <summary>
        /// 创建事务
        /// </summary>
        public void Transaction()
        {
            clientSessionHandle = Client.StartSession();
            clientSessionHandle.StartTransaction();
        }
    }
}
