using System;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/28 22:39:33；更新时间：
************************************************************************************/
namespace VirtualUniverse.BackgroundService.Models.StartUpModes
{
    /// <summary>
    /// 类说明：按周启动模式
    /// </summary>
    public class WeekStartUpDateTimeMode : StartUpDateTimeMode
    {
        /// <summary>
        /// 启动时间；格式 dayOfWeek;HH:mm:ss，且 1&lt;=int&lt;=7
        /// </summary>
        public override string StartTime { get; set; }
        /// <summary>
        /// 在每周的固定时间启动
        /// </summary>
        internal DateTime? StartTimeInWeek { get; private set; }
        /// <summary>
        /// 结束时间；格式 dayOfWeek;HH:mm:ss，且 1&lt;=int&lt;=7
        /// </summary>
        public override string EndTime { get; set; }
        /// <summary>
        /// 在每周的固定时间结束
        /// </summary>
        internal DateTime? EndTimeInWeek { get; private set; }

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
            StartTimeInWeek = TimeConvertDateTime(StartTime);
        }

        /// <summary>
        /// 结束时间，如果设置了StartNow为true，则EndTime无效
        /// </summary>
        /// <returns></returns>
        private void SetEndTime()
        {
            EndTimeInWeek = TimeConvertDateTime(EndTime);
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
            if (!int.TryParse(splits[0], out int dayOfWeek) || !(0 < dayOfWeek && dayOfWeek < 8))
            {
                throw new ArgumentException("符号“;”前的参数必须为数字；且值必须大于等于1，小于等于7");
            }
            var dateTimeOfDay = splits[1];
            if (!DateTime.TryParse(dateTimeOfDay, out DateTime result))
            {
                throw new ArgumentException("时间格式错误");
            }
            return GetDateOfStartWeek(DateTime.Now)
                .AddDays(dayOfWeek - 1)
                .AddHours(result.Hour)
                .AddMinutes(result.Minute)
                .AddSeconds(result.Second);
        }

        private DateTime GetDateOfStartWeek(DateTime now)
        {
            var nowDayOfWeek = now.DayOfWeek == 0 ? 7 : (int)now.DayOfWeek;
            return DateTime.Parse(now.AddDays(-Math.Abs(nowDayOfWeek - 1)).ToShortDateString());
        }

        internal override void ValidParams()
        {
            if (StartTimeInWeek > EndTimeInWeek)
            {
                throw new ArgumentException("启动时间不能大于结束时间");
            }
        }

        internal override bool IsCurrentTimeBetweenStartTimeAndEndTime(DateTime currentDatTime)
        {
            return StartTimeInWeek <= currentDatTime && currentDatTime <= EndTimeInWeek;
        }

        internal override bool IsOtherDateTimeLoop(DateTime firstExecutionTaskTimeInLoop, DateTime currentDateTime)
        {
            var dateOfStartWeek = GetDateOfStartWeek(currentDateTime);
            if (!(dateOfStartWeek <= firstExecutionTaskTimeInLoop &&
                firstExecutionTaskTimeInLoop <= dateOfStartWeek.AddDays(7).AddSeconds(-1)))
            {
                return true;
            }
            return false;
        }

    }
}
