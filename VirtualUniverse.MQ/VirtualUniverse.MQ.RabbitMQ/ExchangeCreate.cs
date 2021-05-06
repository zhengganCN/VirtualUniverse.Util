using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VirtualUniverse.MQ.RabbitMQ.Interfaces;
using VirtualUniverse.MQ.RabbitMQ.Models;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/4/16 16:58:35；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ
{
    /// <summary>
    /// 类 描 述：交换机操作
    /// </summary>
    class ExchangeCreate : IExchangeCreate
    {
        private readonly string _channelId;
        private readonly IModel _channel;
        public ExchangeCreate(string channelId)
        {
            _channelId = channelId;
            _channel = GetChannel(channelId);
        }

        private IModel GetChannel(string channelId)
        {
            if (MQContextOptionsBuilder.Channels.TryGetValue(channelId, out IModel channel))
            {
                return channel;
            }
            else
            {
                throw new ArgumentException($"通道{channelId}不存在");
            }
        }

        private bool IsExistExchange(string exchange, out ExchangeConfig exchangeConfig)
        {
            if (MQContextOptionsBuilder.Exchanges.TryGetValue(exchange, out exchangeConfig))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsChannelOpen(IModel channel)
        {
            return channel.IsOpen;
        }


        /// <summary>
        /// 绑定两个交换机
        /// </summary>
        /// <param name="destination">目标交换机</param>
        /// <param name="source">源交换机</param>
        /// <param name="routingKey">必须少于255个字节</param>
        /// <param name="arguments">参数</param>
        public void ExchangeBind(string destination, string source, string routingKey, IDictionary<string, object> arguments)
        {
            _channel.ExchangeBind(destination, source, routingKey, arguments);
        }

        /// <summary>
        /// 绑定两个交换机，但是把nowait设置为true
        /// </summary>
        /// <param name="destination">目标交换机</param>
        /// <param name="source">源交换机</param>
        /// <param name="routingKey">必须少于255个字节</param>
        /// <param name="arguments">参数</param>
        public void ExchangeBindNoWait(string destination, string source, string routingKey, IDictionary<string, object> arguments)
        {
            _channel.ExchangeBindNoWait(destination, source, routingKey, arguments);
        }

        /// <summary>
        /// 定义一个交换机（The exchange is declared non-passive and non-internal. The "nowait" option is not exercised.）
        /// </summary>
        /// <param name="exchangeConfig">交换机配置</param>
        public void ExchangeDeclare(ExchangeConfig exchangeConfig)
        {
            if (!IsExistExchange(exchangeConfig.Exchange, out ExchangeConfig exchange)||(exchange != null && !exchange.Declare))
            {
                _channel.ExchangeDeclare(
                    exchangeConfig.Exchange,
                    Enum.GetName(typeof(EnumExchangeType), exchangeConfig.Type).ToLower(),
                    exchangeConfig.Durable,
                    exchangeConfig.AutoDelete,
                    exchangeConfig.Arguments);
                MQContextOptionsBuilder.AddExchange(new ExchangeConfig
                {
                    Exchange = exchangeConfig.Exchange,
                    Type = exchangeConfig.Type,
                    Arguments = exchangeConfig.Arguments,
                    AutoDelete = exchangeConfig.AutoDelete,
                    Durable = exchangeConfig.Durable,
                    Declare = true
                });
            }
        }

        /// <summary>
        /// 定义一个交换机，但是把nowait设置为true
        /// </summary>
        /// <param name="exchangeConfig">交换机配置</param>
        public void ExchangeDeclareNoWait(ExchangeConfig exchangeConfig)
        {
            if (!IsExistExchange(exchangeConfig.Exchange, out _))
            {
                _channel.ExchangeDeclareNoWait(
                   exchangeConfig.Exchange,
                   Enum.GetName(typeof(EnumExchangeType), exchangeConfig.Type).ToLower(),
                   exchangeConfig.Durable,
                   exchangeConfig.AutoDelete,
                   exchangeConfig.Arguments);
                MQContextOptionsBuilder.AddExchange(new ExchangeConfig
                {
                    Exchange = exchangeConfig.Exchange,
                    Type = exchangeConfig.Type,
                    Arguments = exchangeConfig.Arguments,
                    AutoDelete = exchangeConfig.AutoDelete,
                    Durable = exchangeConfig.Durable,
                    Declare = true
                });
            }
        }

        /// <summary>
        /// 被动定义一个交换机
        /// 此方法在交换机上执行“被动声明”，以验证是否。
        /// 如果交换已经存在，它将什么也不做；如果不存在，它将导致通道级协议异常（通道关闭）。
        /// </summary>
        /// <param name="exchange">交换机名称</param>
        public void ExchangeDeclarePassive(string exchange)
        {
            _channel.ExchangeDeclarePassive(exchange);
        }
        /// <summary>
        /// 删除一个交换机
        /// </summary>
        /// <param name="exchange">交换机名称</param>
        /// <param name="ifUnused">如果未使用</param>
        public void ExchangeDelete(string exchange, bool ifUnused)
        {
            _channel.ExchangeDelete(exchange, ifUnused);
        }
        /// <summary>
        /// 删除一个交换机，但是把nowait设置为true
        /// </summary>
        /// <param name="exchange">交换机名称</param>
        /// <param name="ifUnused">如果未使用</param>
        public void ExchangeDeleteNoWait(string exchange, bool ifUnused)
        {
            _channel.ExchangeDeleteNoWait(exchange, ifUnused);
        }
        /// <summary>
        /// 解绑两个交换机
        /// </summary>
        /// <param name="destination">目标交换机</param>
        /// <param name="source">源交换机</param>
        /// <param name="routingKey">必须少于255个字节</param>
        /// <param name="arguments">参数</param>
        public void ExchangeUnbind(string destination, string source, string routingKey, IDictionary<string, object> arguments)
        {
            _channel.ExchangeUnbind(destination, source, routingKey, arguments);
        }
        /// <summary>
        /// 解绑两个交换机，但是把nowait设置为true
        /// </summary>
        /// <param name="destination">目标交换机</param>
        /// <param name="source">源交换机</param>
        /// <param name="routingKey">必须少于255个字节</param>
        /// <param name="arguments">参数</param>
        public void ExchangeUnbindNoWait(string destination, string source, string routingKey, IDictionary<string, object> arguments)
        {
            _channel.ExchangeUnbindNoWait(destination, source, routingKey, arguments);
        }
    }
}
