using System.ComponentModel;

namespace VirtualUniverse.ModelResultStandard.Models
{
    /// <summary>
    /// 模型验证结果提示枚举
    /// </summary>
    public enum EnumModelError
    {
        /// <summary>
        /// 模型验证失败
        /// </summary>
        [Description("模型验证失败")]
        ModelVaildFaild = 101001
    }
}
