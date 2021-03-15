﻿using System;
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
        /// 设置要验证的值是全部都是中文，还是至少包含一个中文，默认值为至少包含一个中文
        /// </summary>
        public EnumChineseContainer ChineseContainer { get; set; } = EnumChineseContainer.Container;
        /// <summary>
        /// 是否验证通过
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value is null)
            {
                return true;
            }
            bool result;
            if (value is string chinese)
            {
                if (string.IsNullOrEmpty(chinese))
                {
                    result = false;
                }
                else
                {
                    result = ValidChinese(chinese);
                }
            }
            else
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 验证中文
        /// </summary>
        /// <param name="chinese"></param>
        /// <returns></returns>
        private bool ValidChinese(string chinese)
        {
            bool result = false;
            switch (ChineseContainer)
            {
                case EnumChineseContainer.Container:
                    result = IsContainerChineseAtLeastOne(chinese);
                    break;
                case EnumChineseContainer.All:
                    result = IsAllChinese(chinese);
                    break;
            }
            return result;
        }

        private bool IsContainerChineseAtLeastOne(string value)
        {
            var pattern = "[\u4e00-\u9fa5]+";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(value);
        }

        private bool IsAllChinese(string value)
        {
            var pattern = "[\u4e00-\u9fa5]";
            var matches = new Regex(pattern).Matches(value);
            return matches.Count == value.Length;
        }
        /// <summary>
        /// 枚举包含设置
        /// </summary>
        public enum EnumChineseContainer
        {
            /// <summary>
            /// 至少包含一个中文
            /// </summary>
            Container = 1,
            /// <summary>
            /// 全部是中文
            /// </summary>
            All = 2
        }
    }
}
