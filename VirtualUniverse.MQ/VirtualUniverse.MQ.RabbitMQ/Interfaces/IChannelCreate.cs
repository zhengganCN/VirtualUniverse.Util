using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/4/16 15:49:26；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ.Interfaces
{
    /// <summary>
    /// 类 描 述：通道创建接口
    /// </summary>
    public interface IChannelCreate
    {
        IModel CreateChannel(string connectionId,ref string channelId);
    }
}
