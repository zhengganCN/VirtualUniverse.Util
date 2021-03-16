using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VirtualUniverse.Extension.UniqueIdentity.Interfaces;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/3/11 13:47:39；更新时间：
************************************************************************************/
namespace VirtualUniverse.Extension.UniqueIdentity
{
    /// <summary>
    /// 类说明：唯一Id提供者
    /// </summary>
    public class UniqueIdentityProvider : IUniqueIdentityProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UniqueIdentityProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        /// <summary>
        /// 异步获取唯一Id
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetUniqueIdentityAsync()
        {
            var httpClient = _httpClientFactory.CreateClient(UniqueIdentityExtension.UniqueIdentityHttpClientName);
            var unique = await httpClient.GetStringAsync(UniqueIdentityExtension.Url);
            return unique;
        }
    }
}
