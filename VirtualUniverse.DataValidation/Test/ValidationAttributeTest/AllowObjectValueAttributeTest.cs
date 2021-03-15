using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Models;
using VirtualUniverse.DataValidation.ValidationAttributes;
using VirtualUniverse.DataValidation.ValidationModel;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/18 11:42:11；更新时间：
************************************************************************************/
namespace Test.ValidationAttributeTest
{
    /// <summary>
    /// 类 描 述：允许的对象值验证测试
    /// </summary>
    class AllowObjectValueAttributeTest
    {
        [Test]
        public void AllowCharValueValid()
        {
            AllowObjectValueAttribute allowObjectValueAttribute = new AllowObjectValueAttribute(new object[] { 'A' }, AllowObjectValueAttribute.EnumObjectType.Char);
            Assert.IsFalse(allowObjectValueAttribute.IsValid("123"));
            Assert.IsTrue(allowObjectValueAttribute.IsValid('A'));
        }

        [Test]
        public void AllowStringValueValid()
        {
            AllowObjectValueAttribute allowObjectValueAttribute = new AllowObjectValueAttribute(new object[] { "AA" }, AllowObjectValueAttribute.EnumObjectType.String);
            Assert.IsFalse(allowObjectValueAttribute.IsValid("123"));
            Assert.IsTrue(allowObjectValueAttribute.IsValid("AA"));
        }

        [Test]
        public void AllowInt32ValueValid()
        {
            AllowObjectValueAttribute allowObjectValueAttribute = new AllowObjectValueAttribute(new object[] { 32 }, AllowObjectValueAttribute.EnumObjectType.Int32);
            Assert.IsFalse(allowObjectValueAttribute.IsValid("123"));
            Assert.IsTrue(allowObjectValueAttribute.IsValid(32));
            Assert.IsFalse(allowObjectValueAttribute.IsValid(3));
        }

        [Test]
        public void AllowInt64ValueValid()
        {
            AllowObjectValueAttribute allowObjectValueAttribute = new AllowObjectValueAttribute(new object[] { 32 }, AllowObjectValueAttribute.EnumObjectType.Int64);
            Assert.IsFalse(allowObjectValueAttribute.IsValid("123"));
            Assert.IsTrue(allowObjectValueAttribute.IsValid(long.Parse("32")));
            Assert.IsFalse(allowObjectValueAttribute.IsValid(3));
        }

        [Test]
        public void AllowDoubleValueValid()
        {
            AllowObjectValueAttribute allowObjectValueAttribute = new AllowObjectValueAttribute(new object[] { 12.12 }, AllowObjectValueAttribute.EnumObjectType.Double);
            Assert.IsFalse(allowObjectValueAttribute.IsValid("123"));
            Assert.IsTrue(allowObjectValueAttribute.IsValid(double.Parse("12.12")));
        }

        [Test]
        public void Allow()
        {
            var model = new AllowObjectValueValid
            {
                Account = 12
            };
            ValidationModelState validation = new ValidationModelState(model);
            var s = validation.VerifyModel();
        }
    }
}
