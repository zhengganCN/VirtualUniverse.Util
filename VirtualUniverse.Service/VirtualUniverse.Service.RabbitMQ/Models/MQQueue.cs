/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/5 16:39:45；更新时间：
************************************************************************************/
namespace VirtualUniverse.Service.RabbitMQ.Models
{
    /// <summary>
    /// 类 描 述：
    /// </summary>
    public class MQQueue
    {
        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName { get; set; }
        /// <summary>
        /// 交换机名称
        /// </summary>
        public string ExchangeName { get; set; }
        /// <summary>
        /// 路由键
        /// </summary>
        public string RoutingKey { get; set; }
    }
}
