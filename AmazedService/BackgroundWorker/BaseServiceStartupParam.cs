using System;
using System.Collections.Generic;
using System.Text;

namespace AmazedService.BackgroundWorker
{
    /// <summary>
    /// 服务启动参数
    /// </summary>
    public class BaseServiceStartupParam
    {
        /// <summary>
        /// 是否立即启动；是，则只有Interval参数有效，StartTime和EndTime无效；否，则必须设置StartTime
        /// </summary>
        public bool StartNow { get; set; }
        /// <summary>
        /// 间隔，单位毫秒；默认值为0，表示上一次任务执行完毕后会间隔多久执行下一次任务
        /// </summary>
        public int Interval { get; set; }
        /// <summary>
        /// 启动时间 (只包含时分秒不包括日期)
        /// 说明：
        /// 如果启动时间不为空，则表示程序会在当天的启动时间之后执行
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// 说明：
        /// 如果结束时间不为空，则表示程序会在当天的启动时间之前后执行
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
