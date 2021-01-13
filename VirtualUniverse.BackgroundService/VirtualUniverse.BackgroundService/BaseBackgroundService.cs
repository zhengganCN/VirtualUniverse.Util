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
        private bool disposedValue;
        private readonly BackgroundServiceBuilder backgroundServiceBuilder =new BackgroundServiceBuilder();
        private long ExecutionTimes { get; set; } = 0;
        /// <summary>
        /// 后台服务配置
        /// </summary>
        /// <param name="backgroundServiceBuilder"></param>
        protected virtual void OnConfiguration(BackgroundServiceBuilder backgroundServiceBuilder)
        {
        }

        /// <summary>
        /// 执行用户重写的耗时任务ExecuteAsync()与定时器的启动/停止操作
        /// </summary>
        private void Execute()
        {
            while (true)
            {
                OnConfiguration(backgroundServiceBuilder);
                if (backgroundServiceBuilder.ExecutionTimes >= 0 )
                {
                    if (backgroundServiceBuilder.ExecutionTimes > ExecutionTimes)
                    {
                        if (CanExecute())
                        {
                            ExecuteAsync().Wait();//等待耗时任务执行完毕
                            ExecutionTimes++;
                            Task.Delay(backgroundServiceBuilder.Interval).Wait();//用户设置的间隔时间
                        }
                        else
                        {
                            Task.Delay(1000).Wait();//等待一秒后执行
                        }
                    }
                    else
                    {
                        Task.Delay(1000).Wait();//等待一秒后执行
                    }
                }
                else
                {
                    if (CanExecute())
                    {
                        ExecuteAsync().Wait();//等待耗时任务执行完毕
                        Task.Delay(backgroundServiceBuilder.Interval).Wait();//用户设置的间隔时间
                    }
                    else
                    {
                        Task.Delay(1000).Wait();//等待一秒后执行
                    }
                }
            }
        }
        /// <summary>
        /// 判断是否能够执行
        /// </summary>
        /// <returns></returns>
        private bool CanExecute()
        {
            var now = DateTime.Now;
            if (backgroundServiceBuilder.StartNow)
            {
                return true;
            }
            else if (backgroundServiceBuilder.StartTime.HasValue && backgroundServiceBuilder.EndTime.HasValue)
            {
                if (backgroundServiceBuilder.StartTime >= backgroundServiceBuilder.EndTime)
                {
                    throw new ArgumentException($"参数{nameof(backgroundServiceBuilder.StartTime)}不能大于或等于{nameof(backgroundServiceBuilder.EndTime)}");
                }
                return backgroundServiceBuilder.StartTime <= now && now <= backgroundServiceBuilder.EndTime;
            }
            else if (backgroundServiceBuilder.StartTime.HasValue && (!backgroundServiceBuilder.EndTime.HasValue))
            {
                backgroundServiceBuilder.StartTime = DateTime.Today
                    .AddHours(backgroundServiceBuilder.StartTime.Value.Hour)
                    .AddMinutes(backgroundServiceBuilder.StartTime.Value.Minute)
                    .AddSeconds(backgroundServiceBuilder.StartTime.Value.Second);
                return now >= backgroundServiceBuilder.StartTime.Value;
            }
            else if ((!backgroundServiceBuilder.StartTime.HasValue) && backgroundServiceBuilder.EndTime.HasValue)
            {
                backgroundServiceBuilder.EndTime = DateTime.Today
                    .AddHours(backgroundServiceBuilder.EndTime.Value.Hour)
                    .AddMinutes(backgroundServiceBuilder.EndTime.Value.Minute)
                    .AddSeconds(backgroundServiceBuilder.EndTime.Value.Second);
                return now <= backgroundServiceBuilder.EndTime.Value;
            }
            else if ((!backgroundServiceBuilder.StartTime.HasValue) && (!backgroundServiceBuilder.EndTime.HasValue))
            {
                return backgroundServiceBuilder.StartNow;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 启动任务是执行
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => Execute()).ConfigureAwait(false);
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
