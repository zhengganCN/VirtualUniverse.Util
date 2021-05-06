using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/4/19 17:20:38；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ.Test
{
    /// <summary>
    /// 类 描 述：上下文测试
    /// </summary>
    class ContextTest
    {
        [Test]
        public void ContextExchangeTest()
        {
            new DostudyMQContext();
            Assert.Pass();
        }
    }
}
