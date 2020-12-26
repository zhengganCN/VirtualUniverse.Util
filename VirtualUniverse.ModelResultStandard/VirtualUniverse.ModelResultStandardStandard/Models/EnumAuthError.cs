using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace VirtualUniverse.ModelResultStandard.Models
{
    /// <summary>
    /// 枚举授权错误
    /// </summary>
    public enum EnumAuthError
    {
        /// <summary>
        /// 账号或密码验证不通过
        /// </summary>
        [Description("账号或密码验证不通过")]
        LoginFailed = 100001,
        /// <summary>
        /// 权限不足
        /// </summary>
        [Description("权限不足")]
        InsufficientAuthoritys = 100002
    }
}
