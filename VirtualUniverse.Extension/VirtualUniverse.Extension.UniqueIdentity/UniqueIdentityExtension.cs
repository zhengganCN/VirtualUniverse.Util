using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using VirtualUniverse.Extension.UniqueIdentity.Interfaces;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/3/11 13:36:46；更新时间：
************************************************************************************/
namespace VirtualUniverse.Extension.UniqueIdentity
{
    /// <summary>
    /// 类说明：唯一Id扩展类
    /// </summary>
    public static class UniqueIdentityExtension
    {
        internal const string UniqueIdentityHttpClientName = "VirtualUniverse.Extension.UniqueIdentity";
        internal static string Url { get; set; }
        public static IServiceCollection AddUniqueIdentity(this IServiceCollection services, string url)
        {
            Url = url;
            services.AddHttpClient(UniqueIdentityHttpClientName);
            services.AddSingleton<IUniqueIdentityProvider, UniqueIdentityProvider>();
            return services;
        }
    }
}
