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
        public static string MSSQLConnectionString
        {
            get
            {
                return new JsonConfiguration(_path).GetValue("MSSQL:ConnectionString");
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
                return new JsonConfiguration(_path).GetValue("MongoDB:Database");
            }
        }
    }
}
