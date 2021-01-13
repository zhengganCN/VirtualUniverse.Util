using System;
using System.ComponentModel.DataAnnotations;
using VirtualUniverse.DataValidation.ValidationServices;

namespace VirtualUniverse.DataValidation.ValidationAttributes
{
    /// <summary>
    /// ID卡格式验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class IDCardAttribute : ValidationAttribute
    {
        /// <summary>
        /// 卡类型（默认值为身份证）
        /// </summary>
        public EnumIDCardType CardType { get; set; } = EnumIDCardType.IdentityNumber;
        /// <summary>
        /// 是否验证通过
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value is string)
            {
                var number = value as string;
                var result = false;
                switch (CardType)
                {
                    case EnumIDCardType.IdentityNumber:
                        result = ValidIdentityNumber(number);
                        break;
                    default:
                        break;
                }
                return result;
            }
            else
            {
                return true;
            }
        }

        private bool ValidIdentityNumber(string number)
        {
            return IDNumberVerification.ValidIDNumber(number);
        }
    }
    /// <summary>
    /// ID卡类型
    /// </summary>
    public enum EnumIDCardType
    {
        /// <summary>
        /// 身份证
        /// </summary>
        IdentityNumber = 1
    }
}
