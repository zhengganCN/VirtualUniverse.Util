using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using VirtualUniverse.MQ.RabbitMQ.Models;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/4 20:32:55；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ
{
    /// <summary>
    /// 类说明：MQ上下文
    /// </summary>
    public abstract class MQContext
    {
        private static readonly MQContextOptionsBuilder builder = new MQContextOptionsBuilder();
        private static ConnectionFactory factory = new ConnectionFactory();
        private static IConnection Connection;
        private IModel Channel { get; set; }
        private static ImmutableHashSet<string> Exchanges { get; set; } = ImmutableHashSet.Create<string>();
        private static ImmutableDictionary<string, QueueDeclareOk> QueueDictionaries { get; set; } = ImmutableDictionary.Create<string, QueueDeclareOk>();
        private EventingBasicConsumer Consumer { get; set; }
        private bool IsConsumerEnventBinded { get; set; } = false;
        private bool AutoAsk { get; set; } = false;
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
            CreateConnectionFactory();
            CreateConnection();
            CreateChannel();
            CreateExchanges(MQContextOptionsBuilder.Exchanges);
            CreateQueues(MQContextOptionsBuilder.Queues);
        }
        /// <summary>
        /// 开启自动应答
        /// </summary>
        public void OpenAutoAsk()
        {
            AutoAsk = true;
        }

        private void CreateExchanges(HashSet<MQExchange> exchanges)
        {
            if (!MQContextOptionsBuilder.IsExchangesCreated)
            {
                foreach (var exchange in exchanges)
                {
                    CreateExchange(exchange.ExchangeName, exchange.ExchangeType);
                }
                MQContextOptionsBuilder.IsExchangesCreated = true;
            }
        }

        private void CreateQueues(HashSet<MQQueue> queues)
        {
            if (!MQContextOptionsBuilder.IsQueuesCreated)
            {
                foreach (var queue in queues)
                {
                    CreateQueue(queue.QueueName);
                    if (!string.IsNullOrEmpty(queue.ExchangeName) && !string.IsNullOrEmpty(queue.RoutingKey))
                    {
                        QueueBind(queue.QueueName, queue.ExchangeName, queue.RoutingKey);
                    }
                }
                MQContextOptionsBuilder.IsQueuesCreated = true;
            }
        }
        /// <summary>
        /// 配置MQ
        /// </summary>
        /// <param name="builder"></param>
        public abstract void OnConfiguration(MQContextOptionsBuilder builder);

        private static void CreateConnectionFactory()
        {
            if (!(builder.HostName == factory.HostName &&
                builder.UserName == factory.UserName &&
                builder.Password == factory.Password &&
                builder.Port == factory.Port))
            {
                //创建连接工厂
                factory = new ConnectionFactory
                {
                    UserName = builder.UserName,
                    Password = builder.Password,
                    HostName = builder.HostName,
                    Port = builder.Port
                };
            }
        }

        private static void CreateConnection()
        {
            if (Connection == null || !Connection.IsOpen)
            {
                //创建连接
                Connection = factory.CreateConnection();
            }
        }
        private void CreateChannel()
        {
            if (Channel == null || !Channel.IsOpen)
            {
                //创建通道
                Channel = Connection.CreateModel();
                Channel.CreateBasicProperties().DeliveryMode = 2;
                Channel.BasicQos(0, 1, false);
            }
        }

        private void CreateQueue(string queueName)
        {
            //声明一个队列
            if (!QueueDictionaries.ContainsKey(queueName))
            {
                var queue = Channel.QueueDeclare(queueName, false, false, false, null);
                QueueDictionaries = QueueDictionaries.Add(queueName, queue);
            }
        }
        /// <summary>
        /// 使用MQ默认的Direct Exchange发送消息
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="message"></param>
        public void PublishMessage(string queueName, byte[] message)
        {
            CreateChannel();
            CreateQueue(queueName);
            Channel.BasicPublish(string.Empty, queueName, null, message);
        }
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="exchangeName">交换机名称</param>
        /// <param name="routeKey">路由键</param>
        /// <param name="exchangeType">交换机类型</param>
        /// <param name="message">消息</param>
        public void PublishMessage(string exchangeName, string routeKey, EnumExchangeType exchangeType, byte[] message)
        {
            CreateChannel();
            CreateExchange(exchangeName, exchangeType);
            Channel.BasicPublish(exchangeName, routeKey, null, message);
        }

        /// <summary>
        /// 创建交换机
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <param name="exchangeType"></param>
        private void CreateExchange(string exchangeName, EnumExchangeType exchangeType)
        {
            switch (exchangeType)
            {
                case EnumExchangeType.Direct:
                    CreateExchange(exchangeName, ExchangeType.Direct);
                    break;
                case EnumExchangeType.Fanout:
                    CreateExchange(exchangeName, ExchangeType.Fanout);
                    break;
                case EnumExchangeType.Headers:
                    CreateExchange(exchangeName, ExchangeType.Headers);
                    break;
                case EnumExchangeType.Topic:
                    CreateExchange(exchangeName, ExchangeType.Topic);
                    break;
                default:
                    break;
            }
        }

        private void CreateExchange(string exchangeName, string exchangeType)
        {
            if (!Exchanges.TryGetValue(exchangeName, out _))
            {
                Channel.ExchangeDeclare(exchangeName, exchangeType, false, false, null);
            }
        }

        /// <summary>
        /// 消费消息
        /// </summary>
        /// <param name="queueName">队列名称</param>
        /// <param name="action">处理消息</param>
        public void ConsumeMessage(string queueName, Func<byte[], bool> action)
        {
            CreateQueue(queueName);
            CreateCunsume();
            if (!IsConsumerEnventBinded)
            {
                //接收到消息事件
                Consumer.Received += (ch, ea) =>
                {
                    HandleMessage(action, ea);
                };
                //启动消费者 设置为手动应答消息
                Channel.BasicConsume(queueName, AutoAsk, Consumer);
            }
        }

        /// <summary>
        /// 消费消息
        /// </summary>
        /// <param name="queueName">队列名称</param>
        /// <param name="exchangeName">交换机名称</param>
        /// <param name="routingKey">路由键</param>
        /// <param name="action">处理消息</param>
        public void ConsumeMessage(string queueName, string exchangeName, string routingKey, Func<byte[], bool> action)
        {
            CreateQueue(queueName);
            QueueBind(queueName, exchangeName, routingKey);
            CreateCunsume();
            if (!IsConsumerEnventBinded)
            {
                //接收到消息事件
                Consumer.Received += (ch, ea) =>
                {
                    HandleMessage(action, ea);
                };
                //启动消费者 设置为手动应答消息
                Channel.BasicConsume(queueName, AutoAsk, Consumer);
            }
        }

        private void HandleMessage(Func<byte[], bool> action, BasicDeliverEventArgs ea)
        {
            if (AutoAsk)
            {
                action.Invoke(ea.Body.ToArray());
            }
            else
            {
                var messageDealResult = action.Invoke(ea.Body.ToArray());//消息处理结果
                if (messageDealResult)
                {
                    //确认该消息已被消费
                    Channel.BasicAck(ea.DeliveryTag, false);
                }
            }
        }

        private void QueueBind(string queueName, string exchangeName, string routingKey)
        {
            Channel.QueueBind(queueName, exchangeName, routingKey);
        }

        /// <summary>
        /// 事件基本消费者
        /// </summary>
        private void CreateCunsume()
        {
            while (Consumer is null)
            {
                Consumer = new EventingBasicConsumer(Channel);
            }
        }
        /// <summary>
        /// 清理
        /// </summary>
        /// <param name="disposing"></param>
#pragma warning disable S2953 // Methods named "Dispose" should implement "IDisposable.Dispose"
        private void Dispose(bool disposing)
#pragma warning restore S2953 // Methods named "Dispose" should implement "IDisposable.Dispose"
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // 释放托管状态(托管对象)
                }
                Channel.Dispose();
                disposedValue = true;
            }
        }
        /// <summary>
        /// 清理
        /// </summary>
#pragma warning disable S2953 // Methods named "Dispose" should implement "IDisposable.Dispose"
        public void Dispose()
#pragma warning restore S2953 // Methods named "Dispose" should implement "IDisposable.Dispose"
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        /// </summary>
        ~MQContext()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: false);
        }
    }
}
