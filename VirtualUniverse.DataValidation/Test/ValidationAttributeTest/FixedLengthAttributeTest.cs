using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using VirtualUniverse.DataValidation.ValidationAttributes;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/18 11:33:42；更新时间：
************************************************************************************/
namespace Test.ValidationAttributeTest
{
    /// <summary>
    /// 类 描 述：字符串固定长度测试
    /// </summary>
    class FixedLengthAttributeTest
    {
        [Test]
        public void FixedLengthValid()
        {
            FixedLengthAttribute fixedLengthAttribute = new FixedLengthAttribute
            {
                Lengths = new int[] { 3, 4 }
            };
            Assert.IsTrue(fixedLengthAttribute.IsValid("111"));
            Assert.IsFalse(fixedLengthAttribute.IsValid("11"));
        }

        [Test]
        public void NoStringValid()
        {
            FixedLengthAttribute fixedLengthAttribute = new FixedLengthAttribute
            {
                Lengths = new int[] { 3, 4 }
            };
            Assert.IsTrue(fixedLengthAttribute.IsValid(null));
        }
    }
}
