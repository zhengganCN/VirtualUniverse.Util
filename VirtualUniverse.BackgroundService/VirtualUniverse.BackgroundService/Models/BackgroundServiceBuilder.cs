using System;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/10 23:15:45；更新时间：
************************************************************************************/
namespace VirtualUniverse.BackgroundService.Models
{
    /// <summary>
    /// 类说明：后台服务配置构造器
    /// </summary>
    public class BackgroundServiceBuilder
    {
        /// <summary>
        /// 是否立即启动，如果设置为true，则只有Interval和ExecutionTimes参数有效，默认值为 true
        /// </summary>
        internal bool StartNow { get; set; } = true;
        /// <summary>
        /// 执行次数，默认值为 -1，表示不限制执行次数
        /// </summary>
        internal long ExecutionTimes { get; set; } = -1;
        /// <summary>
        /// 执行间隔，单位毫秒（只有上一个任务执行完毕后才会使用Interval的值来计算下一次任务的启动时间）,默认值 100 ms
        /// </summary>
        internal int Interval { get; set; }
        /// <summary>
        /// 启动时间，如果设置了StartNow为true，则StartTime无效
        /// </summary>
        internal DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间，如果设置了StartNow为true，则StartTime无效
        /// </summary>
        internal DateTime? EndTime { get; set; }
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
        /// 执行次数，默认值为 -1，表示不限制执行次数
        /// </summary>
        /// <param name="executionTimes"></param>
        /// <returns></returns>
        public BackgroundServiceBuilder SetExecutionTimes(long executionTimes = -1)
        {
            ExecutionTimes = executionTimes;
            return this;
        }
        /// <summary>
        /// 执行间隔，单位毫秒（只有上一个任务执行完毕后才会使用Interval的值来计算下一次任务的启动时间）,默认值 100 ms
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        public BackgroundServiceBuilder SetInterval(int interval = 100)
        {
            Interval = interval;
            return this;
        }
        /// <summary>
        /// 启动时间，如果设置了StartNow为true，则StartTime无效
        /// </summary>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public BackgroundServiceBuilder SetStartTime(DateTime? startTime)
        {
            StartTime = startTime;
            return this;
        }
        /// <summary>
        /// 结束时间，如果设置了StartNow为true，则StartTime无效
        /// </summary>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public BackgroundServiceBuilder SetEndTime(DateTime? endTime)
        {
            EndTime = endTime;
            return this;
        }
    }
}
