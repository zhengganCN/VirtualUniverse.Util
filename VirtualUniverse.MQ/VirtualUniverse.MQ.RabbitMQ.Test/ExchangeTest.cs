using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using VirtualUniverse.MQ.RabbitMQ.Models;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/4/19 15:23:38；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ.Test
{
    /// <summary>
    /// 类 描 述：交换机测试
    /// </summary>
    class ExchangeTest
    {
        DostudyMQContext context;
        string connectionId = default;
        [SetUp]
        public void SetUp()
        {
            context = new DostudyMQContext();
            context.CreateConnection(ref connectionId);
        }

        [Test]
        [TestCase(1)]
        public void Exchange_One(int num)
        {
            string channelId = default;
            context.CreateChannel(connectionId, ref channelId);
            for (int i = 0; i < num; i++)
            {
                context.ExchangeDeclare(channelId, new ExchangeConfig
                {
                    Arguments = null,
                    AutoDelete = true,
                    Durable = false,
                    Exchange = "t1",
                    Type = EnumExchangeType.Topic
                });
            }
            Assert.Pass();
        }

    }
}
