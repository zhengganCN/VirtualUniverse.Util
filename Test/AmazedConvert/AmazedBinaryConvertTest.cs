using AmazedConvert;
using NUnit.Framework;

namespace Test.AmazedConvert
{
    public class AmazedBinaryConvertTest
    {
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void TestToDecimal()
        {
            var value = BinaryConvert.ToDecimal("1001");
            Assert.AreEqual(value, 9);
        }

        [Test]
        public void TestToHex()
        {
            var value = BinaryConvert.ToHex("000111111");
            Assert.AreEqual(value, "3F");
        }

        [Test]
        public void TestToOctal()
        {
            var value = BinaryConvert.ToOctal("000111111");
            Assert.AreEqual(value, "77");
        }
    }
}