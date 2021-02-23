using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using VirtualUniverse.Algorithm.UniqueGenerate;

namespace Test.UniqueGenerate
{
    class TestSnowflakeAlgorithm
    {

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
