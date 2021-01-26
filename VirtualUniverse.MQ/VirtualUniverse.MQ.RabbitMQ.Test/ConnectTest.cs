using NUnit.Framework;
using System;
using System.Text;
using System.Threading;
using VirtualUniverse.MQ.RabbitMQ.Models;

namespace VirtualUniverse.MQ.RabbitMQ.Test
{
    public class ConnectTest
    {
        private readonly string[] pubilshMessages = { "你好", "RabbitMQ", "我是", "guest" };
        private readonly string QueueName = "test";
        private readonly string ConsumeQueueName = "consume.queue";
        private readonly string DirectExchangeName = "test.direct.exchange";
        private readonly string TopicExchangeName = "test.topic.exchange";
        private readonly string HeadersExchangeName = "test.headers.exchange";
        private readonly string FanoutExchangeName = "test.fanout.exchange";
        private const string ProductRoutingKey = "test.success.hello.ok";
        [Test]
        public void MQContextTest()
        {
            var context = new TestMQContext();
            Assert.Pass();
        }

        [Test]
        public void MQContextExchangeTest()
        {
            var context = new TestExchangeMQContext();

            Assert.Pass();
        }

        [Test]
        public void MQContextQueueTest()
        {
            var context = new TestQueueMQContext();
            Assert.Pass();
        }

        [Test]
        public void MQContextPublishMessage()
        {
            var context = new TestMQContext();
            foreach (var pubilshMessage in pubilshMessages)
            {
                context.PublishMessage(QueueName, Encoding.UTF8.GetBytes(pubilshMessage));
            }
            Assert.Pass();
        }
        [Test]
        public void MQContextConsumeMessage()
        {
            var context = new TestMQContext();
            context.ConsumeMessage(QueueName, (message) =>
            {
                return DealFixlMessage(Encoding.UTF8.GetString(message));
            });
            Thread.Sleep(5000);
            Assert.Pass();
        }

        [Test]
        public void MQContextConsumeMessageFaild()
        {
            var context = new TestMQContext();
            context.ConsumeMessage(QueueName, (message) =>
            {
                return DealFixlMessageReturnFalse(Encoding.UTF8.GetString(message));
            });
            Assert.Pass();
        }

        private bool DealFixlMessageReturnFalse(string message)
        {
            Console.WriteLine(message);
            return false;
        }

        private bool DealFixlMessage(string message)
        {
            if (message == pubilshMessages[0])
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// 消费所有消息
        /// </summary>
        [Test]
        public void MQContextConsumeAllMessage()
        {
            var context = new TestMQContext();
            context.ConsumeMessage(QueueName, (message) =>
             {
                 return DealAllMessage(Encoding.UTF8.GetString(message));
             });
            Thread.Sleep(5000);
            Assert.Pass();
        }

        private bool DealAllMessage(string message)
        {
            Console.WriteLine(message);
            return true;
        }

        /// <summary>
        /// MQContext性能测试
        /// </summary>
        [Test]
        public void MQContextPublishMessagePerformanceTest()
        {
            var context = new TestMQContext();
            for (int i = 0; i < 100000; i++)
            {
                context.PublishMessage(QueueName, Encoding.UTF8.GetBytes("微笑是一种幸福。微笑是一种美丽。微笑是一种财富。微笑具有最独特的魅力！"));
            }
            Assert.Pass();
        }

        [Test]
        [TestCase(EnumExchangeType.Direct)]
        [TestCase(EnumExchangeType.Topic)]
        [TestCase(EnumExchangeType.Fanout)]
        public void MQContextPublishMessageUseExchange(EnumExchangeType exchangeType)
        {
            var context = new TestMQContext();
            foreach (var pubilshMessage in pubilshMessages)
            {
                switch (exchangeType)
                {
                    case EnumExchangeType.Direct:
                        context.PublishMessage(DirectExchangeName, ProductRoutingKey, exchangeType, Encoding.UTF8.GetBytes(pubilshMessage));
                        break;
                    case EnumExchangeType.Fanout:
                        context.PublishMessage(FanoutExchangeName, ProductRoutingKey, exchangeType, Encoding.UTF8.GetBytes(pubilshMessage));
                        break;
                    case EnumExchangeType.Headers:
                        context.PublishMessage(HeadersExchangeName, ProductRoutingKey, exchangeType, Encoding.UTF8.GetBytes(pubilshMessage));
                        break;
                    case EnumExchangeType.Topic:
                        context.PublishMessage(TopicExchangeName, ProductRoutingKey, exchangeType, Encoding.UTF8.GetBytes(pubilshMessage));
                        break;
                    default:
                        break;
                }
            }
            Assert.Pass();
        }

        [Test]
        [TestCase(ProductRoutingKey)]
        public void MQContextConsumeMessageDirectExchange(string routingKey)
        {
            var context = new TestMQContext();
            context.ConsumeMessage(ConsumeQueueName, DirectExchangeName, routingKey, (message) =>
            {
                return DealFixlMessage(Encoding.UTF8.GetString(message));
            });
            Thread.Sleep(5000);
            Assert.Pass();
        }

        [Test]
        [TestCase("test.#")]
        [TestCase("test.success.hello.*")]
        public void MQContextConsumeMessageTopicExchange(string routingKey)
        {
            var context = new TestMQContext();
            context.ConsumeMessage(ConsumeQueueName, TopicExchangeName, routingKey, (message) =>
            {
                return DealFixlMessage(Encoding.UTF8.GetString(message));
            });
            Thread.Sleep(5000);
            Assert.Pass();
        }
        [Test]
        [TestCase(ProductRoutingKey)]
        public void MQContextConsumeMessageFanoutExchange(string routingKey)
        {
            var context = new TestMQContext();
            context.ConsumeMessage(ConsumeQueueName, FanoutExchangeName, routingKey, (message) =>
            {
                return DealFixlMessage(Encoding.UTF8.GetString(message));
            });
            Thread.Sleep(5000);
            Assert.Pass();
        }

    }
}