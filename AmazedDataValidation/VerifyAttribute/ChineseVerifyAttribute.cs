﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace AmazedDataValidation.VerifyAttribute
{
    public class ChineseVerifyAttribute : ValidationAttribute
    {
        public ChineseVerifyAttribute()
        {
            ErrorMessage = "请输入中文";
        }

        public override bool IsValid(object value)
        {
            var pattern = "[\u4e00-\u9fa5]";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(value.ToString());
        }
    }
}
