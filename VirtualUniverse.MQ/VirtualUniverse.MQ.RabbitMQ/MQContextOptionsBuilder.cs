using RabbitMQ.Client;
using System.Collections.Generic;
using System.Collections.Immutable;
using VirtualUniverse.MQ.RabbitMQ.Models;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/4 20:58:59；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ
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

        #region 连接器
        /// <summary>
        /// 连接器
        /// </summary>
        internal static ImmutableDictionary<string, IConnection> Connections { get;private set; } = ImmutableDictionary.Create<string, IConnection>();
        /// <summary>
        /// 添加连接器
        /// </summary>
        /// <param name="connectionId">连接器Id</param>
        /// <param name="connection">连接器</param>
        internal static void AddConnection(string connectionId,IConnection connection)
        {
            Connections = Connections.Add(connectionId, connection);
        }
        /// <summary>
        /// 删除连接器
        /// </summary>
        /// <param name="connectionId">连接器Id</param>
        internal static void DeleteConnection(string connectionId)
        {
            Connections = Connections.Remove(connectionId);
        }
        #endregion

        #region 通道和连接器之间的关系
        /// <summary>
        /// 通道和连接器之间的关系，key为通道Id，值为连接器Id
        /// </summary>
        internal static ImmutableDictionary<string, string> ChannelConnectionRelations { get; private set; } = ImmutableDictionary.Create<string, string>();

        /// <summary>
        /// 添加通道和连接器之间的关系
        /// </summary>
        /// <param name="channelId">通道Id</param>
        /// <param name="connectionId">连接器Id</param>
        internal static void AddChannelConnectionRelation(string channelId, string connectionId)
        {
            ChannelConnectionRelations = ChannelConnectionRelations.Add(connectionId, channelId);
        }
        #endregion

        #region 通道
        /// <summary>
        /// 通道
        /// </summary>
        internal static ImmutableDictionary<string, IModel> Channels { get; private set; } = ImmutableDictionary.Create<string, IModel>();
        /// <summary>
        /// 添加通道
        /// </summary>
        /// <param name="channelId">通道Id</param>
        /// <param name="channel"></param>
        internal static void AddChannel(string channelId, IModel channel)
        {
            Channels = Channels.Add(channelId, channel);
        }
        #endregion

        #region 交换器

        /// <summary>
        /// 交换器
        /// </summary>
        internal static Dictionary<string, ExchangeConfig> Exchanges { get; private set; } = new Dictionary<string,ExchangeConfig>();

        private static readonly object ExchangeLock = new object();
        /// <summary>
        /// 添加交换器
        /// </summary>
        /// <param name="exchange">交换机</param>
        internal static void AddExchange(ExchangeConfig exchange)
        {
            lock (ExchangeLock)
            {
                if (Exchanges.TryGetValue(exchange.Exchange, out ExchangeConfig exchangeConfig))
                {
                    if (exchange.Declare && exchange != exchangeConfig)
                    {
                        Exchanges[exchange.Exchange] = exchange;
                    }
                }
                else
                {
                    Exchanges.Add(exchange.Exchange, exchange);
                }
            }
        }
        #endregion

        //#region 交换器和通道的关系
        ///// <summary>
        ///// 交换器和通道的关系，key为交换器Id，值为通道Id
        ///// </summary>
        //internal static ImmutableDictionary<string,string> ExchangeChannelRelations { get; private set; } = ImmutableDictionary.Create<string, string>();
        ///// <summary>
        ///// 添加通道
        ///// </summary>
        ///// <param name="exchangeId">交换器Id</param>
        ///// <param name="channelId">通道Id</param>
        //public static void AddExchangeChannelRelation(string exchangeId, string channelId)
        //{
        //    ExchangeChannelRelations = ExchangeChannelRelations.Add(exchangeId, channelId);
        //}
        //#endregion

        #region 队列
        /// <summary>
        /// 队列
        /// </summary>
        internal static Dictionary<string,QueueDeclareConfig> Queues { get; private set; } =new Dictionary<string, QueueDeclareConfig>();

        /// <summary>
        /// 删除队列
        /// </summary>
        /// <param name="queue">队列</param>
        internal static void DeleteQueue(string queue)
        {
            Queues.Remove(queue);
        }

        private static readonly object QueueLock = new object();
        /// <summary>
        /// 添加队列
        /// </summary>
        /// <param name="queue">队列</param>
        internal static void AddQueue(QueueDeclareConfig queue)
        {
            lock (QueueLock)
            {
                if (Queues.TryGetValue(queue.Queue, out QueueDeclareConfig queueDeclareConfig))
                {
                    if (queue.Declare && queue != queueDeclareConfig)
                    {
                        Queues[queue.Queue] = queue;
                    }
                }
                else
                {
                    Queues.Add(queue.Queue, queue);
                }
            }
        }
        #endregion

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
        /// <param name="exchange"></param>
        /// <returns></returns>
        public MQContextOptionsBuilder AddExchangeConfig(ExchangeConfig exchange)
        {
            AddExchange(exchange);
            return this;
        }

        /// <summary>
        /// 添加队列
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="durable"></param>
        /// <param name="exclusive"></param>
        /// <param name="autoDelete"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public MQContextOptionsBuilder AddQueueDeclareConfig(QueueDeclareConfig queueDeclare)
        {
            AddQueue(queueDeclare);
            return this;
        }

    }
}
