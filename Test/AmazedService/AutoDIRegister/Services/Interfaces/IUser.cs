using AmazedService.AuthDIRegisterService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.AmazedService.AutoDIRegister.Services.Interfaces
{
    interface IUser : ITransientDI
    {
        string PrintUser();
    }
}
