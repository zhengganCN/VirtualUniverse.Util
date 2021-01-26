using System;
using System.ComponentModel.DataAnnotations;

namespace VirtualUniverse.DataValidation.ValidationAttributes
{
    /// <summary>
    /// 移动电话格式验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class CellPhoneAttribute : ValidationAttribute
    {
    }
}
