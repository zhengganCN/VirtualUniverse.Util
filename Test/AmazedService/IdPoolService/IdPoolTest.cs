using AmazedService.IdPoolService;
using GRpcClient.Services;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.AmazedService.IdPoolService
{
    class IdPoolTest
    {
        [SetUp]
        public void SetUp()
        {

        }
        [Test]
        public void GenerateIds()
        {
            IdGenerateService service = new IdGenerateService(new LoggerFactory().CreateLogger<IdGenerateService>());
            var ids = new HashSet<long>();
            for (int i = 0; i < 10000; i++)
            {
                ids.Add(IdPool.GainId(() => service.GenerateIds("https://117.26.235.160:9011")));
            }
            Assert.IsFalse(ids.Contains(0));
        }
    }
}
