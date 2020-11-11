using System;
using System.Collections.Generic;
using System.Text;

namespace AmazedSecurity.Permission
{
    /// <summary>
    /// 权限表操作抽象类
    /// </summary>
    public abstract class PermissionOperationAbstract
    {
        /// <summary>
        /// 创建或更新权限表
        /// </summary>
        /// <param name="permissionName"></param>
        public abstract void CreateOrUpdatePermission(string permissionName);
    }
}
