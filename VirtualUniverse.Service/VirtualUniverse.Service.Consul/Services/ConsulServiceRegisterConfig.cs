using Consul;
using System.Collections.Generic;

namespace VirtualUniverse.Service.Consul.Services
{
    /// <summary>
    /// Consul服务配置
    /// </summary>
    public class ConsulServiceRegisterConfig
    {
        /// <summary>
        /// 服务注册配置
        /// </summary>
        public IEnumerable<AgentServiceRegistration> AgentServiceRegistrations { get; private set; }
        /// <summary>
        /// Consul服务地址
        /// </summary>
        public string ConsulAddress { get; set; }
        public string ServiceIP { get; set; }
        private AgentServiceCheck AgentServiceCheck { get; set; }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="agentServiceRegistrations">服务注册配置</param>
        public ConsulServiceRegisterConfig(IEnumerable<AgentServiceRegistration> agentServiceRegistrations)
        {
            AgentServiceRegistrations = agentServiceRegistrations;
        }
    }
}
