using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace THBankAccount.Util.DataAnnotations
{
    /// <summary>
    /// 字符串没有空格验证特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NoSpaceAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string)
            {
                return !((string)value).Contains(' ');
            }
            return false;
        }
    }
}
