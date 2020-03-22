using AmazedConfiguration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NUnitTestAmazedDataContext
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
        public static string MySQLConnectionString 
        { 
            get 
            { 
                return GetJsonConfiguration().GetValue("MySQL:ConnectionString"); 
            } 
        }
        public static string MSSQLConnectionString
        {
            get
            {
                return GetJsonConfiguration().GetValue("MSSQL:ConnectionString");
            }
        }
        public static string MongoDBConnectionString
        {
            get
            {
                return new JsonConfiguration(_path).GetValue("MongoDB:ConnectionString");
            }
        }
        public static string MongoDBDatabase
        {
            get
            {
                return GetJsonConfiguration().GetValue("MongoDB:Database");
            }
        }
    }
}
