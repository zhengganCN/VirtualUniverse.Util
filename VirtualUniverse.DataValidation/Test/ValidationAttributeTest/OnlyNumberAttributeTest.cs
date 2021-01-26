using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using VirtualUniverse.DataValidation.ValidationAttributes;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/18 14:19:27；更新时间：
************************************************************************************/
namespace Test.ValidationAttributeTest
{
    /// <summary>
    /// 类 描 述：只包含数字测试
    /// </summary>
    class OnlyNumberAttributeTest
    {
        [Test]
        public void OnlyNumberValid()
        {
            OnlyNumberAttribute onlyNumberAttribute = new OnlyNumberAttribute();
            Assert.IsTrue(onlyNumberAttribute.IsValid("123"));
            Assert.IsFalse(onlyNumberAttribute.IsValid("123 df"));
            Assert.IsTrue(onlyNumberAttribute.IsValid(11));
        }
    }
}
