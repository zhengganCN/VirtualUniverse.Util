using AmazedService.AuthDIRegisterService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestAmazedUtil.AmazedService.AutoDIRegister.Services.Interfaces
{
    interface IHelloWorld : IScopedDI
    {
        string Print();
    }
}
