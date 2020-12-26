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
using VirtualUniverse.Configuration.Models;

namespace VirtualUniverse.Configuration.Services
{
    /// <summary>
    /// Json配置文件操作类
    /// </summary>
    public class BaseJsonConfiguration : IDisposable
    {

        private readonly string environmentConfig = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConstConfigPath.Environment);//获取当前环境配置文件路径

        private readonly FileSystemWatcher environmentConfigWatcher = new FileSystemWatcher(); //监听当前环境配置文件路径
        private readonly FileSystemWatcher valuesConfigWatcher = new FileSystemWatcher(); //监听值内容配置文件路径
        private JsonConfigurationProvider environmentConfigProvider;
        private JsonConfigurationProvider valuesConfigProvider;

        private static byte[] data;

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseJsonConfiguration()
        {
            valuesConfigProvider = GetJsonConfiguration();
        }

        /// <summary>
        /// 默认读取配置文件函数，如需修改，则重写该函数
        /// </summary>
        /// <returns></returns>
        public virtual JsonConfigurationProvider GetJsonConfiguration()
        {
            LoadEnvironmentConfig();
            environmentConfigWatcher.Path = Path.GetDirectoryName(environmentConfig);
            environmentConfigWatcher.Filter = ConstConfigPath.Environment;
            environmentConfigWatcher.Changed += EnvironmentConfigWatcher_Changed;

            environmentConfigProvider.TryGet(ConstConfigPath.CurrentEnvironment, out string environment);
            if (string.IsNullOrWhiteSpace(environment))
            {
                throw new ArgumentNullException($"在配置文件{ConstConfigPath.Environment}中不存在{ConstConfigPath.CurrentEnvironment}节点");
            }
            string valuesConfigPath = GetValuesConfigPath(environment);
            valuesConfigWatcher.Path = Path.GetDirectoryName(valuesConfigPath);
            valuesConfigWatcher.Filter = ConstConfigPath.ValueConfigFilter;
            valuesConfigWatcher.Changed += ValuesConfigWatcher_Changed;
            return LoadValuesConfig(valuesConfigPath);
        }

        private void ValuesConfigWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            environmentConfigProvider.TryGet("CurrentEnvironment", out string environment);
            string valuesConfigPath = GetValuesConfigPath(environment);
            valuesConfigProvider = LoadValuesConfig(valuesConfigPath);
        }

        private static string GetValuesConfigPath(string environment)
        {
            string valuesConfigPath = environment switch
            {
                nameof(EnumConfigEnvironment.Development) => Path.Combine(Directory.GetCurrentDirectory(), ConstConfigPath.Development),
                nameof(EnumConfigEnvironment.Test) => Path.Combine(Directory.GetCurrentDirectory(), ConstConfigPath.Test),
                nameof(EnumConfigEnvironment.Production) => Path.Combine(Directory.GetCurrentDirectory(), ConstConfigPath.Production),
                _ => throw new ArgumentNullException($"找不到任意一个文件名为{ConstConfigPath.Development}或{ConstConfigPath.Test}或{ConstConfigPath.Production}配置文件"),
            };
            return valuesConfigPath;
        }

        private void EnvironmentConfigWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            LoadEnvironmentConfig();
        }

        private void LoadEnvironmentConfig()
        {
            environmentConfigProvider = new JsonConfigurationProvider(new JsonConfigurationSource());
            using var fs = new FileStream(environmentConfig, FileMode.Open, FileAccess.Read);
            environmentConfigProvider.Load(fs);
        }
        private JsonConfigurationProvider LoadValuesConfig(string valuesConfigPath)
        {
            var valuesConfigProvider = new JsonConfigurationProvider(new JsonConfigurationSource());
            using var fs = new FileStream(valuesConfigPath, FileMode.Open, FileAccess.Read);
            valuesConfigProvider.Load(fs);
            return valuesConfigProvider;
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
                throw new ArgumentNullException(nameof(key));
            }
            var hasValue = valuesConfigProvider.TryGet(key, out string value);
            return hasValue ? value : "";
        }

        /// <summary>
        /// 把整个json文件转换成实例对象
        /// </summary>
        /// <typeparam name="T">json文件实例化后的类型</typeparam>
        /// <returns></returns>
        public T GetObject<T>()
        {
            var json = Encoding.UTF8.GetString(data);
            return (T)JsonConvert.DeserializeObject(json, typeof(T));
        }

        /// <summary>
        /// 根据key设置value
        /// 无效方法，暂未实现
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetValue(string key, string value)
        {
            valuesConfigProvider.Set(key, value);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            valuesConfigProvider.Dispose();
        }
    }
}
