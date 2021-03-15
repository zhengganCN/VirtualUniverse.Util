using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/3 9:55:54；更新时间：
************************************************************************************/
namespace VirtualUniverse.DataValidation.ValidationAttributes
{
    /// <summary>
    /// 类说明：允许的对象值验证特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class AllowObjectValueAttribute : ValidationAttribute
    {
        private EnumObjectType ObjectType { get; set; }
        /// <summary>
        /// 允许的对象
        /// </summary>
        private object[] AllowObjects { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="allowObjects">允许的对象</param>
        /// <param name="objectType">允许的对象类型</param>
        public AllowObjectValueAttribute(object[] allowObjects, EnumObjectType objectType = EnumObjectType.String)
        {
            AllowObjects = allowObjects;
            ObjectType = objectType;
        }
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
            return ValidObject(value);
        }

        private bool ValidObject(object value)
        {
            var result = false;
            switch (ObjectType)
            {
                case EnumObjectType.Int32:
                    if (value is int @int)
                    {
                        result = ObjectToInt32().Any(entity => entity == @int);
                    }
                    break;
                case EnumObjectType.Int64:
                    if (value is long @long)
                    {
                        result = ObjectToInt64().Any(entity => entity == @long);
                    }
                    break;
                case EnumObjectType.String:
                    if (value is string @string)
                    {
                        result = ObjectToString().Any(entity => entity == @string);
                    }
                    break;
                case EnumObjectType.Char:
                    if (value is char @char)
                    {
                        result = ObjectToChar().Any(entity => entity == @char);
                    }
                    break;
                case EnumObjectType.Double:
                    if (value is double @double)
                    {
                        result = ObjectToDouble().Any(entity => entity == @double);
                    }
                    break;
            }
            return result;
        }

        private int[] ObjectToInt32()
        {
            var result = new int[AllowObjects.Length];
            for (int i = 0; i < AllowObjects.Length; i++)
            {
                result[i] = Convert.ToInt32(AllowObjects[i]);
            }
            return result;
        }

        private long[] ObjectToInt64()
        {
            var result = new long[AllowObjects.Length];
            for (int i = 0; i < AllowObjects.Length; i++)
            {
                result[i] = Convert.ToInt64(AllowObjects[i]);
            }
            return result;
        }

        private string[] ObjectToString()
        {
            var result = new string[AllowObjects.Length];
            for (int i = 0; i < AllowObjects.Length; i++)
            {
                result[i] = Convert.ToString(AllowObjects[i]);
            }
            return result;
        }

        private char[] ObjectToChar()
        {
            var result = new char[AllowObjects.Length];
            for (int i = 0; i < AllowObjects.Length; i++)
            {
                result[i] = Convert.ToChar(AllowObjects[i]);
            }
            return result;
        }

        private double[] ObjectToDouble()
        {
            var result = new double[AllowObjects.Length];
            for (int i = 0; i < AllowObjects.Length; i++)
            {
                result[i] = Convert.ToDouble(AllowObjects[i]);
            }
            return result;
        }

        /// <summary>
        /// 枚举允许验证的对象类型
        /// </summary>
        public enum EnumObjectType
        {
            /// <summary>
            /// 32位整型
            /// </summary>
            Int32 = 1,
            /// <summary>
            /// 32位整型
            /// </summary>
            Int64 = 2,
            /// <summary>
            /// 字符串
            /// </summary>
            String = 3,
            /// <summary>
            /// 单字符
            /// </summary>
            Char = 4,
            /// <summary>
            /// 双精度浮点数
            /// </summary>
            Double = 5
        }
    }
}
