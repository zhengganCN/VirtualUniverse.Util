using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace AmazedDataValidation.VerifyAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ChineseAttribute : ValidationAttribute
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
