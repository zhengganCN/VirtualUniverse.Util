/***********************************************************************************
****作者：zhenggan；创建时间：2021/3/25 10:10:37；更新时间：
************************************************************************************/
namespace VirtualUniverse.BackgroundService
{
    /// <summary>
    /// 类 描 述：启动参数配置
    /// </summary>
    public class StartParamConfig
    {
        /// <summary>
        /// 是否立即启动
        /// </summary>
        public bool StartNow { get; set; }
        /// <summary>
        /// 执行次数
        /// </summary>
        public long ExecutionTimes { get; set; } = -1;
        /// <summary>
        /// 启动模式（1：天，2：周，3：月，4：年）
        /// </summary>
        public int StartUpDateTimeMode { get; set; }
        /// <summary>
        /// 是否刷新，如果执行次数已达到最大值
        /// </summary>
        public bool IsRefreshIfExecutionEnough { get; set; } = true;
        /// <summary>
        /// 执行间隔，单位毫秒（只有上一个任务执行完毕后才会使用Interval的值来计算下一次任务的启动时间）,默认值 100 ms
        /// </summary>
        public int Interval { get; set; } = 100;
        /// <summary>
        /// 启动时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
    }
}
