using System.ComponentModel;

namespace VirtualUniverse.ModelResultStandard.Models
{
    /// <summary>
    /// 枚举结果
    /// </summary>
    public enum EnumResult
    {
        /// <summary>
        /// 调用失败
        /// </summary>
        [Description("调用失败")]
        Error = 0,
        /// <summary>
        /// 调用成功
        /// </summary>
        [Description("调用成功")]
        Success = 1
    }
}
