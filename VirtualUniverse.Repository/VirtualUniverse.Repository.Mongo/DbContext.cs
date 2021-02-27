using MongoDB.Driver;
using System;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/2/23 10:36:12；更新时间：
************************************************************************************/
namespace VirtualUniverse.Repository.Mongo
{
    /// <summary>
    /// 类 描 述：上下文
    /// </summary>
    public abstract class DbContext : IDisposable
    {
        /// <summary>
        /// 客户端
        /// </summary>
        public IMongoClient MongoClient { get; private set; }
        /// <summary>
        /// 数据库
        /// </summary>
        public IMongoDatabase MongoDatabase { get; private set; }
        private readonly DbContextOptionsBuilder contextOptionsBuilder = new DbContextOptionsBuilder();
        private bool disposedValue;

        /// <summary>
        /// 初始化
        /// </summary>
        protected DbContext()
        {
            Init();
        }

        /// <summary>
        /// 配置构造
        /// </summary>
        /// <param name="builder"></param>
        protected abstract void OnConfiguring(DbContextOptionsBuilder builder);

        private void Init()
        {
            OnConfiguring(contextOptionsBuilder);
            MongoClient = new MongoClient(contextOptionsBuilder.ConnectionString);
            MongoDatabase = GetMongoDatabase(contextOptionsBuilder.DatabaseName, contextOptionsBuilder.MongoDatabaseSettings);
            InstantiationDbSet();
        }

        private IMongoDatabase GetMongoDatabase(string databaseName,MongoDatabaseSettings mongoDatabaseSettings = null)
        {
            if (!string.IsNullOrWhiteSpace(contextOptionsBuilder.DatabaseName))
            {
                return MongoClient.GetDatabase(databaseName, mongoDatabaseSettings);
            }
            return default;
        }

        /// <summary>
        /// 获取表对象
        /// </summary>
        /// <typeparam name="TEntity">实体</typeparam>
        /// <param name="settings">设置</param>
        /// <returns></returns>
        public IMongoCollection<TEntity> GetMongoCollection<TEntity>(MongoCollectionSettings settings = null)
        {
            return MongoDatabase.GetCollection<TEntity>(typeof(TEntity).Name, settings);
        }
        /// <summary>
        /// 获取表对象
        /// </summary>
        /// <typeparam name="TEntity">实体</typeparam>
        /// <param name="databaseName">数据库名称</param>
        /// <param name="mongoDatabaseSettings">数据库设置</param>
        /// <param name="mongoCollectionSettings">表设置</param>
        /// <returns></returns>
        public IMongoCollection<TEntity> GetMongoCollection<TEntity>(string databaseName,
            MongoDatabaseSettings mongoDatabaseSettings = null, MongoCollectionSettings mongoCollectionSettings = null)
        {
            return GetMongoDatabase(databaseName, mongoDatabaseSettings).GetCollection<TEntity>(typeof(TEntity).Name, mongoCollectionSettings);
        }

        /// <summary>
        /// 实例化DbSet
        /// </summary>
        private void InstantiationDbSet()
        {
            var properties = GetType().GetProperties();
            var genericTypeName = typeof(DbSet<>).Name;
            foreach (var property in properties)
            {
                if (property.PropertyType.Name == genericTypeName && property.GetValue(this) is null)
                {
                    var type = property.PropertyType.GenericTypeArguments[0];
                    var genericType = typeof(MongoDbSet<>);
                    type = genericType.MakeGenericType(type);
                    var instanceObject = Activator.CreateInstance(type, MongoDatabase);
                    try
                    {
                        property.SetValue(this, instanceObject);
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException(ex.Message);
                    }
                }
            }
        }
        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // 释放托管状态(托管对象)
                }

                disposedValue = true;
            }
        }
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
