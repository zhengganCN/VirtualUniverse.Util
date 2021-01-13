using NUnit.Framework;
using System.Collections.Generic;
using VirtualUniverse.Service.Nacos.Models;
using VirtualUniverse.Service.Nacos.Services;

namespace VirtualUniverse.Service.Nacos.Test
{
    public class NacosConfigReadTest
    {
        /// <summary>
        /// ≈‰÷√∂¡»°≤‚ ‘
        /// </summary>
        [Test]
        public void ConfigReadTest()
        {
            var dev = new ConfigEnvironmentOptions { ConfigFileName = "appsettings.dev.json", DataId = "th-bank-account-service", Group = "DEFAULT_GROUP", Tenant = "th_dev" };
            var test = new ConfigEnvironmentOptions { ConfigFileName = "appsettings.dev.json", DataId = "th-bank-account-service", Group = "DEFAULT_GROUP", Tenant = "th_test" };
            var prod = new ConfigEnvironmentOptions { ConfigFileName = "appsettings.dev.json", DataId = "th-bank-account-service", Group = "DEFAULT_GROUP", Tenant = "th_prod" };
            var environments = new Dictionary<string, ConfigEnvironmentOptions>
                 {
                     { "dev",dev },
                     { "test",test },
                     { "prod",prod }
                 };
            var nacosConfigRead = new NacosConfigRead("http://192.168.3.182:8848", "dev", options =>
                {
                    options.Environments = environments;
                    options.ListenConfig = false;
                });
            nacosConfigRead.Dispose();
            Assert.Pass();
        }
    }
}