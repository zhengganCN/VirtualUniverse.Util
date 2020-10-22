using System;
using System.Collections.Generic;
using System.Text;

namespace AmazedService.BackgroundWorker
{
    /// <summary>
    /// 服务启动参数
    /// </summary>
    public class ServiceStartupParam
    {
        /// <summary>
        /// 是否立即启动
        /// </summary>
        public bool StartNow { get; set; }
        /// <summary>
        /// 间隔，单位毫秒
        /// </summary>
        public int Interval { get; set; }
        /// <summary>
        /// 启动“时”
        /// </summary>
        public int StartHour { get; set; }
        /// <summary>
        /// 启动“分”
        /// </summary>
        public int StartMinite { get; set; }
        /// <summary>
        /// 启动“秒”
        /// </summary>
        public int StartSecond { get; set; }
    }
}
