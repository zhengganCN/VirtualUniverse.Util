using System;
using System.Collections.Generic;
using System.Text;

namespace AmazedService.BackgroundWorker
{
    public class OnceServiceStartupParam
    {
        /// <summary>
        /// 是否立即启动；是，则只有Interval参数有效，StartTime无效；否，则必须设置StartTime
        /// </summary>
        public bool StartNow { get; set; }
        /// <summary>
        /// 启动时间 (只包含时分秒不包括日期)
        /// 说明：
        /// 如果启动时间不为空，则表示程序会在当天的启动时间之后执行
        /// </summary>
        public DateTime? StartTime { get; set; }
    }
}
