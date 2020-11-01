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
        RedisOperation operation;
        [SetUp]
        public void SetUp() {
        }
        [Test]
        public void ConnectRedis()
        {
            operation = new RedisOperation(
                () =>
                    {
                        var config= new ConfigurationOptions
                        {
                            Password = "password"
                        };
                        config.EndPoints.Add("host");
                        return config;
                    },
                () => { return 0; }
            );
            var value= operation.CacheRedis.StringGet("Test");
            Assert.IsTrue(operation.CacheConnection.IsConnected);
        }
    }
}
