using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using VirtualUniverse.DataValidation.ValidationAttributes;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/18 14:21:46；更新时间：
************************************************************************************/
namespace Test.ValidationAttributeTest
{
    /// <summary>
    /// 类 描 述：
    /// </summary>
    class StringConvertLongAttributeTest
    {

        [Test]
        public void OnlyNumberValid()
        {
            StringConvertLongAttribute stringConvertLongAttribute = new StringConvertLongAttribute();
            Assert.IsTrue(stringConvertLongAttribute.IsValid("123"));
            Assert.IsFalse(stringConvertLongAttribute.IsValid("123 df"));
            Assert.IsTrue(stringConvertLongAttribute.IsValid(11));
        }
    }
}
