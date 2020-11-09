using AmazedDataContext.Redis;
using NUnit.Framework;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestAmazedUtil.AmazedDataContext.RedisTest
{
    class RedisOperationTest
    {
        private RedisOperation operation;
        private const string host = "127.0.0.1";
        [SetUp]
        public void SetUp() {
            operation = new RedisOperation(
                () =>
                {
                    var config = new ConfigurationOptions
                    { };
                    config.EndPoints.Add(host);
                    return config;
                },
                () => { return 0; }
            );
        }
        [Test]
        public void ConnectRedis()
        {
            Assert.IsTrue(operation.CacheConnection.IsConnected);
        }
        [Test]
        public void HashGet()
        {
            var key = "Test";
            var field = "Field";
            var value = "Value";
            operation.HashSet(key, new Dictionary<string, string> 
            { 
                { field, value } 
            });
            Assert.AreEqual(operation.HashGet(key, field), value);
        }
    }
}
