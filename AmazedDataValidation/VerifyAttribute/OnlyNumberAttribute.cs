using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace THBankAccount.Util.DataAnnotations
{
    /// <summary>
    /// 字符串只能包含数字特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class OnlyNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string)
            {
                return !Regex.IsMatch((string)value, @"^[0-9]");
            }
            else
            {
                return true;
            }
        }
    }
}
