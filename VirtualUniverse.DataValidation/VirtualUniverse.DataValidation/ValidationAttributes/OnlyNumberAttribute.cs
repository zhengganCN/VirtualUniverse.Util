using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace VirtualUniverse.DataValidation.ValidationAttributes
{
    /// <summary>
    /// 字符串只能包含数字特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class OnlyNumberAttribute : ValidationAttribute
    {
        /// <summary>
        /// 是否验证通过
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value is string @string)
            {
                return !Regex.IsMatch(@string, @"[^0-9]");
            }
            return true;
        }
    }
}
