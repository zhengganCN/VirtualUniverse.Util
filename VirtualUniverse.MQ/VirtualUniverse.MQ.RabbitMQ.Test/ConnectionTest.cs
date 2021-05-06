using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/4/19 11:45:27；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ.Test
{
    /// <summary>
    /// 类 描 述：连接测试
    /// </summary>
    class ConnectionTest
    {
        DostudyMQContext context;
        [SetUp]
        public void SetUp()
        {
            context = new DostudyMQContext();
        }
        [Test]
        [TestCase(10)]
        public void Connection_One(int num)
        {
            for (int i = 0; i < num; i++)
            {
                string connectionId = string.Empty;
                context.CreateConnection(ref connectionId);
            }
            Assert.Pass();
        }

        [Test]
        public void Connection_Two()
        {
            string connectionId = string.Empty;
            context.CreateConnection("test_two", ref connectionId); 
            Assert.Pass();
        }

        [Test]
        public void Connection_Three()
        {
            string connectionId = string.Empty;
            context.CreateConnection(new List<string> { "dostudy.top" }, ref connectionId);
            Assert.Pass();
        }

        [Test]
        public void Connection_Four()
        {
            string connectionId = string.Empty;
            context.CreateConnection(new List<string> { "dostudy.top" }, "test_four", ref connectionId);
            Assert.Pass();
        }

    }
}
