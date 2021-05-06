using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using VirtualUniverse.MQ.RabbitMQ.Interfaces;
using VirtualUniverse.MQ.RabbitMQ.Models;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/4/16 14:57:37；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ
{
    /// <summary>
    /// 类 描 述：消息操作类
    /// </summary>
    public class MessageOperation : IMessageOperation
    {
        private IModel Channel { get; set; }
        private bool AutoAck { get; set; }
        /// <summary>
        /// 消息处理函数
        /// </summary>
        private Func<ReadOnlyMemory<byte>, bool> MessageHandle { get; set; }
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

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="publishMessageConfig"></param>
        /// <param name="body"></param>
        public void PublishMessage(PublishMessageConfig publishMessageConfig, ReadOnlyMemory<byte> body)
        {
            Channel = GetChannel(publishMessageConfig.ChannelId);
            Channel.BasicPublish(publishMessageConfig.Exchange, publishMessageConfig.RoutingKey, publishMessageConfig.Mandatory,
                            publishMessageConfig.BasicProperties, body);
        }

        /// <summary>
        /// 消费消息
        /// </summary>
        public string ConsumeMessage(ConsumeMessageConfig consumeMessageConfig)
        {
            Channel = GetChannel(consumeMessageConfig.ChannelId);
            AutoAck = consumeMessageConfig.AutoAck;
            MessageHandle = consumeMessageConfig.MessageHandle;
            var consume = Channel.BasicConsume(consumeMessageConfig.Queue, consumeMessageConfig.AutoAck,
                consumeMessageConfig.ConsumerTag, consumeMessageConfig.NoLocal,
                consumeMessageConfig.Exclusive, consumeMessageConfig.Arguments,
                consumeMessageConfig.Consumer);
            consumeMessageConfig.Consumer.Received += DefaultReceivedMessage;
            
            return consume;
        }

        private void DefaultReceivedMessage(object send, BasicDeliverEventArgs args)
        {
            if (MessageHandle != null)
            {
                MessageHandle.Invoke(args.Body);
            }
            if (!AutoAck)
            {
                Channel.BasicAck(args.DeliveryTag, false);
            }
        }
    }
}
