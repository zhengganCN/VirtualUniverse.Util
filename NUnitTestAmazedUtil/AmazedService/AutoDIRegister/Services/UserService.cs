using NUnitTestAmazedUtil.AmazedService.AutoDIRegister.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestAmazedUtil.AmazedService.AutoDIRegister.Services
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
