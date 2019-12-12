using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using Newtonsoft.Json;

namespace Util.Configuration
{
    /// <summary>
    /// Json配置文件操作类
    /// </summary>
    public class JsonConfiguration
    {
        private readonly JsonConfigurationProvider jsonConfigurationProvider;
        private readonly byte[] data;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configFilePath">配置文件路径</param>
        public JsonConfiguration(string configFilePath)
        {
            var jsonProvider= new JsonConfigurationProvider(new JsonConfigurationSource());
            using (var memoryStream = new MemoryStream())
            {
                using var fs = new FileStream(configFilePath, FileMode.Open, FileAccess.Read);
                var buf = new byte[fs.Length];
                fs.Read(buf, 0, (int)fs.Length);
                data = buf;
                memoryStream.Write(buf, 0, buf.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);
                jsonProvider.Load(memoryStream);
            }
            jsonConfigurationProvider = jsonProvider;
        }

        /// <summary>
        /// 获取值，如果在json文件中没有key，则放回空字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException();
            }
            var hasValue= jsonConfigurationProvider.TryGet(key, out string value);
            return hasValue ? value : "";
        }

        /// <summary>
        /// 把整个json文件转换成实例对象
        /// </summary>
        /// <typeparam name="T">json文件实例化后的类型</typeparam>
        /// <returns></returns>
        public T GetObject<T>()
        {
            var json= Encoding.UTF8.GetString(data);
            return (T)JsonConvert.DeserializeObject(json, typeof(T));
        }

        /// <summary>
        /// 根据key设置value
        /// 无效方法，暂未实现
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetValue(string key,string value)
        {
            jsonConfigurationProvider.Set(key, value);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            jsonConfigurationProvider.Dispose();
        }
    }
}
