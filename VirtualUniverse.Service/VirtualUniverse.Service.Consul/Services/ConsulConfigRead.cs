using Consul;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VirtualUniverse.Configuration.Models;

namespace VirtualUniverse.Service.Consul.Services
{
    /// <summary>
    /// Consul键值对读取扩展
    /// </summary>
    public static class ConsulConfigRead
    {
        /// <summary>
        /// 从Consul读取配置
        /// <br></br>
        /// “prod”读取键为Production的值
        /// <br></br>
        /// “dev”读取键为Development的值
        /// <br></br>
        /// “test”读取键为Test的值
        /// </summary>
        /// <param name="consulAddress"></param>
        /// <param name="env">环境值，只能是“prod”，“dev”，“test”这三个值之一，如果env的值与上述的任意一个不匹配，则env默认为“dev”</param>
        /// <param name="dirPath">配置目录路径，如果有，则必须以“/”结尾</param>
        public static void ReadConfig(string consulAddress, string env, string dirPath = null)
        {
            if (env == "dev")
            {
                ReadConfigAndSaveToConfig(consulAddress, GetEnvKeyName(EnumConfigEnvironment.Development), dirPath);
            }
            else if (env == "test")
            {
                ReadConfigAndSaveToConfig(consulAddress, GetEnvKeyName(EnumConfigEnvironment.Test), dirPath);
            }
            else if (env == "prod")
            {
                ReadConfigAndSaveToConfig(consulAddress, GetEnvKeyName(EnumConfigEnvironment.Production), dirPath);
            }
            else
            {
                ReadConfigAndSaveToConfig(consulAddress, GetEnvKeyName(EnumConfigEnvironment.Development), dirPath);
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
        /// <param name="consulAddress"></param>
        /// <param name="env"></param>
        /// <param name="dirPath"></param>
        private static void ReadConfigAndSaveToConfig(string consulAddress, string env, string dirPath)
        {
            var client = new ConsulClient(options =>
            {
                options.Address = new Uri(consulAddress);
            });
            var config = Encoding.UTF8.GetString(client.KV.Get($"{dirPath}{env}").Result.Response.Value);
            if (!string.IsNullOrWhiteSpace(config))
            {
                using var sw = new StreamWriter($"appsettings.{env}.json");
                sw.Write(config);
                WriterCurrentEnvironmentValue(env);
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
    }
}
