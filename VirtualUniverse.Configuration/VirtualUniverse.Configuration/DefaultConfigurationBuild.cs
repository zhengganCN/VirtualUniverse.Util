using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/3/7 17:25:01；更新时间：
************************************************************************************/
namespace VirtualUniverse.Configuration
{
    /// <summary>
    /// 类说明：默认配置构建
    /// </summary>
    public static class DefaultConfigurationBuild
    {
        /// <summary>
        /// 获取appsettings.json配置
        /// </summary>
        /// <returns></returns>
        public static IConfigurationRoot GetDefaultConfiguration()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            return builder.AddJsonFile("appsettings.json", true, true).Build();
        }

        /// <summary>
        /// 获取指定路径的配置
        /// </summary>
        /// <param name="path">配置文件路径</param>
        /// <param name="optional">可选</param>
        /// <param name="reloadOnChange">改变时重新加载</param>
        /// <returns></returns>
        public static IConfigurationRoot GetConfiguration(string path,bool optional = true, bool reloadOnChange = true)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            return builder.AddJsonFile(path, optional, reloadOnChange).Build();
        }
    }
}
