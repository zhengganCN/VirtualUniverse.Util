using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VirtualUniverse.MQ.RabbitMQ.Interfaces;
using VirtualUniverse.MQ.RabbitMQ.Models;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/4/19 8:36:59；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ
{
    /// <summary>
    /// 类 描 述：队列创建
    /// </summary>
    public class QueueCreate : IQueueCreate
    {
        private readonly string _channelId;
        private readonly IModel _channel;
        public QueueCreate(string channelId)
        {
            _channelId = channelId;
            _channel = GetChannel(channelId);
        }

        private IModel GetChannel(string channelId)
        {
            if (MQContextOptionsBuilder.Channels.TryGetValue(channelId, out IModel channel))
            {
                return channel;
            }
            else
            {
                throw new ArgumentException("不存在通道");
            }
        }

        public void QueueBind(string queue, string exchange, string routingKey, IDictionary<string, object> arguments)
        {
            _channel.QueueBind(queue, exchange, routingKey, arguments);
        }
        public void QueueBindNoWait(string queue, string exchange, string routingKey, IDictionary<string, object> arguments)
        {
            _channel.QueueBindNoWait(queue, exchange, routingKey, arguments);
        }
        public QueueDeclareOk QueueDeclare(QueueDeclareConfig queueDeclare)
        {
            var result = _channel.QueueDeclare(queueDeclare.Queue, queueDeclare.Durable,
                queueDeclare.Exclusive, queueDeclare.AutoDelete, queueDeclare.Arguments);
            if (result != null)
            {
                queueDeclare.Declare = true;
                MQContextOptionsBuilder.AddQueue(queueDeclare);
            }
            return result;
        }

        public void QueueDeclareNoWait(QueueDeclareConfig queueDeclare)
        {
            _channel.QueueDeclareNoWait(queueDeclare.Queue, queueDeclare.Durable,
                queueDeclare.Exclusive, queueDeclare.AutoDelete, queueDeclare.Arguments);
            queueDeclare.Declare = true;
            MQContextOptionsBuilder.AddQueue(queueDeclare);
        }

        public QueueDeclareOk QueueDeclarePassive(string queue)
        {
            var result = _channel.QueueDeclarePassive(queue);
            return result;
        }

        public uint QueueDelete(string queue, bool ifUnused, bool ifEmpty)
        {
            var result = _channel.QueueDelete(queue, ifUnused, ifEmpty);
            MQContextOptionsBuilder.DeleteQueue(queue);
            return result;
        }

        public void QueueDeleteNoWait(string queue, bool ifUnused, bool ifEmpty)
        {
            _channel.QueueDeleteNoWait(queue, ifUnused, ifEmpty);
            MQContextOptionsBuilder.DeleteQueue(queue);
        }

        public uint QueuePurge(string queue)
        {
             return _channel.QueuePurge(queue);
        }

        public void QueueUnbind(string queue, string exchange, string routingKey, IDictionary<string, object> arguments)
        {
            _channel.QueueUnbind(queue, exchange, routingKey, arguments);
        }
    }
}
