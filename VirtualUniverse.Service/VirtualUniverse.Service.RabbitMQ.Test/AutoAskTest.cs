using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/11 10:59:52；更新时间：
************************************************************************************/
namespace VirtualUniverse.Service.RabbitMQ.Test
{
    /// <summary>
    /// 类 描 述：
    /// </summary>
    class AutoAskTest
    {
        private readonly string[] pubilshMessages = { "你好", "RabbitMQ", "我是", "guest" };
        private readonly string OpenAutoAskQueueName = "open.autoask.queue";
        private readonly string CloseAutoAskQueueName = "close.autoask.queue";

        #region 开启自动应答
        [Test]
        public void MQContextPublishMessageToOpenAutoAskQueueName()
        {
            var context = new TestMQContext();
            foreach (var pubilshMessage in pubilshMessages)
            {
                context.PublishMessage(OpenAutoAskQueueName, Encoding.UTF8.GetBytes(pubilshMessage));
            }
            Assert.IsTrue(true);
        }

        [Test]
        public void MQContextConsumeMessageFaildByOpenAutoAsk()
        {
            var context = new TestMQContext();
            context.OpenAutoAsk();
            context.ConsumeMessage(OpenAutoAskQueueName, (message) =>
            {
                return DealFixlMessageReturnFalse(Encoding.UTF8.GetString(message));
            });
            Assert.IsTrue(true);
        }

        [Test]
        public void MQContextConsumeMessageSuccessByOpenAutoAsk()
        {
            var context = new TestMQContext();
            context.OpenAutoAsk();
            context.ConsumeMessage(OpenAutoAskQueueName, (message) =>
            {
                return DealFixlMessageReturnTrue(Encoding.UTF8.GetString(message));
            });
            Assert.IsTrue(true);
        }
        #endregion

        #region 关闭自动应答
        [Test]
        public void MQContextPublishMessageToCloseAskQueueName()
        {
            var context = new TestMQContext();
            foreach (var pubilshMessage in pubilshMessages)
            {
                context.PublishMessage(CloseAutoAskQueueName, Encoding.UTF8.GetBytes(pubilshMessage));
            }
            Assert.IsTrue(true);
        }

        [Test]
        public void MQContextConsumeMessageFaildByCloseAutoAsk()
        {
            var context = new TestMQContext();
            context.ConsumeMessage(CloseAutoAskQueueName, (message) =>
            {
                return DealFixlMessageReturnFalse(Encoding.UTF8.GetString(message));
            });
            Assert.IsTrue(true);
        }

        [Test]
        public void MQContextConsumeMessageSuccessByCloseAutoAsk()
        {
            var context = new TestMQContext();
            context.ConsumeMessage(CloseAutoAskQueueName, (message) =>
            {
                return DealFixlMessageReturnTrue(Encoding.UTF8.GetString(message));
            });
            Assert.IsTrue(true);
        }
        #endregion

        private bool DealFixlMessageReturnFalse(string message)
        {
            Console.WriteLine(message);
            return false;
        }

        private bool DealFixlMessageReturnTrue(string message)
        {
            Console.WriteLine(message);
            return true;
        }
    }
}
