using AmazedService.BackgroundWorker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NUnitTestAmazedUtil.AmazedService.BackgroundWorker
{
    class BgWorkerTest
    {
        [SetUp]
        public void SetUp()
        {

        }
        [Test]
        public void BgWorkerStart()
        {
            HelloWorldService service = new HelloWorldService(new LoggerFactory().CreateLogger<BaseBackgroundService>());
            service.StartAsync(new CancellationToken());
        }
    }
}
