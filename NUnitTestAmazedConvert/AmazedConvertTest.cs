using AmazedConvert;
using NUnit.Framework;

namespace NUnitTestAmazedConvert
{
    public class AmazedConvertTest
    {
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void Test1()
        {
            var value = BinaryConvert.ToDecimal("1001");
            Assert.AreEqual(value, 9);
        }
    }
}