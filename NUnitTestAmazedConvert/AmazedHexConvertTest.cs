using AmazedConvert;
using NUnit.Framework;

namespace NUnitTestAmazedConvert
{
    public class AmazedHexConvertTest
    {
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void TestToBinary()
        {
            var value = HexConvert.ToBinary("FF");
            Assert.AreEqual(value, "11111111");
        }

    }
}