using StackExchange.Redis;
using System;
using System.Collections.Generic;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/2 13:02:03；更新时间：
************************************************************************************/
namespace VirtualUniverse.Repository.Redis.Interfaces
{
    /// <summary>
    /// 类说明：Redis仓储接口
    /// </summary>
    public interface IRedisRepository
    {
        /// <summary>
        /// 删除Hash表中指定字段的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        bool HashDelete(string key, string field);

        /// <summary>
        /// 判断Hash表中是否存在指定字段
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        bool HashExist(string key, string field);

        /// <summary>
        /// 获取Hash表
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        Dictionary<string, string> HashGet(string key);

        /// <summary>
        /// 获取hash表中指定字段的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        string HashGet(string key, string field);

        /// <summary>
        /// 获取Hash表
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        Dictionary<string, T> HashGet<T>(string key);

        /// <summary>
        /// 获取hash表中指定字段的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        T HashGet<T>(string key, string field);

        /// <summary>
        /// 获取Hash表
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        HashEntry[] HashGetAll(string key);

        /// <summary>
        /// 保存Hash表
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="pairs">hash表中的键值对</param>
        void HashSet(string key, Dictionary<string, string> pairs);

        /// <summary>
        /// 保存Hash表
        /// </summary>
        /// <typeparam name="T">hash表中保存对象的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="pairs">hash表中的键值对</param>
        void HashSet<T>(string key, Dictionary<string, T> pairs);

        /// <summary>
        /// 删除单个key
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>是否删除成功</returns>
        bool KeyDelete(string key);

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        bool KeyExist(string key);

        /// <summary>
        /// 删除多个key
        /// </summary>
        /// <param name="keys">键集合</param>
        /// <returns>成功删除的个数</returns>
        long KeysDelete(IList<string> keys);

        /// <summary>
        /// 获取List列表
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        IList<string> ListGet(string key);

        /// <summary>
        /// 获取List列表
        /// </summary>
        /// <typeparam name="T">保存对象的类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        IList<T> ListGet<T>(string key);

        /// <summary>
        /// 获取List列表中指定索引处的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="index">索引</param>
        /// <returns></returns>
        string ListGetByIndex(string key, int index);

        /// <summary>
        /// 获取List列表中指定索引处的对象
        /// </summary>
        /// <typeparam name="T">保存对象的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="index">索引</param>
        /// <returns></returns>
        T ListGetByIndex<T>(string key, int index);

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
        long ListRemove(string key, string value, long count = 0);

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
        long ListRemove<T>(string key, T value, long count = 0);

        /// <summary>
        /// 替换List列表中指定索引处的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="index">索引</param>
        /// <param name="value">值</param>
        void ListSetByIndex(string key, long index, string value);

        /// <summary>
        /// 替换List列表中指定索引处的对象
        /// </summary>
        /// <typeparam name="T">保存对象的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="index">索引</param>
        /// <param name="value">值</param>
        void ListSetByIndex<T>(string key, long index, T value);

        /// <summary>
        /// 重新命名key
        /// </summary>
        /// <param name="oldKey">旧键</param>
        /// <param name="newKey">新键</param>
        /// <returns></returns>
        bool RenameKey(string oldKey, string newKey);

        /// <summary>
        /// 判断Set集合中是否存在指定的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        bool SetContains(string key, string value);

        /// <summary>
        /// 判断Set集合中是否存在指定的值
        /// </summary>
        /// <typeparam name="T">集合保存的值的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        bool SetContains<T>(string key, T value);

        /// <summary>
        /// 删除Set集合中的匹配值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">要删除的值</param>
        /// <returns></returns>
        bool SetDelete(string key, string value);

        /// <summary>
        /// 删除Set集合中的匹配值
        /// </summary>
        /// <typeparam name="T">集合保存的值的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">要删除的值</param>
        /// <returns></returns>
        bool SetDelete<T>(string key, T value);

        /// <summary>
        /// 设置缓存过期
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="datetime">过期时间</param>
        void SetExpire(string key, DateTime datetime);

        /// <summary>
        /// 获取Set集合中所有值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        IEnumerable<string> SetGet(string key);

        /// <summary>
        /// 获取Set集合中所有匹配的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="pattern">模式匹配（正则）</param>
        /// <param name="pageSize">匹配值的数量</param>
        /// <returns></returns>
        IEnumerable<string> SetGet(string key, string pattern, int pageSize = 250);

        /// <summary>
        /// 获取Set集合中所有值
        /// </summary>
        /// <typeparam name="T">集合保存的值的类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        IEnumerable<T> SetGet<T>(string key);

        /// <summary>
        /// 获取Set集合中所有匹配的值
        /// </summary>
        /// <typeparam name="T">集合保存的值的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="pattern">模式匹配（正则）</param>
        /// <param name="pageSize">匹配值的数量</param>
        /// <returns></returns>
        IEnumerable<T> SetGet<T>(string key, string pattern, int pageSize);

        /// <summary>
        /// 向Set集合中添加值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        bool SetInsert(string key, string value);

        /// <summary>
        /// 向Set集合中添加值
        /// </summary>
        /// <typeparam name="T">集合保存的值的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        bool SetInsert<T>(string key, T value);

        /// <summary>
        /// Set集合大小
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        long SetLength(string key);

        /// <summary>
        /// 为指定key的value追加字符串
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>返回字符串追加后的长度</returns>
        long StringAppend(string key, string value);

        /// <summary>
        /// 获取多条记录
        /// </summary>
        /// <param name="keys">键列表</param>
        /// <returns></returns>
        IDictionary<string, string> StringGet(IList<string> keys);

        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        string StringGet(string key);

        /// <summary>
        /// 获取一个key的对象
        /// </summary>
        /// <typeparam name="T">保存对象的类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        T StringGet<T>(string key);

        /// <summary>
        /// 保存多条记录
        /// </summary>
        /// <param name="pairs">键值对</param>
        /// <returns></returns>
        bool StringSet(Dictionary<string, string> pairs);

        /// <summary>
        /// 保存一条记录
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间（多久之后过期）</param>
        /// <returns></returns>
        bool StringSet(string key, string value, TimeSpan? expiry = null);

        /// <summary>
        /// 保存一条记录
        /// </summary>
        /// <typeparam name="T">保存对象的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间（多久之后过期）</param>
        /// <returns></returns>
        bool StringSet<T>(string key, T value, TimeSpan? expiry = null);

        /// <summary>
        /// 删除ZSet集合中匹配的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        bool ZSetDelete(string key, string value);

        /// <summary>
        /// 删除ZSet集合中匹配的值
        /// </summary>
        /// <typeparam name="T">集合保存的值的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        bool ZSetDelete<T>(string key, string value);

        /// <summary>
        /// 获取ZSet集合中所有匹配的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="pattern">模式匹配（正则）</param>
        /// <param name="pageSize">匹配值的数量</param>
        /// <returns></returns>
        IDictionary<string, double> ZSetGet(string key, string pattern, int pageSize);

        /// <summary>
        /// 获取ZSet集合中所有匹配的值
        /// </summary>
        /// <typeparam name="T">集合保存的值的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="pattern">模式匹配（正则）</param>
        /// <param name="pageSize">匹配值的数量</param>
        /// <returns></returns>
        IDictionary<T, double> ZSetGet<T>(string key, string pattern, int pageSize);
        
        /// <summary>
        /// 向ZSet集合中添加值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="score">权重</param>
        /// <returns></returns>
        bool ZSetInsert(string key, string value, double score);

        /// <summary>
        /// 向ZSet集合中添加值
        /// </summary>
        /// <typeparam name="T">集合保存的值的类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="score">权重</param>
        /// <returns></returns>
        bool ZSetInsert<T>(string key, T value, double score);

        /// <summary>
        /// 计算ZSet集合的大小
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        long ZSetLength(string key);
    }
}
