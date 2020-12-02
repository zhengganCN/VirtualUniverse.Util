using AmazedModelResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AmazedSecurity.Permission
{
    /// <summary>
    /// 用户权限验证特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public abstract class PermissionAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 权限所属
        /// </summary>
        public string Namespace { get; private set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName { get; private set; }
        /// <summary>
        /// 是否验证成功
        /// </summary>
        public bool IsValid { get; private set; }
        /// <summary>
        /// 用户权限验证
        /// </summary>
        /// <param name="space">权限所属</param>
        /// <param name="permissionName">权限名称</param>
        public PermissionAttribute(string space, string permissionName)
        {
            if (string.IsNullOrWhiteSpace(space))
            {
                throw new ArgumentException($"参数{nameof(space)}不能为空或空字符串");
            }
            if (string.IsNullOrWhiteSpace(permissionName))
            {
                throw new ArgumentException($"参数{nameof(permissionName)}不能为空或空字符串");
            }
            Namespace = space;
            PermissionName = permissionName;
        }
        /// <summary>
        /// 用户权限验证
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="permissionName">权限名称</param>
        /// <returns></returns>
        public abstract bool ValidUserPermission(string userId, string permissionName);
        /// <summary>
        /// 方法执行前验证权限，如不通过，则返回错误信息
        /// </summary>
        /// <param name="context">执行前上下文</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var result = new ModelResult<string>();
            try
            {
                var auth = context.HttpContext.Request.Headers["Authorization"];
                var jwt = new JwtSecurityToken(auth.ToString().Split(' ')[1]);
                var userId = long.Parse(jwt.Subject);

                IsValid = ValidUserPermission(userId.ToString(), Namespace + "." + PermissionName);
                if (!IsValid)
                {
                    result.FailedResult<string>(null, "权限不足");
                    context.Result = new JsonResult(result);
                }
            }
            catch (Exception ex)
            {
                result.FailedResult<string>(null, "权限验证失败，服务器异常");
                context.Result = new JsonResult(result);
                new LoggerFactory().CreateLogger<PermissionAttribute>().LogError(ex.ToString());
            }
        }
    }
}
