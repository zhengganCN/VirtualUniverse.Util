using System;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/27 23:14:29；更新时间：
************************************************************************************/
namespace VirtualUniverse.BackgroundService
{
    /// <summary>
    /// 类说明：任务执行次数
    /// </summary>
    public class TaskExecutionTimes
    {
        /// <summary>
        /// 第一次执行任务的时间
        /// </summary>
        public DateTime? FirstExecutionTaskTime { get; internal set; }
        /// <summary>
        /// 时间循环内第一次执行任务的时间
        /// </summary>
        public DateTime? FirstExecutionTaskTimeInLoop { get; internal set; }
        /// <summary>
        /// 时间循环内执行次数
        /// </summary>
        public ulong ExecutionTimesInLoop { get; private set; } = 0;
        /// <summary>
        /// 历史执行次数
        /// </summary>
        public ulong HistoryExecutionTimes { get; private set; } = 0;
        /// <summary>
        /// 增加一次执行次数
        /// </summary>
        internal void IncreraseOneTimes()
        {
            ExecutionTimesInLoop++;
            HistoryExecutionTimes++;
        }

        internal void ResetTimes()
        {
            ExecutionTimesInLoop = 0;
        }
    }
}
