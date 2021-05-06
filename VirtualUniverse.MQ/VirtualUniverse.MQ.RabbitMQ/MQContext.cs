using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using VirtualUniverse.MQ.RabbitMQ.Interfaces;
using VirtualUniverse.MQ.RabbitMQ.Models;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/4 20:32:55；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ
{
    /// <summary>
    /// 类说明：MQ上下文
    /// </summary>
    public abstract class MQContext : IMQContext, IDisposable
    {
        private IConnectionCreate ConnectionCreate { get; set; } = new ConnectionCreate();

        private static readonly MQContextOptionsBuilder builder = new MQContextOptionsBuilder();

        private bool disposedValue;
        /// <summary>
        /// 构造器
        /// </summary>
        protected MQContext()
        {
            InitConfiguration();
        }

        private void InitConfiguration()
        {
            OnConfiguration(builder);
            string connectionId = default;
            CreateConnection(ref connectionId);
            string channelId = default;
            CreateChannel(connectionId, ref channelId);
            foreach (var exchange in MQContextOptionsBuilder.Exchanges.ToImmutableDictionary())
            {
                ExchangeDeclare(channelId, exchange.Value);
            }
            foreach (var queue in MQContextOptionsBuilder.Queues.ToImmutableDictionary())
            {
                QueueDeclare(channelId, queue.Value);
            }

        }

        /// <summary>
        /// 配置MQ
        /// </summary>
        /// <param name="builder"></param>
        public abstract void OnConfiguration(MQContextOptionsBuilder builder);

        #region  连接器创建
        public IConnection CreateConnection(IList<string> hostnames, string clientProvidedName, ref string connectionId)
        {
            ConnectionCreate.SetConnectionFactory(builder.HostName, builder.UserName, builder.Password, builder.Port);
            return ConnectionCreate.CreateConnection(hostnames, clientProvidedName, ref connectionId);
        }

        public IConnection CreateConnection(IList<string> hostnames, ref string connectionId)
        {
            ConnectionCreate.SetConnectionFactory(builder.HostName, builder.UserName, builder.Password, builder.Port);
            return ConnectionCreate.CreateConnection(hostnames, ref connectionId);
        }

        public IConnection CreateConnection(string clientProvidedName, ref string connectionId)
        {
            ConnectionCreate.SetConnectionFactory(builder.HostName, builder.UserName, builder.Password, builder.Port);
            return ConnectionCreate.CreateConnection(clientProvidedName, ref connectionId);
        }

        public IConnection CreateConnection(ref string connectionId)
        {
            ConnectionCreate.SetConnectionFactory(builder.HostName, builder.UserName, builder.Password, builder.Port);
            return ConnectionCreate.CreateConnection(ref connectionId);
        }
        #endregion

        #region 通道创建
        public IModel CreateChannel(string connectionId,ref string channelId)
        {
            IChannelCreate channelCreate = new ChannelCreate();
            return channelCreate.CreateChannel(connectionId,ref channelId);
        }
        #endregion

        #region 消息发送
        public void PublishMessage(PublishMessageConfig publishMessageConfig, ReadOnlyMemory<byte> body)
        {
            IMessageOperation messageOperation = new MessageOperation();
            messageOperation.PublishMessage(publishMessageConfig, body);
        }
        /// <summary>
        /// 消费消息
        /// </summary>
        /// <param name="channelId">通道id</param>
        /// <param name="queue"></param>
        /// <param name="autoAck"></param>
        /// <param name="consumerTag"></param>
        /// <param name="noLocal"></param>
        /// <param name="exclusive">排它性</param>
        /// <param name="arguments">参数</param>
        /// <param name="consumer">消费者<see cref="EventingBasicConsumer"/>或<see cref="DefaultBasicConsumer"/></param>
        public string ConsumeMessage(ConsumeMessageConfig consumeMessageConfig)
        {
            IMessageOperation messageOperation = new MessageOperation();
            return messageOperation.ConsumeMessage(consumeMessageConfig);
        }
        #endregion

        #region 交换机操作
        public void ExchangeBind(string channelId, string destination, string source, string routingKey, IDictionary<string, object> arguments)
        {
            IExchangeCreate exchangeCreate = new ExchangeCreate(channelId);
            exchangeCreate.ExchangeBind(destination, source, routingKey, arguments);
        }

        public void ExchangeBindNoWait(string channelId, string destination, string source, string routingKey, IDictionary<string, object> arguments)
        {
            IExchangeCreate exchangeCreate = new ExchangeCreate(channelId);
            exchangeCreate.ExchangeBindNoWait(destination, source, routingKey, arguments);
        }

        public void ExchangeDeclare(string channelId, ExchangeConfig exchangeConfig)
        {
            IExchangeCreate exchangeCreate = new ExchangeCreate(channelId);
            exchangeCreate.ExchangeDeclare(exchangeConfig);
        }

        public void ExchangeDeclareNoWait(string channelId, ExchangeConfig exchangeConfig)
        {
            IExchangeCreate exchangeCreate = new ExchangeCreate(channelId);
            exchangeCreate.ExchangeDeclareNoWait(exchangeConfig);
        }

        public void ExchangeDeclarePassive(string channelId, string exchange)
        {
            IExchangeCreate exchangeCreate = new ExchangeCreate(channelId);
            exchangeCreate.ExchangeDeclarePassive(exchange);
        }

        public void ExchangeDelete(string channelId, string exchange, bool ifUnused)
        {
            IExchangeCreate exchangeCreate = new ExchangeCreate(channelId);
            exchangeCreate.ExchangeDelete(exchange, ifUnused);
        }

        public void ExchangeDeleteNoWait(string channelId, string exchange, bool ifUnused)
        {
            IExchangeCreate exchangeCreate = new ExchangeCreate(channelId);
            exchangeCreate.ExchangeDeleteNoWait(exchange, ifUnused);
        }

        public void ExchangeUnbind(string channelId, string destination, string source, string routingKey, IDictionary<string, object> arguments)
        {
            IExchangeCreate exchangeCreate = new ExchangeCreate(channelId);
            exchangeCreate.ExchangeUnbind(destination, source, routingKey, arguments);
        }

        public void ExchangeUnbindNoWait(string channelId, string destination, string source, string routingKey, IDictionary<string, object> arguments)
        {
            IExchangeCreate exchangeCreate = new ExchangeCreate(channelId);
            exchangeCreate.ExchangeUnbindNoWait(destination, source, routingKey, arguments);
        }
        #endregion

        #region 队列操作
        public void QueueBind(string channelId,string queue, string exchange, string routingKey, IDictionary<string, object> arguments)
        {
            IQueueCreate queueCreate = new QueueCreate(channelId);
            queueCreate.QueueBind(queue, exchange, routingKey, arguments);
        }

        public void QueueBindNoWait(string channelId, string queue, string exchange, string routingKey, IDictionary<string, object> arguments)
        {
            IQueueCreate queueCreate = new QueueCreate(channelId);
            queueCreate.QueueBindNoWait(queue, exchange, routingKey, arguments);
        }

        public QueueDeclareOk QueueDeclare(string channelId, QueueDeclareConfig queueDeclare)
        {
            IQueueCreate queueCreate = new QueueCreate(channelId);
          return  queueCreate.QueueDeclare(queueDeclare);
        }

        public void QueueDeclareNoWait(string channelId, QueueDeclareConfig queueDeclare)
        {
            IQueueCreate queueCreate = new QueueCreate(channelId);
            queueCreate.QueueDeclareNoWait(queueDeclare);
        }

        public QueueDeclareOk QueueDeclarePassive(string channelId, string queue)
        {
            IQueueCreate queueCreate = new QueueCreate(channelId);
            return queueCreate.QueueDeclarePassive(queue);
        }

        public uint QueueDelete(string channelId, string queue, bool ifUnused, bool ifEmpty)
        {
            IQueueCreate queueCreate = new QueueCreate(channelId);
            return queueCreate.QueueDelete(queue,ifUnused,ifEmpty);
        }

        public void QueueDeleteNoWait(string channelId, string queue, bool ifUnused, bool ifEmpty)
        {
            IQueueCreate queueCreate = new QueueCreate(channelId);
            queueCreate.QueueDeleteNoWait(queue, ifUnused, ifEmpty);
        }

        public uint QueuePurge(string channelId, string queue)
        {
            IQueueCreate queueCreate = new QueueCreate(channelId);
            return queueCreate.QueuePurge(queue);
        }

        public void QueueUnbind(string channelId, string queue, string exchange, string routingKey, IDictionary<string, object> arguments)
        {
            IQueueCreate queueCreate = new QueueCreate(channelId);
            queueCreate.QueueUnbind(queue,exchange,routingKey,arguments);
        }
        #endregion 

        #region 销毁
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                }

                // TODO: 释放未托管的资源(未托管的对象)并替代终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
