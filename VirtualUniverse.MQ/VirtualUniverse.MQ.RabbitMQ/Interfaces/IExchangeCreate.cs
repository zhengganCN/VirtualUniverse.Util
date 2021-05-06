using System;
using System.Collections.Generic;
using System.Text;
using VirtualUniverse.MQ.RabbitMQ.Models;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/4/16 16:59:08；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ.Interfaces
{
    /// <summary>
    /// 类 描 述：交换机操作接口
    /// </summary>
    public interface IExchangeCreate
    {
        /// <summary>
        /// 绑定两个交换机
        /// </summary>
        /// <param name="destination">目标交换机</param>
        /// <param name="source">源交换机</param>
        /// <param name="routingKey">必须少于255个字节</param>
        /// <param name="arguments">参数</param>
        void ExchangeBind(string destination, string source, string routingKey, IDictionary<string, object> arguments);

        /// <summary>
        /// 绑定两个交换机，但是把nowait设置为true
        /// </summary>
        /// <param name="destination">目标交换机</param>
        /// <param name="source">源交换机</param>
        /// <param name="routingKey">必须少于255个字节</param>
        /// <param name="arguments">参数</param>
        void ExchangeBindNoWait(string destination, string source, string routingKey, IDictionary<string, object> arguments);

        /// <summary>
        /// 定义一个交换机（The exchange is declared non-passive and non-internal. The "nowait" option is not exercised.）
        /// </summary>
        /// <param name="exchangeConfig">交换机配置</param>
        void ExchangeDeclare(ExchangeConfig exchangeConfig);
        /// <summary>
        /// 定义一个交换机，但是把nowait设置为true
        /// </summary>
        /// <param name="exchangeConfig">交换机配置</param>
        void ExchangeDeclareNoWait(ExchangeConfig exchangeConfig);
        /// <summary>
        /// 被动定义一个交换机
        /// 此方法在交换机上执行“被动声明”，以验证是否。
        /// 如果交换已经存在，它将什么也不做；如果不存在，它将导致通道级协议异常（通道关闭）。
        /// </summary>
        /// <param name="exchange">交换机名称</param>
        void ExchangeDeclarePassive(string exchange);
        /// <summary>
        /// 删除一个交换机
        /// </summary>
        /// <param name="exchange">交换机名称</param>
        /// <param name="ifUnused">如果未使用</param>
        void ExchangeDelete(string exchange, bool ifUnused);
        /// <summary>
        /// 删除一个交换机，但是把nowait设置为true
        /// </summary>
        /// <param name="exchange">交换机名称</param>
        /// <param name="ifUnused">如果未使用</param>
        void ExchangeDeleteNoWait(string exchange, bool ifUnused);
        /// <summary>
        /// 解绑两个交换机
        /// </summary>
        /// <param name="destination">目标交换机</param>
        /// <param name="source">源交换机</param>
        /// <param name="routingKey">必须少于255个字节</param>
        /// <param name="arguments">参数</param>
        void ExchangeUnbind(string destination, string source, string routingKey, IDictionary<string, object> arguments);
        /// <summary>
        /// 解绑两个交换机，但是把nowait设置为true
        /// </summary>
        /// <param name="destination">目标交换机</param>
        /// <param name="source">源交换机</param>
        /// <param name="routingKey">必须少于255个字节</param>
        /// <param name="arguments">参数</param>
        void ExchangeUnbindNoWait(string destination, string source, string routingKey, IDictionary<string, object> arguments);
    }
}
