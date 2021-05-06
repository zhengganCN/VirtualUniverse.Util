using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using VirtualUniverse.MQ.RabbitMQ.Interfaces;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/4/16 15:51:41；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ
{
    /// <summary>
    /// 类 描 述：
    /// </summary>
    class ChannelCreate : IChannelCreate
    {
        private IModel CreateModel(IConnection connection, string channelId)
        {
            var channel = connection.CreateModel();
            MQContextOptionsBuilder.AddChannel(channelId, channel);
            return channel;
        }
        /// <summary>
        /// 创建通道
        /// </summary>
        public IModel CreateChannel(string connectionId, ref string channelId)
        {
            IModel result;
            if (MQContextOptionsBuilder.Connections.TryGetValue(connectionId, out IConnection connection))
            {
                channelId = string.IsNullOrWhiteSpace(channelId) ? Guid.NewGuid().ToString() : channelId;
                if (MQContextOptionsBuilder.Channels.TryGetValue(channelId, out IModel channel))
                {
                    result = channel;
                }
                else
                {
                    result = CreateModel(connection, channelId);
                }
            }
            else
            {
                throw new ArgumentException($"连接器{connectionId}不存在");
            }
            return result;
        }
    }
}
