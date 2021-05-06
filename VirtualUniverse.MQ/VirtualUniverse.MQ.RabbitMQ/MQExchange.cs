/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/5 16:32:41；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ
{
    /// <summary>
    /// 类 描 述：
    /// </summary>
    public class MQExchange
    {
        /// <summary>
        /// 交换机名称
        /// </summary>
        public string ExchangeName { get; set; }
        /// <summary>
        /// 交换机类型
        /// </summary>
        public EnumExchangeType ExchangeType { get; set; }
    }
}
