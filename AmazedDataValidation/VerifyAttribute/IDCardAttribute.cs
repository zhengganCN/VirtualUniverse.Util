using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AmazedDataValidation.VerifyAttribute
{
    /// <summary>
    /// ID卡格式验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IDCardAttribute : ValidationAttribute
    {
        public IDCardAttribute()
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(name);
        }

        public override bool IsValid(object value)
        {
            return base.IsValid(value);
        }

    }
    /// <summary>
    /// ID卡类型
    /// </summary>
    public enum IDCardType
    {
        IdentityNumber=1
    }
}
