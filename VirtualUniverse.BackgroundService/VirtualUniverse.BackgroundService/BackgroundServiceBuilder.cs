using VirtualUniverse.BackgroundService.Models;
using VirtualUniverse.BackgroundService.Models.StartUpModes;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/10 23:15:45；更新时间：
************************************************************************************/
namespace VirtualUniverse.BackgroundService
{
    /// <summary>
    /// 类说明：后台服务配置构造器
    /// </summary>
    public class BackgroundServiceBuilder
    {
        /// <summary>
        /// 是否立即启动，如果设置为true，则只有Interval和ExecutionTimes参数有效，默认值为 true
        /// </summary>
        internal bool StartNow { get; private set; } = true;
        /// <summary>
        /// 允许执行的次数，默认值为 -1，表示不限制执行次数
        /// </summary>
        internal long ExecutionTimes { get; private set; } = -1;
        /// <summary>
        /// 是否刷新，如果执行次数已达到最大值
        /// </summary>
        internal bool IsRefreshIfExecutionEnough { get; private set; } = true;
        /// <summary>
        /// 执行间隔，单位毫秒（只有上一个任务执行完毕后才会使用Interval的值来计算下一次任务的启动时间）,默认值 100 ms
        /// </summary>
        internal int Interval { get; private set; } = 100;
        /// <summary>
        /// 启动模式
        /// </summary>
        internal StartUpDateTimeMode StartUpDateTimeMode { get; private set; }

        /// <summary>
        /// 是否立即启动，如果设置为true，则只有Interval和ExecutionTimes参数有效，默认值为 true
        /// </summary>
        /// <param name="startNow"></param>
        /// <returns></returns>
        public BackgroundServiceBuilder SetStartNow(bool startNow = true)
        {
            StartNow = startNow;
            return this;
        }

        /// <summary>
        /// 设置执行次数和
        /// </summary>
        /// <param name="executionTimes">执行次数，默认值为 -1，表示不限制执行次数</param>
        /// <param name="isRefreshIfExecutionEnough">是否自动刷新执行次数</param>
        /// <remarks>
        /// 参数： IsRefreshIfExecutionEnough <br></br>
        ///     如果 启动模式为立即启动（StartNow = true），则此参数无效 <br></br>
        ///     否则 每当当前时间与启动时间相等且 IsRefreshIfExecutionEnough 等于 True 时，ExecutionTimes 参数会自动刷新为 0
        /// </remarks>
        /// <returns></returns>
        public BackgroundServiceBuilder SetExecutionTimes(long executionTimes = -1, bool isRefreshIfExecutionEnough = true)
        {
            ExecutionTimes = executionTimes;
            IsRefreshIfExecutionEnough = isRefreshIfExecutionEnough;
            return this;
        }

        /// <summary>
        /// 执行间隔，单位毫秒（只有上一个任务执行完毕后才会使用Interval的值
        /// 来计算下一次任务的启动时间）,默认值 1000 ms
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        public BackgroundServiceBuilder SetInterval(int interval = 1000)
        {
            Interval = interval;
            return this;
        }

        /// <summary>
        /// 设置启动模式
        /// </summary>
        /// <param name="startUpMode">启动模式</param>
        /// <param name="startTime">启动时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public BackgroundServiceBuilder SetStartUpDateTime(EnumStartUpMode startUpMode, string startTime, string endTime)
        {
            StartUpDateTimeMode startUpDateTimeMode = null;
            switch (startUpMode)
            {
                case EnumStartUpMode.Day:
                    startUpDateTimeMode = new DayStartUpDateTimeMode { StartTime = startTime, EndTime = endTime };
                    break;
                case EnumStartUpMode.Week:
                    startUpDateTimeMode = new WeekStartUpDateTimeMode { StartTime = startTime, EndTime = endTime };
                    break;
                case EnumStartUpMode.Month:
                    startUpDateTimeMode = new MonthStartUpDateTimeMode { StartTime = startTime, EndTime = endTime };
                    break;
                case EnumStartUpMode.Year:
                    startUpDateTimeMode = new YearStartUpDateTimeMode { StartTime = startTime, EndTime = endTime };
                    break;
            }
            StartUpDateTimeMode = startUpDateTimeMode;
            return this;
        }
    }
}
