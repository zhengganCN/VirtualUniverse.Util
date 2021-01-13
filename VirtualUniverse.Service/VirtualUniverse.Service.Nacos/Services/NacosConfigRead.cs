using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nacos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using VirtualUniverse.Service.Nacos.Models;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/4 9:21:57；更新时间：
************************************************************************************/
namespace VirtualUniverse.Service.Nacos.Services
{
    /// <summary>
    /// 类 描 述：Nacos配置读取
    /// </summary>
    public class NacosConfigRead : IDisposable
    {
        private readonly string _nacosAddress;
        private readonly string _environment;
        private readonly NacosConfigOptions _nacosConfigOptions = new NacosConfigOptions();
        private bool disposedValue;
        private readonly IServiceCollection _services = new ServiceCollection();
        private ServiceProvider _serviceProvider { get; set; }
        private readonly ILogger<NacosConfigRead> _logger = new LoggerFactory().CreateLogger<NacosConfigRead>();
        private string ConfigString { get; set; }
        private bool AlreadyLoadConfig { get; set; } = false;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="consulAddress">Consul地址</param>
        /// <param name="options">读取配置的设置</param>
        public NacosConfigRead(string nacosAddress, string environment, Action<NacosConfigOptions> options)
        {
            _nacosAddress = nacosAddress;
            _environment = environment;
            options.Invoke(_nacosConfigOptions);
            Init();
        }

        private void Init()
        {
            if (_nacosConfigOptions.LoadLocalConfig)
            {
                return;
            }
            _services.AddNacos(configure =>
            {
                configure.DefaultTimeOut = _nacosConfigOptions.NacosOptions.DefaultTimeOut;
                configure.ServerAddresses = new List<string> { _nacosAddress };
                configure.AccessKey = _nacosConfigOptions.NacosOptions.AccessKey;
                configure.SecretKey = _nacosConfigOptions.NacosOptions.SecretKey;
                configure.Namespace = _nacosConfigOptions.NacosOptions.Namespace;
                configure.UserName = _nacosConfigOptions.NacosOptions.UserName;
                configure.Password = _nacosConfigOptions.NacosOptions.Password;
                configure.EndPoint = _nacosConfigOptions.NacosOptions.EndPoint;
            });
            _serviceProvider = _services.BuildServiceProvider();
            IntervalRead();
        }

        private void IntervalRead()
        {
            while (true)
            {
                try
                {
                    if (_nacosConfigOptions.ListenConfig)
                    {
                        ReadConfigAndSaveToConfig();
                    }
                    else if (!AlreadyLoadConfig)
                    {
                        ReadConfigAndSaveToConfig();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }
                Thread.Sleep(_nacosConfigOptions.Interval);
            }
        }
        /// <summary>
        /// 从Consul读取配置并把配置写入配置文件
        /// </summary>
        /// <param name="key">键</param>
        private void ReadConfigAndSaveToConfig()
        {
            var config = RequestNacosGetConfigAsync().Result;
            if (string.IsNullOrEmpty(ConfigString))
            {
                ConfigString = config;
                SaveConfig(config);
            }
            else
            {
                if (string.Compare(config, ConfigString) != 0)
                {
                    ConfigString = config;
                    SaveConfig(config);
                }
            }
        }

        private void SaveConfig(string config)
        {
            var fileName = _nacosConfigOptions.Environments[_environment].ConfigFileName;
            Stream stream;
            if (!File.Exists(fileName))
            {
                stream = File.Create(fileName);
            }
            else
            {
                stream = File.Open(fileName, FileMode.Create);
            }
            using var sw = new StreamWriter(stream);
            sw.Write(config);
            AlreadyLoadConfig = true;
        }

        private async Task<string> RequestNacosGetConfigAsync()
        {
            var nacosConfigClient = _serviceProvider.GetService<INacosConfigClient>();
            var configEnvironmentOptions = _nacosConfigOptions.Environments[_environment];
            var request = new GetConfigRequest
            {
                DataId = configEnvironmentOptions.DataId,
                Group = configEnvironmentOptions.Group,
                Tenant = configEnvironmentOptions.Tenant
            };
            return await nacosConfigClient.GetConfigAsync(request);
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
                    // 释放托管状态
                }
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
