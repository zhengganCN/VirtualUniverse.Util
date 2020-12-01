using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AmazedDataValidation.VerifyAttribute
{
    /// <summary>
    /// 移动电话验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CellPhoneAttribute: ValidationAttribute
    {
    }
}
