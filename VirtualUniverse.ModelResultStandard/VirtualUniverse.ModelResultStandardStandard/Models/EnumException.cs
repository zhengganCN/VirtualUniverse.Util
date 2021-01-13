using System.ComponentModel;

namespace VirtualUniverse.ModelResultStandard.Models
{
    /// <summary>
    /// 枚举服务异常
    /// </summary>
    public enum EnumException
    {
        /// <summary>
        /// 服务器异常，请稍后再试
        /// </summary>
        [Description("服务器异常，请稍后再试")]
        ServerError = 103000,
        /// <summary>
        /// 服务器繁忙，请稍后再试
        /// </summary>
        [Description("服务器繁忙，请稍后再试")]
        ServerBusy = 103001
    }
}
