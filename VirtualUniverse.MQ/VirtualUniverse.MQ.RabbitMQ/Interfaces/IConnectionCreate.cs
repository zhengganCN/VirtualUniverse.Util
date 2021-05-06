using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/4/16 15:13:18；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ.Interfaces
{
    /// <summary>
    /// 类 描 述：连接器创建接口
    /// </summary>
    public interface IConnectionCreate
    {
        void SetConnectionFactory(string hostName, string userName, string password, int port);
        /// <summary>
        /// 根据提供的主机名列表尝试创建一个可用连接
        /// </summary>
        /// <param name="hostnames">主机名列表</param>
        /// <param name="clientProvidedName">客户端提供的连接名</param>
        /// <param name="connectionId">连接器的唯一标识，如果为空，则随机生成一个id用于标识</param>
        /// <returns></returns>
        IConnection CreateConnection(IList<string> hostnames, string clientProvidedName,ref string connectionId);
        /// <summary>
        /// 根据提供的主机名列表尝试创建一个可用连接
        /// </summary>
        /// <param name="hostnames">主机名列表</param>
        /// <param name="connectionId">连接器的唯一标识，如果为空，则随机生成一个id用于标识</param>
        /// <returns></returns>
        IConnection CreateConnection(IList<string> hostnames, ref string connectionId);
        /// <summary>
        /// 根据初始化配置的主机名尝试创建一个可用连接
        /// </summary>
        /// <param name="clientProvidedName">客户端提供的连接名</param>
        /// <param name="connectionId">连接器的唯一标识，如果为空，则随机生成一个id用于标识</param>
        /// <returns></returns>
        IConnection CreateConnection(string clientProvidedName, ref string connectionId);
        /// <summary>
        /// 根据初始化配置的主机名尝试创建一个可用连接
        /// <param name="connectionId">连接器的唯一标识，如果为空，则随机生成一个id用于标识</param>
        /// </summary>
        /// <returns></returns>
        IConnection CreateConnection(ref string connectionId);
    }
}
