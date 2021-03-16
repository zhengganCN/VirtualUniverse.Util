using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/3/11 14:12:14；更新时间：
************************************************************************************/
namespace VirtualUniverse.Extension.UniqueIdentity.Interfaces
{
    /// <summary>
    /// 类 描 述：唯一Id提供者接口
    /// </summary>
    public interface IUniqueIdentityProvider
    {
        /// <summary>
        /// 异步获取唯一Id
        /// </summary>
        /// <returns></returns>
        Task<string> GetUniqueIdentityAsync();
    }
}
