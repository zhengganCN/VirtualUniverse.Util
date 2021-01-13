using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/2 23:00:59；更新时间：
************************************************************************************/
namespace VirtualUniverse.DataValidation.ValidationAttributes
{
    /// <summary>
    /// 类说明：函数验证（调用自定义函数验证，自定义函数返回类型必须为布尔类型,且函数必须为静态函数）
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class FunctionAttribute : ValidationAttribute
    {
        private string AssemblyName { get; set; }
        private string ClassName { get; set; }
        private string MethodName { get; set; }
        /// <summary>
        /// 验证值是否允许Null或者空字符，默认值为 false
        /// </summary>
        public bool AllowNullOrEmpty { get; set; } = false;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        public FunctionAttribute(string assemblyName, string className, string methodName)
        {
            AssemblyName = assemblyName;
            ClassName = className;
            MethodName = methodName;
        }
        /// <summary>
        /// 重写验证逻辑
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            var result = false;
            if (AllowNullOrEmpty)
            {
                if (value is string)
                {
                    if (string.IsNullOrEmpty(value as string))
                    {
                        result = true;
                    }
                    else
                    {
                        result = ValidValue(value, result);
                    }
                }
                else
                {
                    if (value is null)
                    {
                        result = true;
                    }
                    else
                    {
                        result = ValidValue(value, result);
                    }
                }
            }
            else
            {
                result = ValidValue(value, result);
            }
            return result;
        }

        private bool ValidValue(object value, bool result)
        {
            var param = new object[] { value };
            try
            {
                var type = Assembly.Load(AssemblyName).GetType(AssemblyName + "." + ClassName);
                var method = type.GetMethod(MethodName);
                if (method.IsStatic)
                {
                    result = (method.Invoke(null, param) as bool?).GetValueOrDefault(false);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"程序集或类或方法找不到，详细错误：{ex}");
            }

            return result;
        }
    }
}
