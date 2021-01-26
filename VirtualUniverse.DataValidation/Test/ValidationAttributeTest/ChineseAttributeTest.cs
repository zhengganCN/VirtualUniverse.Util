using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using VirtualUniverse.DataValidation.ValidationAttributes;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/17 22:27:51；更新时间：
************************************************************************************/
namespace Test.ValidationAttributeTest
{
    /// <summary>
    /// 类说明：中文验证测试
    /// </summary>
    class ChineseAttributeTest
    {
        [Test]
        public void ChineseAtLeastOneValid()
        {
            ChineseAttribute chineseAttribute = new ChineseAttribute();
            Assert.IsFalse(chineseAttribute.IsValid("123"));
            Assert.IsTrue(chineseAttribute.IsValid("中文测试"));
            Assert.IsTrue(chineseAttribute.IsValid("123中文测试"));
        }

        [Test]
        public void ChineseAllValid()
        {
            ChineseAttribute chineseAttribute = new ChineseAttribute
            {
                ChineseContainer = ChineseAttribute.EnumChineseContainer.All
            };
            Assert.IsFalse(chineseAttribute.IsValid("123"));
            Assert.IsTrue(chineseAttribute.IsValid("中文测试"));
            Assert.IsFalse(chineseAttribute.IsValid("123中文测试"));
        }

        [Test]
        public void NoStringValid()
        {
            ChineseAttribute chineseAttribute = new ChineseAttribute();
            Assert.IsTrue(chineseAttribute.IsValid(null));
        }
    }
}
