using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace VirtualUniverse.DataValidation.ValidationAttributes
{
    /// <summary>
    /// 中文验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ChineseAttribute : ValidationAttribute
    {
        /// <summary>
        /// 是否验证通过
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            var pattern = "[\u4e00-\u9fa5]";
            Regex regex = new Regex(pattern);
            return regex.IsMatch((string)value);
        }
    }
}
