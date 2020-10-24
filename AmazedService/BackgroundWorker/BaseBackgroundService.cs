using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AmazedService.BackgroundWorker
{
    /// <summary>
    /// 后台服务基类
    /// </summary>
    public abstract class BaseBackgroundService : IHostedService, IDisposable
    {
        private readonly Timer _timer;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="param">服务启动参数</param>
        public BaseBackgroundService(ServiceStartupParam param)
        {
            var dueTime = 0;
            if (!param.StartNow)
            {
                var now = DateTime.Now;
                DateTime startTime = DateTime.Today.AddHours(param.StartHour).AddMinutes(param.StartMinite).AddSeconds(param.StartMinite);
                if (now > startTime)
                {
                    startTime = startTime.AddDays(1);
                }
                dueTime = (int)((startTime - now).TotalMilliseconds);
            }
            _timer = new Timer(Execute, null, dueTime, param.Interval);
        }
        /// <summary>
        /// 销毁
        /// </summary>
        public virtual void Dispose()
        {
            _timer.Dispose();
        }
        /// <summary>
        /// 执行耗时任务
        /// </summary>
        /// <param name="state"></param>
        private void Execute(object state)
        {
            ExecuteAsync().Wait();
        }
        /// <summary>
        /// 启动任务
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        /// <summary>
        /// 结束任务
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        /// <summary>
        /// 耗时任务
        /// </summary>
        /// <returns></returns>
        protected abstract Task ExecuteAsync();
    }
}
