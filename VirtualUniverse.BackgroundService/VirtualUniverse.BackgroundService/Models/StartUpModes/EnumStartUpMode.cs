/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/31 21:38:51；更新时间：
************************************************************************************/
namespace VirtualUniverse.BackgroundService.Models.StartUpModes
{
    /// <summary>
    /// 类说明：枚举启动模式
    /// </summary>
    public enum EnumStartUpMode
    {
        /// <summary>
        /// 天；启动时间和结束时间的格式为：HH:mm:ss;
        /// </summary>
        Day = 1,
        /// <summary>
        /// 周；启动时间和结束时间的格式为：dayOfWeek;HH:mm:ss，且 dayOfWeek为 1&lt;=int&lt;=7
        /// </summary>
        Week = 2,
        /// <summary>
        /// 月；启动时间和结束时间的格式为：dayOfMonth;HH:mm:ss，且 dayOfMonth为 1&lt;=int&lt;=28
        /// </summary>
        Month = 3,
        /// <summary>
        /// 年；启动时间和结束时间的格式为：MM-dd;HH:mm:ss
        /// </summary>
        Year = 4
    }
}
