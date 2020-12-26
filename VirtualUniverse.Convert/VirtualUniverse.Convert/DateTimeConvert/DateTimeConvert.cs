using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualUniverse.Convert.DateTimeConvert
{
    /// <summary>
    /// 时间转换类
    /// </summary>
    public static class DateTimeConvert
    {
        /// <summary>
        /// 获取给定月份的周数
        /// </summary>
        /// <param name="dateTime">要获取周数的年月</param>
        /// <returns></returns>
        public static int GetCurrentMonthWeekCount(DateTime dateTime)
        {
            var firstDayOfMonth=dateTime.AddDays(-(dateTime.Day - 1));//获取当月的第一天
            var dayCountOfMonth = DateTime.DaysInMonth(firstDayOfMonth.Year, firstDayOfMonth.Month);//获取指定年月的天数
            var weeksCount = 0;//获取指定年月的周数
            for (int i = 0; i < dayCountOfMonth; i++)
            {
                if (firstDayOfMonth.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                {
                    weeksCount++;
                }
            }
            if (firstDayOfMonth.AddDays(dayCountOfMonth - 1).DayOfWeek != DayOfWeek.Sunday)
            {
                weeksCount++;
            }
            return weeksCount;
        }
        /// <summary>
        /// 获取给定月份的周索引，从1开始
        /// </summary>
        /// <param name="dateTime">要获取周索引的年月</param>
        /// <returns></returns>
        public static int GetCurrentMonthWeekIndex(DateTime dateTime)
        {
            var firstDayOfMonth = dateTime.AddDays(-(dateTime.Day - 1));//获取当月的第一天
            var dayCountOfMonth = DateTime.DaysInMonth(firstDayOfMonth.Year, firstDayOfMonth.Month);//获取指定年月的天数
            var weekIndex = 0;//获取指定年月的周数
            for (int i = 0; i < dayCountOfMonth; i++)
            {
                if (firstDayOfMonth.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                {
                    if (firstDayOfMonth.AddDays(i) > dateTime)
                    {
                        break;
                    }
                    weekIndex++;
                }
            }
            if (firstDayOfMonth.AddDays(dayCountOfMonth - 1).DayOfWeek != DayOfWeek.Sunday)
            {
                weekIndex++;
            }
            return weekIndex;
        }
        /// <summary>
        /// 获取给定年月的给定周的起止日期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="week">周，从1开始</param>
        /// <returns>
        /// 第一项：给定周的开始时间
        /// 第二项：给定周的结束时间
        /// </returns>
        public static Tuple<DateTime, DateTime> GetCurrentWeekStartEndTime(DateTime dateTime, int week)
        {
            DateTime weekStartDate;//指定周的开始时间
            DateTime weekEndDate;//指定周的结束时间
            var firstDayOfMonth = dateTime.AddDays(-(dateTime.Day - 1));//获取当月的第一天
            weekStartDate = firstDayOfMonth;
            weekEndDate = firstDayOfMonth.AddDays(6);//临时赋值
            var dayCountOfMonth = DateTime.DaysInMonth(firstDayOfMonth.Year, firstDayOfMonth.Month);//获取指定年月的天数
            var weeksCount = 0;//获取指定年月的周数
            for (int i = 0; i < dayCountOfMonth; i++)
            {
                if (firstDayOfMonth.AddDays(i).DayOfWeek == DayOfWeek.Sunday)
                {
                    weeksCount++;
                    if (weeksCount == week)
                    {
                        weekEndDate = firstDayOfMonth.AddDays(i);
                        GetCurrentWeekEndTime(ref weekStartDate, ref weekEndDate, firstDayOfMonth);
                    }
                }
            }
            if (firstDayOfMonth.AddDays(dayCountOfMonth - 1).DayOfWeek != DayOfWeek.Sunday)
            {
                weeksCount++;
                if (weeksCount == week)
                {
                    weekEndDate = firstDayOfMonth.AddMonths(1).AddDays(-1);
                    GetCurrentWeekEndTime(ref weekStartDate, ref weekEndDate, firstDayOfMonth);
                }
            }
            if (week > weeksCount || week <= 0)
            {
                throw new ArgumentException(nameof(week) + "参数无效");
            }
            return new Tuple<DateTime, DateTime>(weekStartDate, weekEndDate);
        }

        private static void GetCurrentWeekEndTime(ref DateTime weekStartDate, ref DateTime weekEndDate, DateTime firstDayOfMonth)
        {
            for (int dayIndexOfInWeek = 1; dayIndexOfInWeek < 7; dayIndexOfInWeek++)
            {
                var currentDate = weekEndDate.AddDays(-dayIndexOfInWeek);
                if (currentDate.Month != firstDayOfMonth.Month)
                {
                    weekStartDate = currentDate.AddDays(1);
                    break;
                }
                else if (currentDate.DayOfWeek == DayOfWeek.Monday)
                {
                    weekStartDate = currentDate;
                    break;
                }
            }
        }
    }
}
