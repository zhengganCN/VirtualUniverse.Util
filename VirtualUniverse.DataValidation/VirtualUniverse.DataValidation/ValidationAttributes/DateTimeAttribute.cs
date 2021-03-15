using System;
using System.ComponentModel.DataAnnotations;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/13 11:43:04；更新时间：
************************************************************************************/
namespace VirtualUniverse.DataValidation.ValidationAttributes
{
    /// <summary>
    /// 类 描 述：时间格式验证，验证的参数只能是字符串
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DateTimeAttribute : ValidationAttribute
    {
        /// <summary>
        /// 验证的时间类型
        /// </summary>
        public EnumTimeFormat TimeFormat { get; set; } = EnumTimeFormat.DateTime;
        /// <summary>
        /// 重写验证逻辑
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value is null)
            {
                return true;
            }
            if (value is string datatime)
            {
                if (string.IsNullOrEmpty(datatime))
                {
                    return false;
                }
                else
                {
                    return DateTimeValid(datatime);
                }
            }
            else
            {
                return false;
            }
        }

        private bool DateTimeValid(string dateTimeString)
        {
            var result = false;
            switch (TimeFormat)
            {
                case EnumTimeFormat.DateTime:
                    result = TryParseDateTime(dateTimeString);
                    break;
                case EnumTimeFormat.Date:
                    result = TryParseDateTime(dateTimeString);
                    break;
                case EnumTimeFormat.Time:
                    result = TryParseDateTime(dateTimeString);
                    break;
                case EnumTimeFormat.DateTimeNoSeparator:
                    if (ValidDateTimeStringLength(dateTimeString, EnumTimeFormat.DateTimeNoSeparator))
                    {
                        result = TryParseDateTime(dateTimeString.Insert(12, ":").Insert(10, ":").Insert(8, " ").Insert(6, "/").Insert(4, "/"));
                    }
                    break;
                case EnumTimeFormat.DateNoSeparator:
                    if (ValidDateTimeStringLength(dateTimeString, EnumTimeFormat.DateNoSeparator))
                    {
                        result = TryParseDateTime(dateTimeString.Insert(6, "/").Insert(4, "/"));
                    }
                    break;
                case EnumTimeFormat.TimeNoSeparator:
                    if (ValidDateTimeStringLength(dateTimeString, EnumTimeFormat.TimeNoSeparator))
                    {
                        result = TryParseDateTime(dateTimeString.Insert(4, ":").Insert(2, ":"));
                    }
                    break;
            }
            return result;
        }

        private bool ValidDateTimeStringLength(string value,EnumTimeFormat format)
        {
            var result = false;
            switch (format)
            {
                case EnumTimeFormat.DateTimeNoSeparator:
                    result = value.Length == 14;
                    break;
                case EnumTimeFormat.DateNoSeparator:
                    result = value.Length == 8;
                    break;
                case EnumTimeFormat.TimeNoSeparator:
                    result = value.Length == 6;
                    break;
            }
            return result;
        }

        private static bool TryParseDateTime(string dateTimeString)
        {
            bool result;
            if (DateTime.TryParse(dateTimeString, out DateTime _))
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 时间格式枚举
        /// </summary>
        public enum EnumTimeFormat
        {
            /// <summary>
            /// 日期时间
            /// </summary>
            DateTime = 1,
            /// <summary>
            /// 日期
            /// </summary>
            Date = 2,
            /// <summary>
            /// 时间
            /// </summary>
            Time = 3,
            /// <summary>
            /// 日期时间-无分隔符(如“yyyyMMddhhmmss”)
            /// </summary>
            DateTimeNoSeparator = 4,
            /// <summary>
            /// 日期-无分隔符(如“yyyyMMdd”)
            /// </summary>
            DateNoSeparator = 5,
            /// <summary>
            /// 时间-无分隔符(如“hhmmss”)
            /// </summary>
            TimeNoSeparator = 6
        }
    }
}
