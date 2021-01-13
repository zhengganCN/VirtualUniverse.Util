using Consul;
using System;
using System.IO;
using System.Text;
using System.Threading;
using VirtualUniverse.Configuration.Models;
using VirtualUniverse.Service.Consul.Models;

namespace VirtualUniverse.Service.Consul.Services
{
    /// <summary>
    /// Consul键值对读取扩展
    /// </summary>
    public class ConsulConfigRead : IDisposable
    {
        private readonly string consulAddress;
        private readonly ConsulConfigOptions consulConfigOptions = new ConsulConfigOptions();
        private bool disposedValue;
        private Timer timer;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="consulAddress">Consul地址</param>
        public ConsulConfigRead(string consulAddress)
        {
            this.consulAddress = consulAddress;
            IntervalRead();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="consulAddress">Consul地址</param>
        /// <param name="options">读取配置的设置</param>
        public ConsulConfigRead(string consulAddress, Action<ConsulConfigOptions> options)
        {
            this.consulAddress = consulAddress;
            options.Invoke(consulConfigOptions);
            IntervalRead();
        }

        private void IntervalRead()
        {
            timer = new Timer(ReadConfig, null, 1000, consulConfigOptions.Interval);
        }

        /// <summary>
        /// 从Consul读取配置
        /// </summary>
        public void ReadConfig(object value = null)
        {
            switch (consulConfigOptions.Environment)
            {
                case "dev":
                    ReadConfigAndSaveToConfig(GetEnvKeyName(EnumConfigEnvironment.Development));
                    break;
                case "test":
                    ReadConfigAndSaveToConfig(GetEnvKeyName(EnumConfigEnvironment.Test));
                    break;
                case "prod":
                    ReadConfigAndSaveToConfig(GetEnvKeyName(EnumConfigEnvironment.Production));
                    break;
                default:
                    ReadConfigAndSaveToConfig(GetEnvKeyName(EnumConfigEnvironment.Development));
                    break;
            }
        }

        /// <summary>
        /// 获取环境键名
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        private static string GetEnvKeyName(EnumConfigEnvironment environment)
        {
            return Enum.GetName(typeof(EnumConfigEnvironment), environment);
        }
        /// <summary>
        /// 读取配置并把配置写入配置文件
        /// </summary>
        /// <param name="key">键</param>
        private void ReadConfigAndSaveToConfig(string key)
        {
            var client = new ConsulClient(options =>
            {
                options.Address = new Uri(consulAddress);
            });
            var config = Encoding.UTF8.GetString(client.KV.Get($"{consulConfigOptions.ConfigDirectoryPath}{key}").Result.Response.Value);
            if (!string.IsNullOrWhiteSpace(config))
            {
                using var sw = new StreamWriter($"appsettings.{key}.json");
                sw.Write(config);
                WriterCurrentEnvironmentValue(key);
            }
        }

        /// <summary>
        /// 把当前环境值写入appsettings.json文件
        /// </summary>
        /// <param name="env"></param>
        private static void WriterCurrentEnvironmentValue(string env)
        {
            using var sw = new StreamWriter(ConstConfigPath.Environment);
            sw.Write($"{{\"CurrentEnvironment\" : \"{env}\"}}");
        }
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

                timer.Dispose();
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
