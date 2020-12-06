using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace THBankAccount.Util.DataAnnotations
{
    /// <summary>
    /// 字符串转换成长整型数据验证特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class StringConvertLongAttribute : ValidationAttribute
    {
        /// <summary>
        /// 是否验证通过
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value is string)
            {
                return long.TryParse((string)value, out _);
            }
            return true;
        }
    }
}
