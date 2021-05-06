using NUnit.Framework;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/4/19 15:08:02；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ.Test
{
    /// <summary>
    /// 类 描 述：通道测试
    /// </summary>
    class ChannelTest
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
        [TestCase(10)]
        public void CreateChannel(int num)
        {
            for (int i = 0; i < num; i++)
            {
                string channelId = default;
                context.CreateChannel(connectionId, ref channelId);
            }
            Assert.Pass();
        }
    }
}
