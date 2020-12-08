using System;
using System.Collections.Generic;
using System.Text;

namespace AmazedService.ConsulService
{
    /// <summary>
    /// 基本配置
    /// </summary>
    public class BaseConsulServiceConfig
    {
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
