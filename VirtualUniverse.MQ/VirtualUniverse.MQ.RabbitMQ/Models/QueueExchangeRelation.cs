using System;
using System.Collections.Generic;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/4/19 8:43:00；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ.Models
{
    /// <summary>
    /// 类 描 述：队列和交换机的关系
    /// </summary>
    public class QueueExchangeRelation
    {
        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName { get; set; }
        /// <summary>
        /// 交换机名称
        /// </summary>
        public string ExchangeName { get; set; }
    }
}
