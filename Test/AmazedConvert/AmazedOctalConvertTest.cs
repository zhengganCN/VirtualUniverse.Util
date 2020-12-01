using AmazedConvert;
using NUnit.Framework;

namespace Test.AmazedConvert
{
    public class AmazedOctalConvertTest
    {
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void TestToBinary()
        {
            var value = OctConvert.ToBinary("77");
            Assert.AreEqual(value, "111111");
        }

    }
}