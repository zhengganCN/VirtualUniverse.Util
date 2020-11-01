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
    /// 每日执行一次的后台服务基类
    /// </summary>
    public abstract class OnceBackgroundService : IHostedService, IDisposable
    {
        private Timer _timer;
        private OnceServiceStartupParam _param;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="logger"></param>
        public OnceBackgroundService(Func<OnceServiceStartupParam> func, ILogger<OnceBackgroundService> logger)
        {
            _param = func.Invoke();
            var now = DateTime.Now;
            if (_param.StartNow == true)
            {
                _param.StartTime = now;
            }
            if (!_param.StartTime.HasValue)
            {
                throw new ArgumentNullException($"{nameof(_param.StartTime)}不能为空");
            }
            var startTime = _param.StartTime.Value;
            if (now.Hour == startTime.Hour && now.Minute == startTime.Minute && now.Second == startTime.Second)
            {

            }
            _timer = new Timer(Execute, null, 0, Timeout.Infinite);
        }
        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            _timer.Dispose();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        private void Execute(object state)
        {
            while (true)
            {
                ExecuteAsync().Wait();
                var dueTime = (DateTime.Today.AddDays(1) - DateTime.Now).TotalMilliseconds;
                Task.Delay((int)dueTime).Wait();
            }
        }


        public Task StopAsync(CancellationToken cancellationToken)
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
