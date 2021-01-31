using System;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/28 22:20:20；更新时间：
************************************************************************************/
namespace VirtualUniverse.BackgroundService.Models
{
    /// <summary>
    /// 类说明：启动时间模式
    /// </summary>
    public abstract class StartUpDateTimeMode
    {
        /// <summary>
        /// 启动时间，如果设置了StartNow为true，则StartTime无效
        /// </summary>
        public abstract string StartTime { get; set; }
        /// <summary>
        /// 结束时间，如果设置了StartNow为true，则EndTime无效
        /// </summary>
        public abstract string EndTime { get; set; }
        /// <summary>
        /// 初始化
        /// </summary>
        internal abstract void Init();
        /// <summary>
        /// 参数验证
        /// </summary>
        /// <returns></returns>
        internal abstract void ValidParams();
        /// <summary>
        /// 当前时间是否在启动时间和结束时间之间
        /// </summary>
        /// <returns></returns>
        internal abstract bool IsCurrentTimeBetweenStartTimeAndEndTime(DateTime currentDatTime);
        /// <summary>
        /// 判断当前时间是否是处于其他时间循环中
        /// </summary>
        /// <returns></returns>
        internal abstract bool IsOtherDateTimeLoop(DateTime firstExecutionTaskTimeInLoop, DateTime currentDateTime);
    }
}
