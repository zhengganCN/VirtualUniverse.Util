using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Util.Configuration;

namespace UtilTest
{
    public static class StaticConfigurationValues
    {
        private static readonly string _path;
        static StaticConfigurationValues()
        {
            var path = Directory.GetCurrentDirectory();
            _path = Path.Combine(path, "App.json");
        }
        public static string MySQLConnectionString 
        { 
            get 
            { 
                return new JsonConfiguration(_path).GetValue("MySQL:ConnectionString"); 
            } 
        }
    }
}
