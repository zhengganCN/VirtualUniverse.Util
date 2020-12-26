using AmazedSecurity.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using VirtualUniverse.Security.Permission.Attributes;

namespace VirtualUniverse.Security.Permission.Services
{
    /// <summary>
    /// 自动生成权限列表，通过每个方法的<see cref="PermissionAttribute"/>特性的<see cref="PermissionAttribute.Namespace"/>和<see cref="PermissionAttribute.PermissionName"/>属性来生成权限列表
    /// 如果权限不存在，生成；否则查看该权限是否已标记为删除，是，则取消删除标记，否则，不做任何操作
    /// </summary>
    public static class AutoGeneratePermission
    {
        private static readonly ILogger _logger = new LoggerFactory().CreateLogger(nameof(AutoGeneratePermission));
        private static PermissionOperationAbstract _permissionOperation;
        /// <summary>
        /// 服务集合的扩展方法，用于自动生成权限列表
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="permissionOperation"></param>
        /// <param name="assemblies">程序集集合</param>
        /// <returns></returns>
        public static IServiceCollection AddAutoGeneratePermission(this IServiceCollection services, PermissionOperationAbstract permissionOperation, params Assembly[] assemblies)
        {
            _permissionOperation = permissionOperation;
            _logger.LogDebug($"程序集数量{assemblies.Length}");
            if (assemblies.Length > 0)
            {
                foreach (var assembly in assemblies)
                {
                    _logger.LogDebug($"搜索{assembly.FullName}程序集");
                    GetControllers(assembly);
                }
            }
            return services;
        }
        /// <summary>
        /// 获取所有控制器类
        /// </summary>
        /// <param name="assembly">程序集</param>
        private static void GetControllers(Assembly assembly)
        {
            var controllers = assembly.GetTypes().Where(entity => entity.IsClass && (entity.BaseType == typeof(ControllerBase) || entity.BaseType == typeof(Controller)));
            foreach (var controller in controllers)
            {
                _logger.LogInformation($"搜索{controller.Name}控制器");
                GetControllerMethods(controller);
            }
        }
        /// <summary>
        /// 获取控制器中的所有方法
        /// </summary>
        /// <param name="controller">控制器类</param>
        private static void GetControllerMethods(Type controller)
        {
            var methods = controller.GetMethods();
            foreach (var method in methods)
            {
                GetPermissionValidAttribute(method);
            }
        }
        /// <summary>
        /// 获取方法的所有<see cref="Attribute"/>特性
        /// </summary>
        /// <param name="method">函数方法</param>
        private static void GetPermissionValidAttribute(MethodInfo method)
        {
            var permissionValids = method.GetCustomAttributes<PermissionAttribute>();
            foreach (var permissionValid in permissionValids)
            {
                CreateOrUpdatePermission(permissionValid);
            }
        }
        /// <summary>
        /// 创建或更新权限
        /// </summary>
        /// <param name="permissionValid">权限验证特性</param>
        private static void CreateOrUpdatePermission(PermissionAttribute permissionValid)
        {
            var value = permissionValid.Namespace + "." + permissionValid.PermissionName;
            if (!string.IsNullOrWhiteSpace(value))
            {
                _permissionOperation.CreateOrUpdatePermission(value);
            }
        }
    }
}
