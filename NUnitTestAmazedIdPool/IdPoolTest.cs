using AmazedIdPool;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using NUnitTestAmazedIdPool.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTestAmazedIdPool
{
    public class IdPoolTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("https://59.58.116.208:9011")]
        public void IdPoolGenerateTest(string ip)
        {
            IdGenerateService idGenerateService = new IdGenerateService(new LoggerFactory().CreateLogger<IdGenerateService>());
            for (int i = 0; i < 2000; i++)
            {
                var id = IdPool.GainId(() =>
                {
                    return idGenerateService.GenerateIds(ip);
                });
            }
            var count= IdPool.Count;
        }
    }
}