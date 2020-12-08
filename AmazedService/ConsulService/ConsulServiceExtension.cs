using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmazedService.ConsulService
{
    /// <summary>
    /// Consul服务扩展
    /// </summary>
    public static class ConsulServiceExtension
    {
        /// <summary>
        /// 添加并注册服务至Consul
        /// </summary>
        /// <param name="services"></param>
        /// <param name="consulServiceRegisterConfig"></param>
        /// <returns></returns>
        public static IServiceCollection AddConsul(this IServiceCollection services, ConsulServiceRegisterConfig consulServiceRegisterConfig)
        {
            var client = new ConsulClient(options =>
            {
                options.Address = new Uri(consulServiceRegisterConfig.ConsulAddress);
            });
            foreach (var registration in consulServiceRegisterConfig.AgentServiceRegistrations)
            {
                // 注册服务
                client.Agent.ServiceRegister(registration).Wait();
            }
            return services;
        }

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
                var http = string.IsNullOrWhiteSpace(config.HTTP) ? $"http://{config.ServiceIpAddress}:{listenPort}/healthcheck" : config.HTTP;
                registers.Add(new AgentServiceRegistration
                {
                    ID = $"{config.ServiceIpAddress}:{listenPort}",
                    Name = config.ServiceName,
                    Address = config.ServiceIpAddress,
                    Port = listenPort,
                    Check = new AgentServiceCheck
                    {
                        DeregisterCriticalServiceAfter = config.DeregisterCriticalServiceAfter,
                        Interval = config.Interval,
                        HTTP = http,
                        Timeout = config.Timeout
                    }
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
