using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Models;
using VirtualUniverse.DataValidation.ValidationAttributes;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/17 23:53:09；更新时间：
************************************************************************************/
namespace Test.ValidationAttributeTest
{
    /// <summary>
    /// 类说明：函数特性验证
    /// </summary>
    class FunctionAttributeTest
    {
        [Test]
        public void FunctionAllowNullOrEmptyValid()
        {
            FunctionAttribute functionAttribute = new FunctionAttribute("Test", "Services.ValidTest", "AllowNullTest");
            var model = new ValidModel
            {
                SchoolName = "名字",
                SchoolAddress = 10011
            };
            Assert.IsTrue(functionAttribute.IsValid(model.SchoolName));
            Assert.IsTrue(functionAttribute.IsValid(model.SchoolAddress));
            Assert.IsTrue(functionAttribute.IsValid(""));
            Assert.IsTrue(functionAttribute.IsValid(null));
            Assert.IsTrue(functionAttribute.IsValid("你好"));
        }

        [Test]
        public void FunctionDotAllowNullOrEmptyValid()
        {
            FunctionAttribute functionAttribute = new FunctionAttribute("Test", "Services.ValidTest", "DontAllowNullTest");
            var model = new ValidModel
            {
                SchoolName = "名字"
            };
            Assert.IsTrue(functionAttribute.IsValid(model.SchoolName));
            Assert.IsFalse(functionAttribute.IsValid(""));
            Assert.IsTrue(functionAttribute.IsValid("你好"));
            Assert.IsFalse(functionAttribute.IsValid(null));
        }

        [Test]
        public void FunctionExceptionValid()
        {
            Assert.Catch(FunctionAssemblyException);
            Assert.Catch(FunctionClassException);
            Assert.Catch(FunctionMethodException);
        }

        private static void FunctionAssemblyException()
        {
            FunctionAttribute functionAttribute = new FunctionAttribute("Test1", "Services.ValidTest", "DontAllowNullTest");
            var model = new ValidModel
            {
                SchoolName = "名字"
            };
            functionAttribute.IsValid(model.SchoolName);
        }

        private static void FunctionClassException()
        {
            FunctionAttribute functionAttribute = new FunctionAttribute("Test", "Services.ValidTest_1", "DontAllowNullTest");
            var model = new ValidModel
            {
                SchoolName = "名字"
            };
            functionAttribute.IsValid(model.SchoolName);
        }

        private static void FunctionMethodException()
        {
            FunctionAttribute functionAttribute = new FunctionAttribute("Test", "Services.ValidTest", "DontAllowNullTest_1");
            var model = new ValidModel
            {
                SchoolName = "名字"
            };
            functionAttribute.IsValid(model.SchoolName);
        }
    }
}
