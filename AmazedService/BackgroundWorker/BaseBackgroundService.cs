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
    /// 后台服务基类（定时任务）
    /// </summary>
    public abstract class BaseBackgroundService : IHostedService, IDisposable
    {
        private readonly Timer _timer;
        private readonly Func<BaseServiceStartupParam> _func;
        private BaseServiceStartupParam _param;
        private readonly ILogger<BaseBackgroundService> _logger;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="func">服务启动参数</param>
        /// <param name="logger">日志</param>
        protected BaseBackgroundService(Func<BaseServiceStartupParam> func, ILogger<BaseBackgroundService> logger)
        {
            _func = func;
            _param = func.Invoke();
            _timer = new Timer(Execute, null, 0, Timeout.Infinite);
            _logger = logger;
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
            while (CanExecuteTask())
            {
                try
                {
                    _param = _func.Invoke();
                    ExecuteAsync().Wait();
                    _param = _func.Invoke();
                    Task.Delay(_param.Interval).Wait();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                    Task.Delay(1000).Wait();
                }
            }
           
        }
        private bool CanExecuteTask()
        {
            var now = DateTime.Now;
            bool result = false;
            if (_param.StartNow)
            {
                result = true;
            }
            else
            {
                if (_param.StartTime.HasValue && _param.EndTime.HasValue)
                {
                    var startTime = DateTime.Today.AddHours(_param.StartTime.Value.Hour).AddMinutes(_param.StartTime.Value.Minute).AddSeconds(_param.StartTime.Value.Second);
                    var endTime = DateTime.Today.AddHours(_param.EndTime.Value.Hour).AddMinutes(_param.EndTime.Value.Minute).AddSeconds(_param.EndTime.Value.Second);
                    result = startTime <= now && now <= endTime;
                }
                else if (_param.StartTime.HasValue && (!_param.EndTime.HasValue))
                {
                    var startTime = DateTime.Today.AddHours(_param.StartTime.Value.Hour).AddMinutes(_param.StartTime.Value.Minute).AddSeconds(_param.StartTime.Value.Second);
                    result = startTime <= now;
                }
                else if (!_param.StartTime.HasValue)
                {
                    result = false;
                }
            }
            return result;
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
