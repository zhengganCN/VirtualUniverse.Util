using System;
using System.Collections.Generic;
using System.Text;
using Test.AmazedService.AutoDIRegister.Services.Interfaces;

namespace Test.AmazedService.AutoDIRegister.Services
{
    class UserService : IUser
    {
        public static string User = "王小明";
        public string PrintUser()
        {
            return User;
        }
    }
}
