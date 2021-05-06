using VirtualUniverse.MQ.RabbitMQ.Models;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/5 8:31:59；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ.Test
{
    /// <summary>
    /// 类 描 述：
    /// </summary>
    class DostudyMQContext : MQContext
    {
        public override void OnConfiguration(MQContextOptionsBuilder builder)
        {
            builder.SetConnection("dostudy.top", "zhenggan", "zhenggan");
            ExchangeConfig(builder);
            QueueConfig(builder);
        }

        private void ExchangeConfig(MQContextOptionsBuilder builder)
        {
            builder.AddExchangeConfig(new ExchangeConfig
            {
                Arguments = null,
                AutoDelete = true,
                Durable = false,
                Exchange = "t1",
                Type = EnumExchangeType.Topic
            })
                .AddExchangeConfig(new ExchangeConfig
                {
                    Arguments = null,
                    AutoDelete = true,
                    Durable = false,
                    Exchange = "t2",
                    Type = EnumExchangeType.Topic
                })
                .AddExchangeConfig(new ExchangeConfig
                {
                    Arguments = null,
                    AutoDelete = true,
                    Durable = false,
                    Exchange = "d1",
                    Type = EnumExchangeType.Direct
                })
                .AddExchangeConfig(new ExchangeConfig
                {
                    Arguments = null,
                    AutoDelete = true,
                    Durable = false,
                    Exchange = "d2",
                    Type = EnumExchangeType.Direct
                })
                .AddExchangeConfig(new ExchangeConfig
                {
                    Arguments = null,
                    AutoDelete = true,
                    Durable = false,
                    Exchange = "f1",
                    Type = EnumExchangeType.Fanout
                })
                .AddExchangeConfig(new ExchangeConfig
                {
                    Arguments = null,
                    AutoDelete = true,
                    Durable = false,
                    Exchange = "f2",
                    Type = EnumExchangeType.Fanout
                })
                .AddExchangeConfig(new ExchangeConfig
                {
                    Arguments = null,
                    AutoDelete = true,
                    Durable = false,
                    Exchange = "h1",
                    Type = EnumExchangeType.Headers
                })
                .AddExchangeConfig(new ExchangeConfig
                {
                    Arguments = null,
                    AutoDelete = true,
                    Durable = false,
                    Exchange = "h2",
                    Type = EnumExchangeType.Headers
                });
        }

        private void QueueConfig(MQContextOptionsBuilder builder)
        {
            builder.AddQueueDeclareConfig(new QueueDeclareConfig
            {
                Arguments = null,
                AutoDelete = false,
                Durable = true,
                Exclusive = false,
                Queue = "q1"
            })
            .AddQueueDeclareConfig(new QueueDeclareConfig
            {
                Arguments = null,
                AutoDelete = false,
                Durable = true,
                Exclusive = false,
                Queue = "q2"
            });
        }
    }
}
