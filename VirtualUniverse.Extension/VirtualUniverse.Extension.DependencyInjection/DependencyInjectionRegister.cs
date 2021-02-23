using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using VirtualUniverse.Extension.DependencyInjection.Interfaces;

namespace VirtualUniverse.Extension.DependencyInjection
{
    /// <summary>
    /// 依赖注入注册
    /// </summary>
    public static class DependencyInjectionRegister
    {
        private const string Const_ITransientDI = nameof(ITransientDI);
        private const string Const_IScopedDI = nameof(IScopedDI);
        private const string Const_ISingletonDI = nameof(ISingletonDI);
        private static readonly string[] Const_DIs = new string[3] { Const_ITransientDI, Const_IScopedDI, Const_ISingletonDI };
        /// <summary>
        /// 自动注入
        /// 要自动注入的接口必须继承<see cref="ITransientDI"/>，<see cref="IScopedDI"/>，<see cref="ISingletonDI"/>这三个接口之中的一个，且只能继承其中一个，不能继承多个
        /// 如果继承了多个，则按 ITransientDI、IScopedDI、ISingletonDI 的顺序按找到的第一个来注入相应的类型
        /// </summary>
        /// <param name="services">服务集合</param>
        public static IServiceCollection AddDependencyInjectionRegister(this IServiceCollection services,params string[] assemblyNames)
        {
            var assemblies = new List<Assembly>();
            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, @"*.dll");
            foreach (var file in files)
            {
#pragma warning disable S3885 // "Assembly.Load" should be used
                assemblies.Add(Assembly.LoadFile(file));
#pragma warning restore S3885 // "Assembly.Load" should be used
            }
            foreach (var assemblyName in assemblyNames)
            {
                assemblies = assemblies.Where(entity => entity.FullName.StartsWith(assemblyName)).ToList();
            }
            if (assemblies.Any())
            {
                foreach (var assembly in assemblies)
                {
                    var entityClasses = assembly.GetTypes().Where(entity =>
                        entity.GetInterfaces().Any(inter =>
                                    inter.Name == Const_ITransientDI ||
                                    inter.Name == Const_IScopedDI ||
                                    inter.Name == Const_ISingletonDI) &&
                        entity.IsClass
                                );//获取该程序集的所有继承了 ITransientDI、IScopedDI、ISingletonDI 接口的实体类
                    foreach (var entityClass in entityClasses)
                    {
                        var interfaces = entityClass.GetInterfaces().Where(entity =>
                         entity.Name != Const_IScopedDI ||
                         entity.Name != Const_ISingletonDI ||
                         entity.Name != Const_ITransientDI);//获取实体类的所有非 ITransientDI、IScopedDI、ISingletonDI 接口
                        DIRegister(services, entityClass, interfaces);
                    }
                }
            }
            return services;
        }
        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="entityClass"></param>
        /// <param name="interfaces"></param>
        private static void DIRegister(IServiceCollection services, Type entityClass, IEnumerable<Type> interfaces)
        {
            foreach (var diType in Const_DIs)
            {
                var diTypeInterfaces = GetDIInterfaces(interfaces, diType);
                if (diTypeInterfaces.Any())
                {
                    foreach (var diTypeInterface in diTypeInterfaces)
                    {
                        switch (diType)
                        {
                            case Const_ITransientDI:
                                services.AddTransient(diTypeInterface, entityClass);
                                break;
                            case Const_IScopedDI:
                                services.AddScoped(diTypeInterface, entityClass);
                                break;
                            case Const_ISingletonDI:
                                services.AddSingleton(diTypeInterface, entityClass);
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                }
            }
        }
        /// <summary>
        /// 获取所有继承了 ITransientDI、IScopedDI、ISingletonDI 这三个接口的接口
        /// </summary>
        /// <param name="interfaces"></param>
        /// <param name="dIType"></param>
        /// <returns></returns>
        private static IEnumerable<Type> GetDIInterfaces(IEnumerable<Type> interfaces, string dIType)
        {
            return interfaces.Where(entity => entity.GetInterfaces().Any(inter =>
                                    inter.Name == dIType));
        }
    }
}
