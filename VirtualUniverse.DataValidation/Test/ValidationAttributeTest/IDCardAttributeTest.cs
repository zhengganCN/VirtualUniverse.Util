using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using VirtualUniverse.DataValidation.ValidationAttributes;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/18 11:56:18；更新时间：
************************************************************************************/
namespace Test.ValidationAttributeTest
{
    /// <summary>
    /// 类 描 述：身份证验证测试
    /// </summary>
    class IDCardAttributeTest
    {
        [Test]
        public void IdCardValid()
        {
            IDCardAttribute iDCardAttribute = new IDCardAttribute
            {
                CardType = IDCardAttribute.EnumIDCardType.IdentityNumber
            };
            Assert.IsTrue(iDCardAttribute.IsValid("110101199003070652"));
            Assert.IsFalse(iDCardAttribute.IsValid("11010119900307qwer"));
        }

        [Test]
        public void DontStringValid()
        {
            IDCardAttribute iDCardAttribute = new IDCardAttribute();
            Assert.IsFalse(iDCardAttribute.IsValid(null));
            Assert.IsFalse(iDCardAttribute.IsValid(11));
        }
    }
}
