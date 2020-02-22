using AmazedSecurity.Cryptography;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestAmazedSecurity.Security.Cryptography
{
    class SHATest
    {
        [SetUp]
        public void SetUp()
        {

        }
        [Test]
        public void SHA1Test()
        {
            var data = "hello！小李。";
            var secret = SHAHelper.SHA1Encrypt(Encoding.UTF8.GetBytes(data));
            Assert.IsNotEmpty(secret);
        }
        [Test]
        public void SHA256Test()
        {
            var data = "hello！小李。";
            var secret = SHAHelper.SHA256Encrypt(Encoding.UTF8.GetBytes(data));
            Assert.IsNotEmpty(secret);
        }
        [Test]
        public void SHA384Test()
        {
            var data = "hello！小李。";
            var secret = SHAHelper.SHA384Encrypt(Encoding.UTF8.GetBytes(data));
            Assert.IsNotEmpty(secret);
        }
        [Test]
        public void SHA512Test()
        {
            var data = "hello！小李。";
            var secret = SHAHelper.SHA512Encrypt(Encoding.UTF8.GetBytes(data));
            Assert.IsNotEmpty(secret);
        }

        [Test]
        public void SHAExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(SHA1Exception);
            Assert.Throws<ArgumentNullException>(SHA256Exception);
            Assert.Throws<ArgumentNullException>(SHA384Exception);
            Assert.Throws<ArgumentNullException>(SHA512Exception);
        }
        private void SHA1Exception()
        {
            SHAHelper.SHA1Encrypt(null);
        }
        private void SHA256Exception()
        {
            SHAHelper.SHA256Encrypt(null);
        }
        private void SHA384Exception()
        {
            SHAHelper.SHA384Encrypt(null);
        }
        private void SHA512Exception()
        {
            SHAHelper.SHA512Encrypt(null);
        }
    }
}
