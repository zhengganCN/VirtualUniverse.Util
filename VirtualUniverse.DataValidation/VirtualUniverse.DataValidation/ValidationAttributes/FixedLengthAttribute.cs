using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace VirtualUniverse.DataValidation.ValidationAttributes
{
    /// <summary>
    /// 字符串固定长度验证特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class FixedLengthAttribute : ValidationAttribute
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
            if (value is null)
            {
                return true;
            }
            bool result;
            if (value is string @string)
            {
                result= Lengths.Contains(@string.Length);
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}
