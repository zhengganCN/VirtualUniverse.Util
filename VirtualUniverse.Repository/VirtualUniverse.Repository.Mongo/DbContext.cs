using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/2/23 10:36:12；更新时间：
************************************************************************************/
namespace VirtualUniverse.Repository.Mongo
{
    /// <summary>
    /// 类 描 述：
    /// </summary>
    public abstract class DbContext
    {
        public IMongoClient MongoClient { get; private set; }
        public IMongoDatabase MongoDatabase { get; private set; }
        private readonly DbContextOptionsBuilder contextOptionsBuilder = new DbContextOptionsBuilder();
        protected DbContext()
        {
            Init();
        }

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

        public IMongoCollection<TEntity> GetMongoCollection<TEntity>(MongoCollectionSettings settings = null)
        {
            return MongoDatabase.GetCollection<TEntity>(typeof(TEntity).Name, settings);
        }

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
    }
}
