using System;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/28 22:40:43；更新时间：
************************************************************************************/
namespace VirtualUniverse.BackgroundService.Models.StartUpModes
{
    /// <summary>
    /// 类说明：按月启动模式
    /// </summary>
    public class MonthStartUpDateTimeMode : StartUpDateTimeMode
    {
        /// <summary>
        /// 启动时间；格式 dayOfMonth;HH:mm:ss，且 dayOfMonth为 1&lt;=int&lt;=28
        /// </summary>
        public override string StartTime { get; set; }
        /// <summary>
        /// 在每月的固定时间启动
        /// </summary>
        internal DateTime? StartTimeInMonth { get; private set; }
        /// <summary>
        /// 结束时间；格式 dayOfMonth;HH:mm:ss，且 dayOfMonth为 1&lt;=int&lt;=28
        /// </summary>
        public override string EndTime { get; set; }
        /// <summary>
        /// 在每月的固定时间结束
        /// </summary>
        internal DateTime? EndTimeInMonth { get; private set; }

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
            StartTimeInMonth = TimeConvertDateTime(StartTime);
        }

        /// <summary>
        /// 结束时间，如果设置了StartNow为true，则EndTime无效
        /// </summary>
        /// <returns></returns>
        private void SetEndTime()
        {

            EndTimeInMonth = TimeConvertDateTime(EndTime);
        }

        private DateTime? TimeConvertDateTime(string time)
        {
            if (string.IsNullOrEmpty(time))
            {
                throw new ArgumentException("时间不能为空");
            }
            var splits = time.Split(';');
            if (splits.Length != 2)
            {
                throw new ArgumentException("请按格式填写时间参数，缺少符号“;”");
            }
            if (!int.TryParse(splits[0], out int dayOfMonth) || !(0 < dayOfMonth && dayOfMonth < 29))
            {
                throw new ArgumentException("符号“;”前的参数必须为数字；且值必须大于等于1，小于等于28");
            }
            var dateTimeOfDay = splits[1];
            if (!DateTime.TryParse(dateTimeOfDay, out DateTime result))
            {
                throw new ArgumentException("时间格式错误");
            }
            return DateTime.Parse(DateTime.Now.ToString("yyyy-MM"))
                .AddDays(dayOfMonth - 1)
                .AddHours(result.Hour)
                .AddMinutes(result.Minute)
                .AddSeconds(result.Second);
        }

        internal override void ValidParams()
        {
            if (StartTimeInMonth > EndTimeInMonth)
            {
                throw new ArgumentException("启动时间不能大于结束时间");
            }
        }

        internal override bool IsCurrentTimeBetweenStartTimeAndEndTime(DateTime currentDatTime)
        {
            return StartTimeInMonth <= currentDatTime && currentDatTime <= EndTimeInMonth;
        }

        internal override bool IsOtherDateTimeLoop(DateTime firstExecutionTaskTimeInLoop, DateTime currentDateTime)
        {
            if (firstExecutionTaskTimeInLoop.Year != currentDateTime.Year ||
                firstExecutionTaskTimeInLoop.Month != currentDateTime.Month)
            {
                return true;
            }
            return false;
        }
    }
}
