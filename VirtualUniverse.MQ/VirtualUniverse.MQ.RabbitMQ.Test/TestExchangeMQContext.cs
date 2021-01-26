using System.Collections.Generic;
using VirtualUniverse.MQ.RabbitMQ.Models;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/5 17:02:25；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ.Test
{
    /// <summary>
    /// 类 描 述：
    /// </summary>
    class TestExchangeMQContext : MQContext
    {
        public override void OnConfiguration(MQContextOptionsBuilder builder)
        {
            var set = new HashSet<MQExchange>
            {
                new MQExchange
                {
                    ExchangeName = "test.exchange.1",
                    ExchangeType = EnumExchangeType.Direct
                },
                new MQExchange
                {
                    ExchangeName = "test.exchange.2",
                    ExchangeType = EnumExchangeType.Topic
                },
                new MQExchange
                {
                    ExchangeName = "test.exchange.3",
                    ExchangeType = EnumExchangeType.Fanout
                }
            };
            builder.SetConnection("localhost", "guest", "guest").AddExchange(set);
        }
    }
}
