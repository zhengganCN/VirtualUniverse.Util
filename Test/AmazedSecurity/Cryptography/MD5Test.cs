using AmazedSecurity.Cryptography;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.AmazedSecurity.Security.Cryptography
{
    class MD5Test
    {
        [SetUp]
        public void SetUp()
        {

        }
        [Test]
        public void MD5EncryptTest()
        {
            var data = "hello！小李。";
            var secret = MD5Helper.Encrypt(Encoding.UTF8.GetBytes(data));
            Assert.IsNotEmpty(secret);
        }
        [Test]
        public void MD5ExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(MD5Exception);
        }
        private void MD5Exception()
        {
            MD5Helper.Encrypt(null);
        }
    }
}
