using AmazedService.BackgroundWorker;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test.AmazedService.BackgroundWorker
{
    class HelloWorldService: BaseBackgroundService
    {
        private static BaseServiceStartupParam StartupParam
        {
            get
            {
                return new BaseServiceStartupParam
                {
                    StartNow = true,
                    Interval = 200
                };
            }
        }
        public HelloWorldService(ILogger<BaseBackgroundService> logger) : base(() => { return StartupParam; }, logger)
        {

        }

        protected override Task ExecuteAsync()
        {
            Console.WriteLine("HH");
            return Task.Delay(1000);
        }

    }
}
