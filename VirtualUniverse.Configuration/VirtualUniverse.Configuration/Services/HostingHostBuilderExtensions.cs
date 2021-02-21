using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/2/21 15:48:36；更新时间：
************************************************************************************/
namespace VirtualUniverse.Configuration.Services
{
    /// <summary>
    /// 类说明：主机构建扩展类
    /// </summary>
    public static class HostingHostBuilderExtensions
    {
        /// <summary>
        /// 加载环境变量env的参数值,用以设置当前环境
        /// </summary>
        /// <param name="hostBuilder">hostBuilder</param>
        /// <returns></returns>
        public static IHostBuilder LoadCurrentEnvironmentFromEnvironmentVariable(this IHostBuilder hostBuilder)
        {
            var env = Environment.GetEnvironmentVariable("env");
            if (!string.IsNullOrWhiteSpace(env))
            {
                hostBuilder.UseEnvironment(env);
            }
            return hostBuilder;
        }

        /// <summary>
        /// 加载命令行参数env的参数值,用以设置当前环境
        /// </summary>
        /// <param name="hostBuilder">hostBuilder</param>
        /// <returns></returns>
        public static IHostBuilder LoadCurrentEnvironmentFromCommandLine(this IHostBuilder hostBuilder)
        {
            string env = default;
            _ = Environment.GetCommandLineArgs().LastOrDefault(entity =>
              {
                  var value = entity.Split('=');
                  if (value.Length == 2 && value[0] == "env" && !string.IsNullOrWhiteSpace(value[1]))
                  {
                      env = value[1];
                      return true;
                  }
                  else
                  {
                      return false;
                  }
              });
            if (!string.IsNullOrWhiteSpace(env))
            {
                hostBuilder.UseEnvironment(env);
            }
            return hostBuilder;
        }
    }
}
