using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace VirtualUniverse.DataValidation.ValidationAttributes
{
    /// <summary>
    /// 固定长度验证特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class FixLengthAttribute : ValidationAttribute
    {
        /// <summary>
        /// 字符串长度
        /// </summary>
        public int[] Lengths { get; set; }
        /// <summary>
        /// 是否验证通过
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value is string @string)
            {
                return Lengths.Contains(@string.Length);
            }
            return true;
        }
    }
}
