using AmazedConfiguration.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NUnitTestSocket
{
    public static class StaticConfigurationValues
    {
        private static readonly string _path;
        static StaticConfigurationValues()
        {
            var path = Directory.GetCurrentDirectory();
            _path = Path.Combine(path, "App.json");
        }

        private static JsonConfiguration GetJsonConfiguration()
        {
            return new JsonConfiguration(_path);
        }
    }
}
