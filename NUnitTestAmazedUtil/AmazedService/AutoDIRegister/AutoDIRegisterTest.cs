using AmazedService.AuthDIRegisterService;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnitTestAmazedUtil.AmazedService.AutoDIRegister.Services;
using NUnitTestAmazedUtil.AmazedService.AutoDIRegister.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestAmazedUtil.AmazedService.AutoDIRegister
{
    class AutoDIRegisterTest
    {
        private ServiceProvider serviceProvider;
        [SetUp]
        public void SetUp()
        {
            serviceProvider = new ServiceCollection()
              .AddLogging()
              .AddAutoDIRegister(AppDomain.CurrentDomain.GetAssemblies())
              .BuildServiceProvider();
        }
        [Test]
        public void PrintHelloWorld()
        {
            var helloWorld= serviceProvider.GetService<IHelloWorld>();
            var result= helloWorld.Print();
            Assert.AreEqual(UserService.User + HelloWorldService.HelloWorld, result);
        }
    }
}
