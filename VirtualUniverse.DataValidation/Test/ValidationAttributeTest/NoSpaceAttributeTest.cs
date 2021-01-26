using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using VirtualUniverse.DataValidation.ValidationAttributes;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/18 14:16:21；更新时间：
************************************************************************************/
namespace Test.ValidationAttributeTest
{
    /// <summary>
    /// 类 描 述：不包含空格测试
    /// </summary>
    class NoSpaceAttributeTest
    {
        [Test]
        public void NoSpaceValid()
        {
            NoSpaceAttribute noSpaceAttribute = new NoSpaceAttribute();
            Assert.IsTrue(noSpaceAttribute.IsValid("asdfsdf"));
            Assert.IsFalse(noSpaceAttribute.IsValid("asdfss df"));
            Assert.IsTrue(noSpaceAttribute.IsValid(11));
        }
    }
}
