using AmazedConfiguration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NUnitTestAmazedConfiguration
{
    class ConfigurationTest
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
            var path= Directory.GetCurrentDirectory();
            path= Path.Combine(path, "App.json");
            JsonConfiguration jsonConfiguration = new JsonConfiguration(path);
            var str1= jsonConfiguration.GetValue("TestString");
            Assert.AreEqual(str1, "hello world");
            var str2 = jsonConfiguration.GetValue("FF");
            Assert.AreEqual(str2, "");
            var array = jsonConfiguration.GetValue("TestArray:0");
            Assert.AreEqual(array, "1");
            var obj= jsonConfiguration.GetValue("TestObject:ColorRed");
            Assert.AreEqual(obj, "red");
            var testObject= jsonConfiguration.GetObject<JsonObject>();
            Assert.IsNotNull(testObject);
            jsonConfiguration.Dispose();
        }
        [Test]
        public void TestJsonConfigurationGetValueExecption()
        {
            Assert.Throws<ArgumentNullException>(ConfigurationGetValueExecption);
        }
        public void ConfigurationGetValueExecption()
        {
            var path = Directory.GetCurrentDirectory();
            path = Path.Combine(path, "App.json");
            JsonConfiguration jsonConfiguration = new JsonConfiguration(path);
            jsonConfiguration.GetValue("");
            jsonConfiguration.Dispose();
        }

        /// <summary>
        /// 设置配置文件的值
        /// </summary>
        [Test]
        public void TestJsonConfigurationSetValue()
        {
            var path = Directory.GetCurrentDirectory();
            path = Path.Combine(path, "App.json");
            JsonConfiguration jsonConfiguration = new JsonConfiguration(path);
            jsonConfiguration.SetValue("TestString", "3");
            jsonConfiguration.Dispose();
        }


        /// <summary>
        /// 测试读取性能
        /// </summary>
        [Test]
        public void TestJsonConfigurationAbility()
        {
            var path = Directory.GetCurrentDirectory();
            path = Path.Combine(path, "App.json");
            for (int i = 0; i < 10000; i++)
            {
                JsonConfiguration jsonConfiguration = new JsonConfiguration(path);
                var str = jsonConfiguration.GetValue("TestString");
                Assert.AreEqual(str, "hello world");
                jsonConfiguration.Dispose();
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
                JsonConfiguration jsonConfiguration = new JsonConfiguration(path);
                var testObject = jsonConfiguration.GetObject<JsonObject>();
                Assert.IsNotNull(testObject);
                jsonConfiguration.Dispose();
            }
        }
    }
    class JsonObject
    {
        public string TestString{ get; set; }
        public int[] TestArray{ get; set; }
        public TestObject TestObject { get; set; }
    }
    class TestObject
    {
        public string ColorRed { get; set; }
        public int[] Order { get; set; }
        public string[] Command { get; set; }
        public DF DF { get; set; }
    }
    class DF
    {
        public string D { get; set; }
        public string F { get; set; }
    }
}
