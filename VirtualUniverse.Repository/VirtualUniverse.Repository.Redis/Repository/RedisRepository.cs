using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using VirtualUniverse.Repository.Redis.Interfaces;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/2 12:48:26；更新时间：
************************************************************************************/
namespace VirtualUniverse.Repository.Redis.Repository
{
    /// <summary>
    /// 类说明：Redis仓储
    /// </summary>
    public class RedisRepository : IDisposable, IRedisRepository
    {
        private ConnectionMultiplexer connection;
        private ConfigurationOptions _configurationOptions;
        private int _db;
        private bool disposedValue;
        /// <summary>
        /// Redis仓储构造函数
        /// </summary>
        /// <param name="configurationOptions">配置项</param>
        /// <param name="db">数据库序号，默认为 0 </param>
        public RedisRepository(ConfigurationOptions configurationOptions, int db = 0)
        {
            _configurationOptions = configurationOptions;
            _db = db;
        }
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="configurationOptions">配置选项</param>
        /// <param name="db">数据库号</param>
        public virtual void OnConfiguring(ConfigurationOptions configurationOptions, int db = 0)
        {
            _configurationOptions = configurationOptions;
            _db = db;
        }
        /// <summary>
        /// 缓存数据库，数据库连接
        /// </summary>
        private ConnectionMultiplexer CacheConnection
        {
            get
            {
                if (connection == null || !connection.IsConnected)
                {
                    connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(_configurationOptions)).Value;
                }
                return connection;
            }
        }

        /// <summary>
        /// 缓存数据库
        /// </summary>
        private IDatabase CacheRedis => CacheConnection.GetDatabase(_db);

        #region String的增、改、查操作

        /// <summary>
        /// 保存一条记录
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间（多久之后过期）</param>
        /// <returns></returns>
        public bool StringSet(string key, string value, TimeSpan? expiry = default)
        {
            return CacheRedis.StringSet(key, value, expiry);
        }

        /// <summary>
        /// 保存一条记录
        /// </summary>
        /// <typeparam name="T">保存对象的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间（多久之后过期）</param>
        /// <returns></returns>
        public bool StringSet<T>(string key, T value, TimeSpan? expiry = default)
        {
            string json = JsonConvert.SerializeObject(value);
            return CacheRedis.StringSet(key, json, expiry);
        }

        /// <summary>
        /// 保存多条记录
        /// </summary>
        /// <param name="pairs">键值对</param>
        /// <returns></returns>
        public bool StringSet(Dictionary<string, string> pairs)
        {
            var keyValuePairs = new KeyValuePair<RedisKey, RedisValue>[pairs.Count];
            int index = 0;
            foreach (var pair in pairs)
            {
                keyValuePairs[index] = new KeyValuePair<RedisKey, RedisValue>(pair.Key, pair.Value);
                index++;
            }
            return CacheRedis.StringSet(keyValuePairs);
        }

        /// <summary>
        /// 为指定key的value追加字符串
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>返回字符串追加后的长度</returns>
        public long StringAppend(string key, string value)
        {
            return CacheRedis.StringAppend(key, value);
        }

        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public string StringGet(string key)
        {
            return CacheRedis.StringGet(key);
        }

        /// <summary>
        /// 获取一个key的对象
        /// </summary>
        /// <typeparam name="T">保存对象的类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        public T StringGet<T>(string key)
        {
            return JsonConvert.DeserializeObject<T>(CacheRedis.StringGet(key));
        }

        /// <summary>
        /// 获取多条记录
        /// </summary>
        /// <param name="keys">键列表</param>
        /// <returns></returns>
        public IDictionary<string, string> StringGet(IList<string> keys)
        {
            var redisKeys = new RedisKey[keys.Count];
            var index = 0;
            foreach (var key in keys)
            {
                redisKeys[index] = key;
                index++;
            }
            var redisValues = CacheRedis.StringGet(redisKeys);
            var pairs = new Dictionary<string, string>();
            for (int i = 0; i < redisKeys.Length; i++)
            {
                pairs.Add(redisKeys[i], redisValues[i]);
            }
            return pairs;
        }

        #endregion

        #region List的增、删、改、查操作

        /// <summary>
        /// 获取List列表
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public IList<string> ListGet(string key)
        {
            var values = new List<string>();
            var count = CacheRedis.ListLength(key);
            for (int i = 0; i < count; i++)
            {
                values.Add(CacheRedis.ListGetByIndex(key, i));
            }
            return values;
        }

        /// <summary>
        /// 获取List列表
        /// </summary>
        /// <typeparam name="T">保存对象的类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        public IList<T> ListGet<T>(string key)
        {
            var values = new List<T>();
            var count = CacheRedis.ListLength(key);
            for (int i = 0; i < count; i++)
            {
                values.Add(JsonConvert.DeserializeObject<T>(CacheRedis.ListGetByIndex(key, i)));
            }
            return values;
        }

        /// <summary>
        /// 获取List列表中指定索引处的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public string ListGetByIndex(string key, int index)
        {
            return CacheRedis.ListGetByIndex(key, index);
        }

        /// <summary>
        /// 获取List列表中指定索引处的对象
        /// </summary>
        /// <typeparam name="T">保存对象的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public T ListGetByIndex<T>(string key, int index)
        {
            return JsonConvert.DeserializeObject<T>(CacheRedis.ListGetByIndex(key, index));
        }

        /// <summary>
        /// 替换List列表中指定索引处的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="index">索引</param>
        /// <param name="value">值</param>
        public void ListSetByIndex(string key, long index, string value)
        {
            CacheRedis.ListSetByIndex(key, index, value);
        }

        /// <summary>
        /// 替换List列表中指定索引处的对象
        /// </summary>
        /// <typeparam name="T">保存对象的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="index">索引</param>
        /// <param name="value">值</param>
        public void ListSetByIndex<T>(string key, long index, T value)
        {
            CacheRedis.ListSetByIndex(key, index, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// 移除列表中与value匹配的值<br></br>
        /// 当 count 大于 0 时：从头到尾移除列表中与value匹配的值，移除数量为count的绝对值。<br></br>
        /// 当 count 小于 0 时：从尾到头移除列表中与value匹配的值，移除数量为count的绝对值。
        /// 当 count 等于 0 时：移除列表中所有与value匹配的值<br></br>
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="count">移除行为</param>
        /// <returns>移除的值的个数</returns>
        public long ListRemove(string key, string value, long count = 0)
        {
            return CacheRedis.ListRemove(key, value, count);
        }

        /// <summary>
        /// 移除列表中与value匹配的对象<br></br>
        /// 当 count 大于 0 时：从头到尾移除列表中与value匹配的值，移除数量为count的绝对值。<br></br>
        /// 当 count 小于 0 时：从尾到头移除列表中与value匹配的值，移除数量为count的绝对值。<br></br>
        /// 当 count 等于 0 时：移除列表中所有与value匹配的值<br></br>
        /// </summary>
        /// <typeparam name="T">保存对象的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="count">移除行为</param>
        /// <returns>移除的值的个数</returns>
        public long ListRemove<T>(string key, T value, long count = 0)
        {
            return CacheRedis.ListRemove(key, JsonConvert.SerializeObject(value), count);
        }

        #endregion

        #region Hash的增、删、改、查操作

        /// <summary>
        /// 获取Hash表
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public HashEntry[] HashGetAll(string key)
        {
            return CacheRedis.HashGetAll(key);
        }

        /// <summary>
        /// 获取Hash表
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>hash表中的键值对</returns>
        public Dictionary<string, string> HashGet(string key)
        {
            var pairs = new Dictionary<string, string>();
            var hash = CacheRedis.HashGetAll(key);
            for (int i = 0; i < hash.Length; i++)
            {
                pairs.Add(hash[i].Name, hash[0].Value);
            }
            return pairs;
        }

        /// <summary>
        /// 获取Hash表
        /// </summary>
        /// <typeparam name="T">hash表中保存对象的类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>hash表中的键值对</returns>
        /// <returns></returns>
        public Dictionary<string, T> HashGet<T>(string key)
        {
            var pairs = new Dictionary<string, T>();
            var hash = CacheRedis.HashGetAll(key);
            for (int i = 0; i < hash.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(hash[0].Value.ToString()))
                {
                    pairs.Add(hash[i].Name, default);
                }
                else
                {
                    pairs.Add(hash[i].Name, JsonConvert.DeserializeObject<T>(hash[0].Value.ToString()));
                }
            }
            return pairs;
        }

        /// <summary>
        /// 获取hash表中指定字段的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public string HashGet(string key, string field)
        {
            return CacheRedis.HashGet(key, field);
        }

        /// <summary>
        /// 获取hash表中指定字段的对象
        /// </summary>
        /// <typeparam name="T">hash表中保存对象的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public T HashGet<T>(string key, string field)
        {
            var value = CacheRedis.HashGet(key, field);
            return JsonConvert.DeserializeObject<T>(value.ToString());
        }

        /// <summary>
        /// 保存Hash表
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="pairs">hash表中的键值对</param>
        public void HashSet(string key, Dictionary<string, string> pairs)
        {
            var hashEntries = new HashEntry[pairs.Count];
            var index = 0;
            foreach (var pair in pairs)
            {
                hashEntries[index] = new HashEntry(pair.Key, pair.Value);
                index++;
            }
            CacheRedis.HashSet(key, hashEntries);
        }

        /// <summary>
        /// 保存Hash表
        /// </summary>
        /// <typeparam name="T">hash表中保存对象的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="pairs">hash表中的键值对</param>
        public void HashSet<T>(string key, Dictionary<string, T> pairs)
        {
            var hashEntries = new HashEntry[pairs.Count];
            var index = 0;
            foreach (var pair in pairs)
            {
                hashEntries[index] = new HashEntry(pair.Key, JsonConvert.SerializeObject(pair.Value));
                index++;
            }
            CacheRedis.HashSet(key, hashEntries);
        }

        /// <summary>
        /// 删除Hash表中指定字段的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public bool HashDelete(string key, string field)
        {
            return CacheRedis.HashDelete(key, field);
        }

        /// <summary>
        /// 判断Hash表中是否存在指定字段
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        public bool HashExist(string key, string field)
        {
            return CacheRedis.HashExists(key, field);
        }

        #endregion

        #region Set的增、删、改、查操作

        /// <summary>
        /// 向Set集合中添加值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool SetInsert(string key, string value)
        {
            return CacheRedis.SetAdd(key, value);
        }

        /// <summary>
        /// 向Set集合中添加值
        /// </summary>
        /// <typeparam name="T">集合保存的值的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool SetInsert<T>(string key, T value)
        {
            return CacheRedis.SetAdd(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// 删除Set集合中的匹配值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">要删除的值</param>
        /// <returns></returns>
        public bool SetDelete(string key, string value)
        {
            return CacheRedis.SetRemove(key, value);
        }

        /// <summary>
        /// 删除Set集合中的匹配值
        /// </summary>
        /// <typeparam name="T">集合保存的值的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">要删除的值</param>
        /// <returns></returns>
        public bool SetDelete<T>(string key, T value)
        {
            return CacheRedis.SetRemove(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// Set集合大小
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public long SetLength(string key)
        {
            return CacheRedis.SetLength(key);
        }

        /// <summary>
        /// 判断Set集合中是否存在指定的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool SetContains(string key, string value)
        {
            return CacheRedis.SetContains(key, value);
        }

        /// <summary>
        /// 判断Set集合中是否存在指定的值
        /// </summary>
        /// <typeparam name="T">集合保存的值的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool SetContains<T>(string key, T value)
        {
            return CacheRedis.SetContains(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// 获取Set集合中所有匹配的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="pattern">模式匹配（正则）</param>
        /// <param name="pageSize">匹配值的数量</param>
        /// <returns></returns>
        public IEnumerable<string> SetGet(string key, string pattern, int pageSize = 250)
        {
            var result = new List<string>();
            var values = CacheRedis.SetScan(key, pattern, pageSize);
            foreach (var value in values)
            {
                result.Add(value);
            }
            return result;
        }

        /// <summary>
        /// 获取Set集合中所有匹配的值
        /// </summary>
        /// <typeparam name="T">集合保存的值的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="pattern">模式匹配（正则）</param>
        /// <param name="pageSize">匹配值的数量</param>
        /// <returns></returns>
        public IEnumerable<T> SetGet<T>(string key, string pattern, int pageSize)
        {
            var result = new List<T>();
            var values = CacheRedis.SetScan(key, pattern, pageSize);
            foreach (var value in values)
            {
                result.Add(JsonConvert.DeserializeObject<T>(value));
            }
            return result;
        }

        /// <summary>
        /// 获取Set集合中所有值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public IEnumerable<string> SetGet(string key)
        {
            var result = new List<string>();
            var values = CacheRedis.SetMembers(key);
            foreach (var value in values)
            {
                result.Add(value);
            }
            return result;
        }

        /// <summary>
        /// 获取Set集合中所有值
        /// </summary>
        /// <typeparam name="T">集合保存的值的类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        public IEnumerable<T> SetGet<T>(string key)
        {
            var result = new List<T>();
            var values = CacheRedis.SetMembers(key);
            foreach (var value in values)
            {
                result.Add(JsonConvert.DeserializeObject<T>(value));
            }
            return result;
        }

        #endregion

        #region ZSet的增、删、改、查操作

        /// <summary>
        /// 向ZSet集合中添加值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="score">权重</param>
        /// <returns></returns>
        public bool ZSetInsert(string key, string value, double score)
        {
            return CacheRedis.SortedSetAdd(key, value, score);
        }

        /// <summary>
        /// 向ZSet集合中添加值
        /// </summary>
        /// <typeparam name="T">集合保存的值的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="score">权重</param>
        /// <returns></returns>
        public bool ZSetInsert<T>(string key, T value, double score)
        {
            return CacheRedis.SortedSetAdd(key, JsonConvert.SerializeObject(value), score);
        }

        /// <summary>
        /// 删除ZSet集合中匹配的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool ZSetDelete(string key, string value)
        {
            return CacheRedis.SortedSetRemove(key, value);
        }

        /// <summary>
        /// 删除ZSet集合中匹配的值
        /// </summary>
        /// <typeparam name="T">集合保存的值的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool ZSetDelete<T>(string key, string value)
        {
            return CacheRedis.SortedSetRemove(key, value);
        }

        /// <summary>
        /// 计算ZSet集合的大小
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public long ZSetLength(string key)
        {
            return CacheRedis.SortedSetLength(key);
        }

        /// <summary>
        /// 获取ZSet集合中所有匹配的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="pattern">模式匹配（正则）</param>
        /// <param name="pageSize">匹配值的数量</param>
        /// <returns></returns>
        public IDictionary<string, double> ZSetGet(string key, string pattern, int pageSize)
        {
            var result = new Dictionary<string, double>();
            var values = CacheRedis.SortedSetScan(key, pattern, pageSize);
            foreach (var value in values)
            {
                result.Add(value.Element, value.Score);
            }
            return result;
        }

        /// <summary>
        /// 获取ZSet集合中所有匹配的值
        /// </summary>
        /// <typeparam name="T">集合保存的值的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="pattern">模式匹配（正则）</param>
        /// <param name="pageSize">匹配值的数量</param>
        /// <returns></returns>
        public IDictionary<T, double> ZSetGet<T>(string key, string pattern, int pageSize)
        {
            var result = new Dictionary<T, double>();
            var values = CacheRedis.SortedSetScan(key, pattern, pageSize);
            foreach (var value in values)
            {
                result.Add(JsonConvert.DeserializeObject<T>(value.Element), value.Score);
            }
            return result;
        }

        #endregion

        #region 删除操作

        /// <summary>
        /// 删除单个key
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>是否删除成功</returns>
        public bool KeyDelete(string key)
        {
            return CacheRedis.KeyDelete(key);
        }

        /// <summary>
        /// 删除多个key
        /// </summary>
        /// <param name="keys">键集合</param>
        /// <returns>成功删除的个数</returns>
        public long KeysDelete(IList<string> keys)
        {
            var redisKeys = new RedisKey[keys.Count];
            var index = 0;
            foreach (var key in keys)
            {
                redisKeys[index] = key;
                index++;
            }
            return CacheRedis.KeyDelete(redisKeys);
        }

        #endregion

        #region 重命名操作

        /// <summary>
        /// 重新命名key
        /// </summary>
        /// <param name="oldKey">旧键</param>
        /// <param name="newKey">新键</param>
        /// <returns></returns>
        public bool RenameKey(string oldKey, string newKey)
        {
            return CacheRedis.KeyRename(oldKey, newKey);
        }

        #endregion

        #region 缓存过期时间设置

        /// <summary>
        /// 设置缓存过期
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="datetime">过期时间</param>
        public void SetExpire(string key, DateTime datetime)
        {
            CacheRedis.KeyExpire(key, datetime);
        }

        #endregion

        #region 判断Key是否存在

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public bool KeyExist(string key)
        {
            return CacheRedis.KeyExists(key);
        }

        #endregion

        /// <summary>
        /// 清理
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
                CacheConnection.CloseAsync();
                disposedValue = true;
            }
        }

        /// <summary>
        /// 清理
        /// </summary>
        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
