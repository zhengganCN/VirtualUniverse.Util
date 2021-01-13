using System.Collections.Generic;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/4 20:58:59；更新时间：
************************************************************************************/
namespace VirtualUniverse.Service.RabbitMQ.Models
{
    /// <summary>
    /// 类说明：
    /// </summary>
    public class MQContextOptionsBuilder
    {
        internal string UserName { get; set; }
        internal string Password { get; set; }
        internal string HostName { get; set; }
        internal int Port { get; set; }
        internal static bool IsExchangesCreated { get; set; } = false;
        internal static HashSet<MQExchange> Exchanges { get; set; } = new HashSet<MQExchange>();
        internal static bool IsQueuesCreated { get; set; } = false;
        internal static HashSet<MQQueue> Queues { get; set; } = new HashSet<MQQueue>();
        /// <summary>
        /// 设置连接配置
        /// </summary>
        /// <param name="hostName">主机地址</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="port">端口</param>
        /// <returns></returns>
        public MQContextOptionsBuilder SetConnection(string hostName, string userName, string password, int port = 5672)
        {
            HostName = hostName;
            Port = port;
            Password = password;
            UserName = userName;
            return this;
        }

        /// <summary>
        /// 添加交换机
        /// </summary>
        /// <returns></returns>
        public MQContextOptionsBuilder AddExchange(HashSet<MQExchange> exchanges)
        {
            foreach (var exchange in exchanges)
            {
                if (Exchanges.Add(exchange))
                {
                    IsExchangesCreated = false;
                }
            }
            return this;
        }

        /// <summary>
        /// 添加队列
        /// </summary>
        /// <returns></returns>
        public MQContextOptionsBuilder AddQueue(HashSet<MQQueue> queues)
        {
            Queues = queues;
            foreach (var queue in queues)
            {
                if (Queues.Add(queue))
                {
                    IsQueuesCreated = false;
                }
            }
            return this;
        }
    }
}
