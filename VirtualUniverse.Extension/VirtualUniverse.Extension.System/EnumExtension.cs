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

        #region 获取枚举字典
        /// <summary>
        /// 获取枚举字典
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="nameSource">名称来源，默认从描述特性中读取，如果不存在描述特性，则读取枚举名称</param>
        /// <returns></returns>
        public static Dictionary<int, string> GetEnumDictionary<T>(EnumNameSource nameSource = EnumNameSource.EnumDescriptionAttribute) where T : Enum
        {
            Dictionary<int, string> dictionary;
            switch (nameSource)
            {
                case EnumNameSource.EnumName:
                    dictionary = GetEnumDictionaryFromName<T>();
                    break;
                case EnumNameSource.EnumDescriptionAttribute:
                    dictionary = GetEnumDictionaryFromDescriptionAttribute<T>();
                    break;
                default:
                    dictionary = GetEnumDictionaryFromDescriptionAttribute<T>();
                    break;
            }
            return dictionary;
        }

        /// <summary>
        /// 从枚举名称中读取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static Dictionary<int, string> GetEnumDictionaryFromName<T>() where T : Enum
        {
            var enumType = typeof(T);
            var values = Enum.GetValues(enumType);
            var dictionary = new Dictionary<int, string>();
            foreach (var value in values)
            {
                dictionary.Add((int)value, value.ToString());
            }
            return dictionary;
        }

        /// <summary>
        /// 从描述特性中读取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static Dictionary<int, string> GetEnumDictionaryFromDescriptionAttribute<T>() where T : Enum
        {
            var dictionary = GetEnumDictionaryFromName<T>();
            var enumType = typeof(T);
            var fieldInfos = enumType.GetFields();
            foreach (var fieldInfo in fieldInfos)
            {
                if (fieldInfo.FieldType == enumType)
                {
                    var key = (int)fieldInfo.GetValue(enumType);
                    if (dictionary.ContainsKey(key))
                    {
                        var description = fieldInfo.GetCustomAttribute<DescriptionAttribute>();
                        if (description != null && !string.IsNullOrWhiteSpace(description.Description))
                        {
                            dictionary[key] = description.Description;
                        }
                    }
                }
            }
            return dictionary;
        }

        /// <summary>
        /// 枚举名称来源
        /// </summary>
        public enum EnumNameSource
        {
            /// <summary>
            /// 从枚举的名称获取
            /// </summary>
            EnumName = 1,
            /// <summary>
            /// 从枚举的描述特性获取，即[Description]特性
            /// </summary>
            EnumDescriptionAttribute = 2
        }
        #endregion
    }
}
