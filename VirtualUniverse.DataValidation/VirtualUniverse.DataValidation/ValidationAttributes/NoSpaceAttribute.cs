using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace VirtualUniverse.DataValidation.ValidationAttributes
{
    /// <summary>
    /// 字符串没有空格验证特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class NoSpaceAttribute : ValidationAttribute
    {
        /// <summary>
        /// 是否验证通过
        /// </summary>
        /// <param name="value">验证值</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            return !(value is string @string) || !@string.Contains(' ');
        }
    }
}
