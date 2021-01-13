using System.Collections.Generic;

namespace VirtualUniverse.Service.Consul.Models
{
    /// <summary>
    /// 基本配置
    /// </summary>
    public class BaseConsulServiceConfig
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public BaseConsulServiceConfig()
        {
            ServiceCheckConfig = new ConsulServiceCheckConfig();
        }

        /// <summary>
        /// 服务名称，必填
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// Consul服务地址，必填
        /// </summary>
        public string ConsulAddress { get; set; }
        /// <summary>
        /// 服务IP地址，如为空则IP地址自动获取本机的IPV4地址
        /// </summary>
        public string ServiceIpAddress { get; set; }
        /// <summary>
        /// 服务监听端口，必填
        /// </summary>
        public int[] ServiceListenPorts { get; set; }
        /// <summary>
        /// 服务健康检查配置
        /// </summary>
        public ConsulServiceCheckConfig ServiceCheckConfig { get; set; }
        /// <summary>
        /// 服务具有的标签
        /// </summary>
        public string[] ConsulServiceTags { get; set; }
        /// <summary>
        /// 元数据
        /// </summary>
        public Dictionary<string, string> Meta { get; set; }
    }
}
