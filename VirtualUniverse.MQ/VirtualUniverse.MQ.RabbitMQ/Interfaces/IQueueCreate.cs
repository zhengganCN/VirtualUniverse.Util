using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using VirtualUniverse.MQ.RabbitMQ.Models;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/4/19 8:37:12；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ.Interfaces
{
    /// <summary>
    /// 类 描 述：队列创建
    /// </summary>
    public interface IQueueCreate
    {
        /// <summary>
        /// 绑定队列到交换机
        /// </summary>
        /// <param name="queue">队列名称</param>
        /// <param name="exchange">交换机名称</param>
        /// <param name="routingKey">必须少于255个字节</param>
        /// <param name="arguments">参数</param>
        void QueueBind(string queue, string exchange, string routingKey, IDictionary<string, object> arguments);
        /// <summary>
        /// 绑定队列到交换机（但是把nowait设置为true）
        /// </summary>
        /// <param name="queue">队列名称</param>
        /// <param name="exchange">交换机名称</param>
        /// <param name="routingKey">必须少于255个字节</param>
        /// <param name="arguments">参数</param>
        void QueueBindNoWait(string queue, string exchange, string routingKey, IDictionary<string, object> arguments);
        /// <summary>
        /// 定义一个队列
        /// </summary>
        /// <param name="queueDeclare">声明队列配置</param>
        QueueDeclareOk QueueDeclare(QueueDeclareConfig queueDeclare);
        /// <summary>
        /// 定义一个队列，但是把nowait设置为true
        /// </summary>
        /// <param name="queueDeclare">声明队列配置</param>
        void QueueDeclareNoWait(QueueDeclareConfig queueDeclare);
        /// <summary>
        /// 被动定义一个队列
        /// 此方法在队列上执行“被动声明”，以验证是否。
        /// 如果交换已经存在，它将什么也不做；如果不存在，它将导致通道级协议异常（通道关闭）。
        /// </summary>
        /// <param name="queue">交换机名称</param>
        QueueDeclareOk QueueDeclarePassive(string queue);
        /// <summary>
        /// 删除一个队列
        /// </summary>
        /// <param name="queue">队列名称</param>
        /// <param name="ifUnused">如果未使用</param>
        /// <param name="ifEmpty">如果队列为空</param>
        /// <returns>删除队列时被清除的消息数量</returns>
        uint QueueDelete(string queue, bool ifUnused, bool ifEmpty);
        /// <summary>
        /// 删除一个队列,但是把nowait设置为true
        /// </summary>
        /// <param name="queue">队列名称</param>
        /// <param name="ifUnused">如果未使用</param>
        /// <param name="ifEmpty">如果队列为空</param>
        void QueueDeleteNoWait(string queue, bool ifUnused, bool ifEmpty);
        /// <summary>
        /// 清除消息队列
        /// </summary>
        /// <param name="queue">队列名称</param>
        /// <returns>清除的消息数量</returns>
        uint QueuePurge(string queue);
        /// <summary>
        /// 解绑队列
        /// </summary>
        /// <param name="queue">队列名称</param>
        /// <param name="exchange">交换机名称</param>
        /// <param name="routingKey">必须少于255个字节</param>
        /// <param name="arguments">参数</param>
        void QueueUnbind(string queue, string exchange, string routingKey, IDictionary<string, object> arguments);
    }
}
