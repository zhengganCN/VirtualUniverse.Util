using AmazedConvert;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestAmazedConvert
{
    public class AmazedDecimalConvertTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestToDecimal()
        {
            var value = DecimalConvert.ToBinary(255);
            Assert.AreEqual(value, "11111111");
        }
    }
}
