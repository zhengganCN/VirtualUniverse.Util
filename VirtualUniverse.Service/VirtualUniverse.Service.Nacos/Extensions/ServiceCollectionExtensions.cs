using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using VirtualUniverse.Service.Nacos.Models;
using VirtualUniverse.Service.Nacos.Services;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/4 15:20:39；更新时间：
************************************************************************************/
namespace VirtualUniverse.Service.Nacos.Extensions
{
    /// <summary>
    /// 类 描 述：
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNacosConfigRead(this IServiceCollection services, string nacosAddress, string environment, Action<NacosConfigOptions> options)
        {
            Task.Run(() =>
            {
#pragma warning disable S1848 // Objects should not be created to be dropped immediately without being used
                new NacosConfigRead(nacosAddress, environment, options);
#pragma warning restore S1848 // Objects should not be created to be dropped immediately without being used
            });
            return services;
        }
    }
}
