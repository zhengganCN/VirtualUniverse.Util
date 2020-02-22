﻿using AmazedExtension.Extension;
using NUnit.Framework;

namespace NUnitTestAmazedExtension
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
            if (TestEnum.Monday.GetDescription()== "星期一")
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
