using NUnit.Framework;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualUniverse.MQ.RabbitMQ.Models;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/4/21 10:03:59；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ.Test
{
    /// <summary>
    /// 类 描 述：消费消息测试
    /// </summary>
    class ConsumerMessageTest
    {
        IModel channel;
        [Test]
        public void PublishMessage()
        {

            var context = new DostudyMQContext();
            string connectionId = default;
            context.CreateConnection(ref connectionId);
            string channelId = default;
            channel= context.CreateChannel(connectionId, ref channelId);
            var exchange = "h1";
            var queue = "q1";
            var routingKey = "q#";

            var eventingBasicConsumer = new EventingBasicConsumer(channel);
            eventingBasicConsumer.Received += ReceivedMessage;
            var config = new ConsumeMessageConfig
            {
                ChannelId = channelId,
                Queue = queue,
                AutoAck = false,
                Arguments = null,
                ConsumerTag = "",
                Consumer = eventingBasicConsumer,
                Exclusive = false,
                NoLocal = false
            };
            context.ConsumeMessage(config);
            
            Task.Delay(1000).Wait();
            Assert.Pass();
        }

        public void ReceivedMessage(object send, BasicDeliverEventArgs args)
        {
            channel.BasicAck(args.DeliveryTag, false);
        }
    }
}
