using Consul;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using VirtualUniverse.Service.Consul.Models;

namespace VirtualUniverse.Service.Consul.Services
{
    /// <summary>
    /// Consul服务扩展
    /// </summary>
    public static class ConsulServiceExtension
    {
        /// <summary>
        /// 添加Consul服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config">配置</param>
        /// <returns></returns>
        public static IServiceCollection AddConsul(this IServiceCollection services, BaseConsulServiceConfig config)
        {
            config.ServiceIpAddress = string.IsNullOrWhiteSpace(config.ServiceIpAddress) ? IpAddressOperation.GetLanIPAddress() : config.ServiceIpAddress;
            var client = new ConsulClient(options =>
            {
                options.Address = new Uri(config.ConsulAddress);
            });
            var registers = new List<AgentServiceRegistration>();
            foreach (var listenPort in config.ServiceListenPorts)
            {
                var http = string.IsNullOrWhiteSpace(config.ServiceCheckConfig.HTTP) ? $"http://{config.ServiceIpAddress}:{listenPort}/healthcheck" : config.ServiceCheckConfig.HTTP;
                registers.Add(new AgentServiceRegistration
                {
                    ID = $"{config.ServiceIpAddress}:{listenPort}",
                    Name = config.ServiceName,
                    Address = config.ServiceIpAddress,
                    Port = listenPort,
                    Check = new AgentServiceCheck
                    {
                        DeregisterCriticalServiceAfter = config.ServiceCheckConfig.DeregisterCriticalServiceAfter,
                        Interval = config.ServiceCheckConfig.Interval,
                        HTTP = http,
                        Timeout = config.ServiceCheckConfig.Timeout
                    },
                    Tags = config.ConsulServiceTags,
                    Meta = config.Meta
                });
            }
            foreach (var register in registers)
            {
                client.Agent.ServiceRegister(register).Wait();// 注册服务
            }
            return services;
        }
    }
}
