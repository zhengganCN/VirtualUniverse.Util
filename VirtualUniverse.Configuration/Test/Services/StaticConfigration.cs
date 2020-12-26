using System;
using System.Collections.Generic;
using System.Text;
using VirtualUniverse.Configuration.Services;

namespace Test.Services
{
    public static class StaticConfigration
    {
        private static BaseJsonConfiguration jsonConfiguration;
        public static BaseJsonConfiguration JsonConfiguration
        {
            get
            {
                if (jsonConfiguration is null)
                {
                    jsonConfiguration = new BaseJsonConfiguration();
                }
                return jsonConfiguration;
            }
        }
        public static string FileName
        {
            get
            {
                return JsonConfiguration.GetValue("FileName");
            }
        }
    }
}
