using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/4/21 10:44:47；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ.Models
{
    /// <summary>
    /// 类 描 述：消费消息配置
    /// </summary>
    public class ConsumeMessageConfig
    {
        public string ChannelId { get; set; }
        public string Queue { get; set; }
        public bool AutoAck { get; set; }
        public string ConsumerTag { get; set; }
        public bool NoLocal { get; set; }
        /// <summary>
        /// 排它性
        /// </summary>
        public bool Exclusive { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public IDictionary<string, object> Arguments { get; set; }
        /// <summary>
        /// 消费者
        /// </summary>
        public EventingBasicConsumer Consumer { get; set; }
        /// <summary>
        /// 消息处理函数
        /// </summary>
        public Func<ReadOnlyMemory<byte>, bool> MessageHandle { get; set; }
    }
}
