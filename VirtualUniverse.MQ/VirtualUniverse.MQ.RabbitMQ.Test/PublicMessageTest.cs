using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualUniverse.MQ.RabbitMQ.Models;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/4/20 10:24:10；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ.Test
{
    /// <summary>
    /// 类 描 述：
    /// </summary>
    class PublicMessageTest
    {
        [Test]
        public void PublishMessage()
        {
            var context = new DostudyMQContext();
            string connectionId = default;
            context.CreateConnection(ref connectionId);
            string channelId = default;
            context.CreateChannel(connectionId, ref channelId);
            var exchange = "h1";
            var queue = "q1";
            var routingKey = "q#";
            context.QueueBind(channelId, queue, exchange,routingKey, null);
            context.QueueBind(channelId, "q2", exchange, routingKey, null);
            var publishMessageConfig = new PublishMessageConfig
            {
                ChannelId = channelId,
                Exchange = exchange,
                RoutingKey = routingKey,
                BasicProperties = null,
                Mandatory = false
            };
            for (int i = 0; i < 10; i++)
            {
                context.PublishMessage(publishMessageConfig, Encoding.UTF8.GetBytes("hello!"));
            }
            Task.Delay(1000).Wait();
            Assert.Pass();
        }
    }
}
