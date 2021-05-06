using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using VirtualUniverse.MQ.RabbitMQ.Models;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/4/16 15:03:55；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ.Interfaces
{
    /// <summary>
    /// 类 描 述：消息操作接口
    /// </summary>
    public interface IMessageOperation
    {
        void PublishMessage(PublishMessageConfig publishMessageConfig, ReadOnlyMemory<byte> body);
        string ConsumeMessage(ConsumeMessageConfig consumeMessageConfig);
    }
}
