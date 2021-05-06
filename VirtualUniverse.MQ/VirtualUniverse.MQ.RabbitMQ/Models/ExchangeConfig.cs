using System;
using System.Collections.Generic;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/4/19 17:01:40；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ.Models
{
    /// <summary>
    /// 类 描 述：交换机配置
    /// </summary>
    public class ExchangeConfig
    {
        /// <summary>
        /// 交换机名称
        /// </summary>
        public string Exchange { get; set; }
        /// <summary>
        /// 交换机类型
        /// </summary>
        public EnumExchangeType Type { get; set; }
        /// <summary>
        /// 持久化
        /// </summary>
        public bool Durable { get; set; }
        /// <summary>
        /// 自动删除
        /// </summary>
        public bool AutoDelete { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public IDictionary<string, object> Arguments { get; set; }
        /// <summary>
        /// 是否已声明
        /// </summary>
        public bool Declare { get; internal set; }
    }
}
