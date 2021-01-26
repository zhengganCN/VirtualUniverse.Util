using System.Collections.Generic;
using VirtualUniverse.MQ.RabbitMQ.Models;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/5 17:05:13；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ.Test
{
    /// <summary>
    /// 类 描 述：
    /// </summary>
    class TestQueueMQContext : MQContext
    {
        public override void OnConfiguration(MQContextOptionsBuilder builder)
        {
            var set = new HashSet<MQQueue>
            {
                new MQQueue
                {
                    QueueName = "test.queue.1"
                },
                new MQQueue
                {
                    QueueName = "test.queue.2"
                },
                new MQQueue
                {
                    QueueName = "test.queue.3",
                    ExchangeName = "test.exchange.1",
                    RoutingKey = "test.*"
                },
                new MQQueue
                {
                    QueueName = "test.queue.4",
                    RoutingKey = "test.exchange"
                }
            };
            builder.SetConnection("localhost", "guest", "guest").AddQueue(set);
        }
    }
}
