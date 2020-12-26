using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualUniverse.Security.Permission.Services.Interfaces
{
    /// <summary>
    /// 权限实体表必须继承的接口
    /// </summary>
    public interface IPermission
    {
        /// <summary>
        /// 权限名称，唯一
        /// </summary>
        string PermissionName { get; set; }
    }
}
