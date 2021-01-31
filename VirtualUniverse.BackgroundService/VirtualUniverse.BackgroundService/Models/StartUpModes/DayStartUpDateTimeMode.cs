using System;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/28 22:27:25；更新时间：
************************************************************************************/
namespace VirtualUniverse.BackgroundService.Models
{
    /// <summary>
    /// 类说明：按天启动模式
    /// </summary>
    public class DayStartUpDateTimeMode : StartUpDateTimeMode
    {
        /// <summary>
        /// 启动时间；格式 HH:mm:ss
        /// </summary>
        public override string StartTime { get; set; }
        internal DateTime? StartTimeInDay { get; private set; }
        /// <summary>
        /// 结束时间；格式 HH:mm:ss
        /// </summary>
        public override string EndTime { get; set; }
        internal DateTime? EndTimeInDay { get; private set; }

        internal override void Init()
        {
            SetStartTime();
            SetEndTime();
            ValidParams();
        }

        /// <summary>
        /// 启动时间，如果设置了StartNow为true，则StartTime无效
        /// </summary>
        /// <returns></returns>
        private void SetStartTime()
        {
            StartTimeInDay = TimeConvertDateTime(StartTime)?? DateTime.Parse(DateTime.Now.ToShortDateString());
        }

        /// <summary>
        /// 结束时间，如果设置了StartNow为true，则EndTime无效
        /// </summary>
        /// <returns></returns>
        private void SetEndTime()
        {
            EndTimeInDay = TimeConvertDateTime(EndTime) ?? DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
        }

        private DateTime? TimeConvertDateTime(string time)
        {
            if (string.IsNullOrEmpty(time))
            {
                return null;
            }
            if (!DateTime.TryParse(time, out DateTime result))
            {
                throw new ArgumentException("时间格式错误");
            }
            return DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"))
                .AddHours(result.Hour)
                .AddMinutes(result.Minute)
                .AddSeconds(result.Second);
        }

        internal override void ValidParams()
        {
            if (StartTimeInDay > EndTimeInDay)
            {
                throw new ArgumentException("启动时间不能大于结束时间");
            }
        }

        internal override bool IsCurrentTimeBetweenStartTimeAndEndTime(DateTime currentDatTime)
        {
            return StartTimeInDay <= currentDatTime && currentDatTime <= EndTimeInDay;
        }

        internal override bool IsOtherDateTimeLoop(DateTime firstExecutionTaskTimeInLoop, DateTime currentDateTime)
        {
            if (firstExecutionTaskTimeInLoop.Year != currentDateTime.Year ||
                firstExecutionTaskTimeInLoop.Month != currentDateTime.Month ||
                firstExecutionTaskTimeInLoop.Day != currentDateTime.Day)
            {
                return true;
            }
            return false;
        }
    }
}
