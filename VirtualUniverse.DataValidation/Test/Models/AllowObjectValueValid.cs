using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VirtualUniverse.DataValidation.ValidationAttributes;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/3/15 9:41:36；更新时间：
************************************************************************************/
namespace Test.Models
{
    /// <summary>
    /// 类 描 述：允许值验证模型
    /// </summary>
    class AllowObjectValueValid
    {
        [Display(Name = "账号")]
        [Required(ErrorMessage = "{0}为必填项")]
        [AllowObjectValue(new object[] { 1, 2 }, AllowObjectValueAttribute.EnumObjectType.Int32, ErrorMessage = "{0}的值不合法")]
        public int? Account { get; set; }
    }
}
