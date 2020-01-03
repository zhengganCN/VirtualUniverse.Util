using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Util.Configuration;

namespace UtilTest
{
    public static class StaticConfigurationValues
    {
        private static readonly JsonConfiguration jsonConfiguration;
        static StaticConfigurationValues()
        {
            var path = Directory.GetCurrentDirectory();
            path = Path.Combine(path, "App.json");
            jsonConfiguration = new JsonConfiguration(path);
        }
        
        public static string MySQLConnectionString 
        { 
            get 
            { 
                return jsonConfiguration.GetValue("MySQL:ConnectionString"); 
            } 
        }
    }
}
