using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Test.Services
{
    public class ConfigurationTest
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// 获取配置文件的值
        /// </summary>
        [Test]
        public void TestJsonConfigurationGetValue()
        {
            Assert.IsNotNull(StaticConfigration.FileName);
        }

        /// <summary>
        /// 设置配置文件的值
        /// </summary>
        [Test]
        public void TestJsonConfigurationSetValue()
        {
        }

        /// <summary>
        /// 测试读取性能
        /// </summary>
        [Test]
        public void TestJsonConfigurationAbility()
        {
            for (int i = 0; i < 1000000; i++)
            {
                Assert.IsNotNull(StaticConfigration.FileName);
            }
        }

        /// <summary>
        /// 测试实例化json文件性能
        /// </summary>
        [Test]
        public void TestJsonConfigurationConvertObjectAbility()
        {
            var path = Directory.GetCurrentDirectory();
            path = Path.Combine(path, "App.json");
            for (int i = 0; i < 1000; i++)
            {
                
            }
        }
    }
}
