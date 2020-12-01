using System;
using System.Collections.Generic;
using System.Text;
using Test.AmazedService.AutoDIRegister.Services.Interfaces;

namespace Test.AmazedService.AutoDIRegister.Services
{
    class HelloWorldService : IHelloWorld
    {
        private readonly IUser _user;
        public static string HelloWorld = "HelloWorld";

        public HelloWorldService(IUser user)
        {
            _user = user;
        }

        public string Print()
        {
            return _user.PrintUser() + HelloWorld;
        }

    }
}
