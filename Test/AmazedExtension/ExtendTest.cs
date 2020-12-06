using AmazedExtension;
using NUnit.Framework;
using System;

namespace Test.AmazedExtension
{
    class ExtendTest
    {
        [SetUp]
        public void Setup()
        {

        }
        [Test]
        public void TestEnumExtend()
        {
            if (TestEnum.Monday.GetDescription() == "星期一")
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }

        [Test]
        public void TestEnumList()
        {
           var enums= EnumOperation.GetEnumList(typeof(TestEnum));
            if(enums[0].EnumDescription== "星期一" && enums[1].EnumDescription == "星期二" && enums[2].EnumDescription == "星期三")
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }

        enum TestEnum
        {
            [System.ComponentModel.Description("星期一")]
            Monday=1,
            [System.ComponentModel.Description("星期二")]
            TrusDay=2,
            [System.ComponentModel.Description("星期三")]
            WensDay =3
        }
    }
}
