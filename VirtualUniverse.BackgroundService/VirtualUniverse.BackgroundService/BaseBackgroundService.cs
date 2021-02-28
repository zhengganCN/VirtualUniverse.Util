using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using VirtualUniverse.BackgroundService.Models;

namespace VirtualUniverse.BackgroundService
{
    /// <summary>
    /// 后台服务基类
    /// </summary>
    public abstract class BaseBackgroundService : IHostedService, IDisposable
    {
        private Task _executeTask;
        private bool disposedValue;
        private readonly BackgroundServiceBuilder backgroundServiceBuilder = new BackgroundServiceBuilder();

        /// <summary>
        /// 任务执行次数
        /// </summary>
        public TaskExecutionTimes ExecutionTimes { get; } = new TaskExecutionTimes();

        /// <summary>
        /// 构造器
        /// </summary>
        protected BaseBackgroundService()
        {
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            OnConfiguration(backgroundServiceBuilder);
            if (!backgroundServiceBuilder.StartNow)
            {
                backgroundServiceBuilder.StartUpDateTimeMode.Init();
            }
        }

        /// <summary>
        /// 后台服务配置
        /// </summary>
        /// <param name="backgroundServiceBuilder">配置项</param>
        protected abstract void OnConfiguration(BackgroundServiceBuilder backgroundServiceBuilder);

        /// <summary>
        /// 执行用户重写的耗时任务ExecuteAsync()与定时器的启动/停止操作
        /// </summary>
        private void ExecutionTask()
        {
            while (true)
            {
                var now = DateTime.Now;
                OnConfiguration(backgroundServiceBuilder);
                if (backgroundServiceBuilder.StartNow)
                {
                    if (WhenIsStartNowTheTaskCanExecutionTask())
                    {
                        ExecutionTaskAndRecordExecutionInfo(now);
                    }
                    else
                    {
                        Task.Delay(1000).Wait();
                    }
                }
                else
                {
                    backgroundServiceBuilder.StartUpDateTimeMode.Init();
                    if (backgroundServiceBuilder.IsRefreshIfExecutionEnough &&
                       ExecutionTimes.FirstExecutionTaskTimeInLoop.HasValue &&
                       backgroundServiceBuilder.StartUpDateTimeMode.IsOtherDateTimeLoop(
                           ExecutionTimes.FirstExecutionTaskTimeInLoop.Value, now))
                    {
                        ExecutionTimes.ResetTimes();
                    }
                    if (WhenNotStartNowTheTaskCanExecutionTask(now))
                    {
                        ExecutionTaskAndRecordExecutionInfo(now);
                    }
                    else
                    {
                        Task.Delay(1000).Wait();
                    }
                }
            }
        }

        /// <summary>
        /// 执行任务并记录执行信息
        /// </summary>
        /// <param name="now">执行任务的时间</param>
        private void ExecutionTaskAndRecordExecutionInfo(DateTime now)
        {
            ExecuteAsync().Wait();
            if (ExecutionTimes.HistoryExecutionTimes == 0)
            {
                ExecutionTimes.FirstExecutionTaskTime = now;
            }
            if (ExecutionTimes.ExecutionTimesInLoop == 0)
            {
                ExecutionTimes.FirstExecutionTaskTimeInLoop = now;
            }
            ExecutionTimes.IncreraseOneTimes();
            Task.Delay(backgroundServiceBuilder.Interval).Wait();
        }

        private bool WhenIsStartNowTheTaskCanExecutionTask()
        {
            if (backgroundServiceBuilder.ExecutionTimes >= 0)
            {
                if ((long)ExecutionTimes.ExecutionTimesInLoop < backgroundServiceBuilder.ExecutionTimes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 判断是否能够执行
        /// </summary>
        /// <param name="currentDatetime"></param>
        /// <returns></returns>
        private bool WhenNotStartNowTheTaskCanExecutionTask(DateTime currentDatetime)
        {
            if (backgroundServiceBuilder.ExecutionTimes >= 0)
            {
                if ((long)ExecutionTimes.ExecutionTimesInLoop < backgroundServiceBuilder.ExecutionTimes)
                {
                    return backgroundServiceBuilder.StartUpDateTimeMode
                        .IsCurrentTimeBetweenStartTimeAndEndTime(currentDatetime);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return backgroundServiceBuilder.StartUpDateTimeMode
                        .IsCurrentTimeBetweenStartTimeAndEndTime(currentDatetime);
            }
        }

        /// <summary>
        /// 启动任务是执行
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            _executeTask = Task.Run(() => ExecutionTask());
            // If the task is completed then return it, this will bubble cancellation and failure to the caller
            if (_executeTask.IsCompleted)
            {
                return _executeTask;
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 结束任务是执行
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        /// <summary>
        /// 用户需重写的函数，业务逻辑在本函数中实现
        /// </summary>
        /// <returns></returns>
        protected abstract Task ExecuteAsync();
        /// <summary>
        /// 销毁
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // 释放托管状态(托管对象)
                }

                disposedValue = true;
            }
        }
        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

}
