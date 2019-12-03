using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Util.DBContext
{
    /// <summary>
    /// MongoDB上下文（操作类）
    /// </summary>
    public class MongoDBContext
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly ILogger logger;
        private string _connectString; 

        /// <summary>
        /// 构造函数
        /// </summary>
        public MongoDBContext()
        {
            //logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            _client = ConnectMongoDbClient();
            _database = GetMongoDbDatabase();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectString">连接字符串</param>
        public MongoDBContext(string connectString)
        {
            _connectString = connectString;
        }

        /// <summary>
        /// 获取实体类的名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private string CollectionName<T>() where T : class
        {
            var collectionClassName = typeof(T).Name;
            return collectionClassName.Substring(0, collectionClassName.Length - 10);
        }

        /// <summary>
        /// 连接客户端
        /// </summary>
        /// <returns></returns>
        public MongoClient ConnectMongoDbClient()
        {
            try
            {
                var mongoClient = new MongoClient(_connectString);
                //logger.LogLogInformationrmation("MongoDb客户端连接成功");
                logger.LogInformation("MongoDb客户端连接成功");
                return mongoClient;
            }
            catch (Exception e)
            {
                logger.LogError("MongoDb客户端连接失败" + e.Message);
                return null;
            }
        }

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns></returns>
        public IMongoDatabase GetMongoDbDatabase()
        {
            try
            {
                var database = _client.GetDatabase(_connectString);
                logger.LogInformation("连接MongoDb数据库成功;");
                return database;
            }
            catch (Exception e)
            {
                logger.LogError("连接MongoDb数据库失败;" + e.Message);
                return null;
            }
        }
        
        /// <summary>
        /// 获取所有的数据库
        /// </summary>
        /// <returns></returns>
        public IList<BsonDocument> GetDatabases()
        {
            try
            {
                return _client.ListDatabases().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        
        #region 插入操作
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="data">实体数据</param>
        /// <returns></returns>
        public long InsertOne<T>(T data) where T : class
        {
            try
            {
                var mongoCollection = _database.GetCollection<T>(CollectionName<T>());
                mongoCollection.InsertOne(data);
                logger.LogInformation("成功插入一条数据;");
                logger.LogDebug(data.ToJson());
                return 1;
            }
            catch (Exception e)
            {
                logger.LogError("插入一条数据失败成功;" + e.Message);
                logger.LogDebug(data.ToJson());
                return -1;
            }
        }

        /// <summary>
        /// 异步插入一条数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="data">实体数据</param>
        /// <returns></returns>
        
        public async Task<int> InsertOneAsync<T>(T data) where T : class
        {
            try
            {
                var mongoCollection = _database.GetCollection<T>(CollectionName<T>());
                await mongoCollection.InsertOneAsync(data);
                logger.LogInformation("成功插入一条数据;");
                logger.LogDebug(data.ToJson());
                return 1;
            }
            catch (Exception e)
            {
                logger.LogError("插入一条数据失败成功;" + e.Message);
                logger.LogDebug(data.ToJson());
                return 0;
            }
        }
        
        /// <summary>
        /// 插入多条数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="datas">实体数据集合</param>
        public int InsertMany<T>(IList<T> datas) where T : class
        {
            try
            {
                var mongoCollection = _database.GetCollection<T>(CollectionName<T>());
                mongoCollection.InsertMany(datas);
                logger.LogInformation("成功插入{0}条数据;", datas.Count);
                logger.LogDebug(datas.ToJson());
                return datas.Count;
            }
            catch (Exception e)
            {
                logger.LogError("成功插入{0}条数据;" + e.Message, datas.Count);
                logger.LogDebug(datas.ToJson());
                return -1;
            }
        }
        
        /// <summary>
        /// 异步插入多条数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="datas">实体数据集合</param>
        /// <returns></returns>
        public async Task<int> InsertManyAsync<T>(IList<T> datas) where T : class
        {
            try
            {
                var mongoCollection = _database.GetCollection<T>(CollectionName<T>());

                await mongoCollection.InsertManyAsync(datas);
                logger.LogInformation("成功插入{0}条数据;", datas.Count);
                logger.LogDebug(datas.ToJson());
                return datas.Count;
            }
            catch (Exception e)
            {
                logger.LogError("插入{0}条数据失败;" + e.Message, datas.Count);
                logger.LogDebug(datas.ToJson());
                return -1;
            }
        }
        #endregion


        #region 查询操作
        
        /// <summary>
        /// 查询匹配的第一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="sortDefinition"></param>
        /// <returns></returns>
        public T FindFirst<T>(FilterDefinition<T> filter, SortDefinition<T> sortDefinition = null) where T : class
        {
            try
            {
                var mongoCollection = _database.GetCollection<T>(CollectionName<T>());
                T document = null;
                if (sortDefinition == null)
                {
                    document = mongoCollection.Find(filter).FirstOrDefault();
                }
                else
                {
                    document = mongoCollection.Find(filter).Sort(sortDefinition).FirstOrDefault();
                }
                logger.LogInformation("查询数据成功");
                logger.LogDebug(document.ToJson());
                return document;
            }
            catch (Exception e)
            {
                logger.LogError("查询数据失败;" + e.Message);
                logger.LogDebug(filter.ToJson());
                return null;
            }
        }
        
        /// <summary>
        /// 异步查询第一条匹配的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="sortDefinition"></param>
        /// <returns></returns>
        public async Task<T> FindFirstAsync<T>(FilterDefinition<T> filter, SortDefinition<T> sortDefinition = null) where T : class
        {
            try
            {
                var mongoCollection = _database.GetCollection<T>(CollectionName<T>());
                T document = null;
                if (sortDefinition == null)
                {
                    document = await mongoCollection.Find(filter).FirstOrDefaultAsync();
                }
                else
                {
                    document = await mongoCollection.Find(filter).Sort(sortDefinition).FirstOrDefaultAsync();
                }
                logger.LogInformation("查询数据成功");
                logger.LogDebug(document.ToJson());
                return document;
            }
            catch (Exception e)
            {
                logger.LogError("查询数据失败;" + e.Message);
                logger.LogDebug(filter.ToJson());
                return null;
            }
        }
        
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortDefinition"></param>
        /// <returns></returns>
        public IList<T> Find<T>(FilterDefinition<T> filter, int pageIndex = 1, int pageSize = 10, SortDefinition<T> sortDefinition = null) where T : class
        {
            try
            {
                var mongoCollection = _database.GetCollection<T>(CollectionName<T>());
                IList<T> documents = null;
                if (sortDefinition == null)
                {
                    documents = mongoCollection.Find(filter).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
                }
                else
                {
                    documents = mongoCollection.Find(filter).Sort(sortDefinition).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
                }
                logger.LogInformation("查询数据成功");
                logger.LogDebug(documents.ToJson());
                return documents;
            }
            catch (Exception e)
            {
                logger.LogError("查询数据失败;" + e.Message);
                logger.LogDebug(filter.ToJson());
                return null;
            }
        }
        
        /// <summary>
        /// 查询所有匹配的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="sortDefinition"></param>
        /// <returns></returns>
        public IList<T> FindAll<T>(FilterDefinition<T> filter, SortDefinition<T> sortDefinition = null) where T : class
        {
            try
            {
                var mongoCollection = _database.GetCollection<T>(CollectionName<T>());
                IList<T> documents = null;
                if (sortDefinition == null)
                {
                    documents = mongoCollection.Find(filter).ToList();
                }
                else
                {
                    documents = mongoCollection.Find(filter).Sort(sortDefinition).ToList();
                }
                logger.LogInformation("查询数据成功");
                logger.LogDebug(documents.ToJson());
                return documents;
            }
            catch (Exception e)
            {
                logger.LogError("查询数据失败;" + e.Message);
                logger.LogDebug(filter.ToJson());
                return null;
            }
        }
        #endregion
        #region 统计操作
        
        /// <summary>
        /// 统计匹配的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public long Count<T>(FilterDefinition<T> filter) where T : class
        {
            try
            {
                var mongoCollection = _database.GetCollection<T>(CollectionName<T>());
                var count = mongoCollection.CountDocuments(filter);
                logger.LogInformation("查询统计数据成功");
                logger.LogDebug(count.ToString());
                return count;
            }
            catch (Exception e)
            {
                logger.LogError("查询统计数据失败;" + e.Message);
                logger.LogDebug(filter.ToJson());
                return -1;
            }
        }

        /// <summary>
        /// 异步统计匹配的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<long> CountAsync<T>(FilterDefinition<T> filter) where T : class
        {
            try
            {
                var mongoCollection = _database.GetCollection<T>(CollectionName<T>());
                var count = await mongoCollection.CountDocumentsAsync(filter);
                logger.LogInformation("查询统计数据成功");
                logger.LogDebug(count.ToString());
                return count;
            }
            catch (Exception e)
            {
                logger.LogError("查询统计数据失败;" + e.Message);
                logger.LogDebug(filter.ToJson());
                return -1;
            }
        }
        #endregion
        #region 更新操作

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="filter">过滤器</param>
        /// <param name="update"></param>
        /// <returns></returns>
        public long UpdateOne<T>(FilterDefinition<T> filter, UpdateDefinition<T> update) where T : class
        {
            try
            {
                var mongoCollection = _database.GetCollection<T>(CollectionName<T>());
                var result = mongoCollection.UpdateOne(filter, update);
                logger.LogInformation("成功更新{0}条数据", result.ModifiedCount);
                logger.LogDebug("更新条件：{0};\n更新数据：{1}", filter.ToJson(), update.ToJson());
                return result.ModifiedCount;
            }
            catch (Exception e)
            {
                logger.LogError("更新数据失败;异常信息：{0}", e.Message);
                logger.LogDebug("更新条件：{0};\n更新数据：{1}", filter.ToJson(), update.ToJson());
                return -1;
            }
        }
        
        /// <summary>
        /// 异步更新一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<long> UpdateOneAsync<T>(FilterDefinition<T> filter, UpdateDefinition<T> update) where T : class
        {
            try
            {
                var mongoCollection = _database.GetCollection<T>(CollectionName<T>());
                var result = await mongoCollection.UpdateOneAsync(filter, update);
                logger.LogInformation("成功更新{0}条数据", result.ModifiedCount);
                logger.LogDebug("更新条件：{0};\n更新数据：{1}", filter.ToJson(), update.ToJson());
                return result.ModifiedCount;
            }
            catch (Exception e)
            {
                logger.LogError("更新数据失败;异常信息：{0}", e.Message);
                logger.LogDebug("更新条件：{0};\n更新数据：{1}", filter.ToJson(), update.ToJson());
                return -1;
            }
        }

        /// <summary>
        /// 更新多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public long UpdateMany<T>(FilterDefinition<T> filter, UpdateDefinition<T> update) where T : class
        {
            try
            {
                var mongoCollection = _database.GetCollection<T>(CollectionName<T>());
                var result = mongoCollection.UpdateMany(filter, update);
                logger.LogInformation("成功更新{0}条数据", result.ModifiedCount);
                logger.LogDebug("更新条件：{0};\n更新数据：{1}", filter.ToJson(), update.ToJson());
                return result.ModifiedCount;
            }
            catch (Exception e)
            {
                logger.LogError("更新数据失败;异常信息：{0}", e.Message);
                logger.LogDebug("更新条件：{0};\n更新数据：{1}", filter.ToJson(), update.ToJson());
                return -1;
            }
        }
        
        /// <summary>
        /// 异步更新多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<long> UpdateManyAsync<T>(FilterDefinition<T> filter, UpdateDefinition<T> update) where T : class
        {
            try
            {
                var mongoCollection = _database.GetCollection<T>(CollectionName<T>());
                var result = await mongoCollection.UpdateManyAsync(filter, update);
                if (result.IsModifiedCountAvailable)
                {
                    return result.ModifiedCount;
                }
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        /// <summary>
        /// 替换一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="updateBsonDocument"></param>
        /// <returns></returns>
        public long ReplaceOne<T>(FilterDefinition<T> filter, T updateBsonDocument) where T : class
        {
            try
            {
                var mongoCollection = _database.GetCollection<T>(CollectionName<T>());
                var replaceOneResult = mongoCollection.ReplaceOne(filter, updateBsonDocument);
                logger.LogInformation("成功替换{0}条数据", replaceOneResult.ModifiedCount);
                logger.LogDebug("替换条件：{0};\n替换数据：{1}", filter.ToJson(), updateBsonDocument.ToJson());
                return replaceOneResult.ModifiedCount;
            }
            catch (Exception e)
            {
                logger.LogError("替换数据失败;异常信息：{0}", e.Message);
                logger.LogDebug("替换条件：{0};\n替换数据：{1}", filter.ToJson(), updateBsonDocument.ToJson());
                return -1;
            }
        }
        
        /// <summary>
        /// 异步替换一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="filter"></param>
        /// <param name="updateBsonDocument"></param>
        /// <returns></returns>
        public async Task<long> ReplaceOneAsync<T>(FilterDefinition<T> filter, T updateBsonDocument) where T : class
        {
            try
            {
                var mongoCollection = _database.GetCollection<T>(CollectionName<T>());
                var replaceOneResult = await mongoCollection.ReplaceOneAsync(filter, updateBsonDocument);
                logger.LogInformation("成功替换{0}条数据", replaceOneResult.ModifiedCount);
                logger.LogDebug("替换条件：{0};\n替换数据：{1}", filter.ToJson(), updateBsonDocument.ToJson());
                return replaceOneResult.ModifiedCount;
            }
            catch (Exception e)
            {
                logger.LogError("替换数据失败;异常信息：{0}", e.Message);
                logger.LogDebug("替换条件：{0};\n替换数据：{1}", filter.ToJson(), updateBsonDocument.ToJson());
                return -1;
            }
        }
        #endregion
        #region 删除操作
        
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public long DeleteOne<T>(FilterDefinition<T> filter) where T : class
        {
            try
            {
                var mongoCollection = _database.GetCollection<T>(CollectionName<T>());
                var deleteResult = mongoCollection.DeleteOne(filter);
                logger.LogInformation("成功删除{0}条数据", deleteResult.DeletedCount);
                logger.LogDebug("删除条件：{0}", filter.ToJson());
                return deleteResult.DeletedCount;
            }
            catch (Exception e)
            {
                logger.LogError("删除数据失败;异常信息：{0}", e.Message);
                logger.LogDebug("删除条件：{0}", filter.ToJson());
                return -1;
            }
        }
        
        /// <summary>
        /// 异步删除一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<long> DeleteOneAsync<T>(FilterDefinition<T> filter) where T : class
        {
            try
            {
                var mongoCollection = _database.GetCollection<T>(CollectionName<T>());
                var deleteResult = await mongoCollection.DeleteOneAsync(filter);
                logger.LogInformation("成功删除{0}条数据", deleteResult.DeletedCount);
                logger.LogDebug("删除条件：{0}", filter.ToJson());
                return deleteResult.DeletedCount;
            }
            catch (Exception e)
            {
                logger.LogError("删除数据失败;异常信息：{0}", e.Message);
                logger.LogDebug("删除条件：{0}", filter.ToJson());
                return -1;
            }
        }
        
        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public long DeleteMany<T>(FilterDefinition<T> filter) where T : class
        {
            try
            {
                var mongoCollection = _database.GetCollection<T>(CollectionName<T>());
                var deleteResult = mongoCollection.DeleteMany(filter);
                logger.LogInformation("成功删除{0}条数据", deleteResult.DeletedCount);
                logger.LogDebug("删除条件：{0}", filter.ToJson());
                return deleteResult.DeletedCount;
            }
            catch (Exception e)
            {
                logger.LogError("删除数据失败;异常信息：{0}", e.Message);
                logger.LogDebug("删除条件：{0}", filter.ToJson());
                return -1;
            }
        }
        
        /// <summary>
        /// 异步删除多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<long> DeleteManyAsync<T>(FilterDefinition<T> filter) where T : class
        {
            try
            {
                var mongoCollection = _database.GetCollection<T>(CollectionName<T>());
                var deleteResult = await mongoCollection.DeleteManyAsync(filter);
                logger.LogInformation("成功删除{0}条数据", deleteResult.DeletedCount);
                logger.LogDebug("删除条件：{0}", filter.ToJson());
                return deleteResult.DeletedCount;
            }
            catch (Exception e)
            {
                logger.LogError("删除数据失败;异常信息：{0}", e.Message);
                logger.LogDebug("删除条件：{0}", filter.ToJson());
                return -1;
            }
        }
        #endregion
    }
}
