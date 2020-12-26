using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualUniverse.Service.Consul.Models
{
    /// <summary>
    /// 服务健康检查配置
    /// </summary>
    public class ConsulServiceCheckConfig
    {

        /// <summary>
        /// 取消注册关键服务后
        /// </summary>
        public TimeSpan DeregisterCriticalServiceAfter { get; set; } = TimeSpan.FromSeconds(5);
        /// <summary>
        /// 健康检查时间间隔
        /// </summary>
        public TimeSpan Interval { get; set; } = TimeSpan.FromSeconds(10);
        /// <summary>
        /// 健康检查地址
        /// <br></br>
        /// 默认为：$"http://{consulServiceConfig.ServiceIpAddress}:{consulServiceConfig.ServiceListenPorts[i]}/healthcheck"
        /// </summary>
        public string HTTP { get; set; }
        /// <summary>
        /// 健康检查超时时间
        /// </summary>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(5);
    }
}
