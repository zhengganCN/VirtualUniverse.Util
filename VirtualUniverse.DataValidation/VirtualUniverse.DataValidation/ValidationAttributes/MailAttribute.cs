using System;
using System.ComponentModel.DataAnnotations;

namespace VirtualUniverse.DataValidation.ValidationAttributes
{
    /// <summary>
    /// 邮箱验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class MailAttribute : ValidationAttribute
    {
    }
}
