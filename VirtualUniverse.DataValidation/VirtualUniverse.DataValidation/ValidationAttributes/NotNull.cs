using System;
using System.ComponentModel.DataAnnotations;

namespace VirtualUniverse.DataValidation.ValidationAttributes
{
    /// <summary>
    /// 类 描 述：
    /// <br></br>
    /// 作    者 ：zhenggan ；创建时间 ：2020/12/24 17:05:47 ；更新时间 ：
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class NotNull : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return base.IsValid(value);
        }
    }
}
