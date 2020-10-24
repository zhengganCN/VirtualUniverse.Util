using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NUnitTestAmazedUtil.AmazedSocket
{
    class SocketClientTest
    {
        [SetUp]
        public void SetUp() { }
        [Test]
        [TestCase("hello")]
        public void ClientSendReceive(string collor)
        {
            Console.WriteLine(collor);
            //var client = new SocketClient();
            //var ipAddress = IPAddress.Parse("localhost");
            //var ip = new IPEndPoint(ipAddress, 11111);
            //client.SocketSendReceive(ip);
        }
    }
}
