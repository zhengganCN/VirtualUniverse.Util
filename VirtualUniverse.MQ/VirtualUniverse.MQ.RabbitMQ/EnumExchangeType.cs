/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/5 14:06:54；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ
{
    /// <summary>
    /// 类 描 述：
    /// </summary>
    public enum EnumExchangeType
    {
        /// <summary>
        /// 直接
        /// </summary>
        Direct = 1,
        /// <summary>
        /// 发布订阅模式
        /// </summary>
        Fanout = 2,
        /// <summary>
        /// 消息头
        /// </summary>
        Headers = 3,
        /// <summary>
        /// 匹配订阅模式
        /// </summary>
        Topic = 4
    }
}
