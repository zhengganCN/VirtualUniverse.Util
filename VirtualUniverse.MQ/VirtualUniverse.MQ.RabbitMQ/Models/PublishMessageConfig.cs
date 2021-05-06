using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/4/21 10:36:48；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ.Models
{
    /// <summary>
    /// 类 描 述：消息发布配置
    /// </summary>
    public class PublishMessageConfig
    {
        public string ChannelId { get; set; }
        public string Exchange { get; set; }
        public string RoutingKey { get; set; }
        public bool Mandatory { get; set; }
        public IBasicProperties BasicProperties { get; set; }
    }
}
