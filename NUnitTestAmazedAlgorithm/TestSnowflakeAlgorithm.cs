using AmazedAlgorithm.UniqueGenerate;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace NUnitTestAmazedAlgorithm
{
    public class TestSnowflakeAlgorithm
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SnowflakeAlgorithmTest()
        {
            var ids = new List<long>(1000000);
            for (int i = 0; i < 1000000; i++)
            {
                ids.Add(SnowflakeAlgorithm.GenerateId(1));
            }
        }
    }
}