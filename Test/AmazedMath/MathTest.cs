using AmazedMath.Math;
using NUnit.Framework;

namespace Test.AmazedMath
{
    public class Tests
    {
        private RandomNumber random;
        [SetUp]
        public void Setup()
        {
            random = new RandomNumber();
        }

        [Test]
        public void TestGenerateIntRandom()
        {
            random.GenerateRandom(0, 10, true, true);
            Assert.IsTrue(true);
        }

        [Test]
        public void TestGenerateDoubleRandom()
        {
            random.GenerateRandom(10.0, 20.0, true, true);
            Assert.IsTrue(true);
        }
        
    }
}