using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/2/27 14:23:08；更新时间：
************************************************************************************/
namespace VirtualUniverse.Extension.System
{
    /// <summary>
    /// 枚举扩展类
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            var field = value.GetType().GetField(value.ToString());
            var description = ((DescriptionAttribute)field.GetCustomAttribute(typeof(DescriptionAttribute)))?.Description;
            return !string.IsNullOrEmpty(description) ? description : field.Name;
        }
    }
}
