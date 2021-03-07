using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/3/7 17:11:46；更新时间：
************************************************************************************/
namespace VirtualUniverse.Configuration
{
    /// <summary>
    /// 类说明：web主机监听地址
    /// </summary>
    public static class WebHostListenUrl
    {
        /// <summary>
        /// 从appsettings.json文件读取url
        /// </summary>
        /// <param name="urlKey">监听地址键名</param>
        /// <param name="optional">可选</param>
        /// <param name="reloadOnChange">改变时重新加载</param>
        /// <returns></returns>
        public static string ReadUrlFromAppsettingsJson(string urlKey, bool optional = true, bool reloadOnChange = true)
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            var configuration = configurationBuilder.AddJsonFile("appsettings.json", optional, reloadOnChange)
                .Build();
            return configuration.GetValue<string>(urlKey);
        }

        /// <summary>
        /// 从appsettings.json文件读取urls
        /// </summary>
        /// <param name="urlsKey">监听地址键名</param>
        /// <param name="optional">可选</param>
        /// <param name="reloadOnChange">改变时重新加载</param>
        /// <returns></returns>
        public static IList<string> ReadUrlsFromAppsettingsJson(string urlsKey, bool optional = true, bool reloadOnChange = true)
        {
            var urls = new List<string>();
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            var configuration = configurationBuilder.AddJsonFile("appsettings.json", optional, reloadOnChange)
                .Build();
            var index = 0;
            while (true)
            {
                var url = configuration.GetValue<string>(urlsKey);
                if (string.IsNullOrWhiteSpace(url))
                {
                    break;
                }
                else
                {
                    urls.Add(url);
                    index++;
                }
            }
            return urls;
        }
    }
}
