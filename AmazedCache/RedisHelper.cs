using StackExchange.Redis;
using System;
using System.Security.Cryptography;

namespace AmazedCache
{
    public class RedisHelper
    {
        private readonly string _password;
        private readonly string _host;
        public RedisHelper(string host,string password)
        {
            _password = password;
            _host = host;
        }
        private Lazy<ConnectionMultiplexer> LazyConnection()
        {
            return new Lazy<ConnectionMultiplexer>(
                () => ConnectionMultiplexer.Connect(new ConfigurationOptions
                {
                    Password = _password,
                    SslHost = _host
                })
            );
        }

        public ConnectionMultiplexer Connection
        {
            get
            {
                return LazyConnection().Value;
            }
        }

        private IDatabase Database
        {
            get
            {
                return Connection.GetDatabase();
            }
        }

        public string GetString(string key)
        {
            return Database.StringGet(key);
        }
        public bool GetString(string key,string value)
        {
            return Database.StringSet(key,value);
        }
    }
}
