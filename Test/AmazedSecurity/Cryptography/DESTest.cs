using AmazedSecurity.Cryptography;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.AmazedSecurity.Security.Cryptography
{
    class DESTest
    {
        [SetUp]
        public void SetUp()
        {

        }
        [Test]
        public void DESEncryptDecryptTest()
        {
            var data = "hello！小李。";
            var secret = DESHelper.DESEncrypt(Encoding.UTF8.GetBytes(data), Encoding.ASCII.GetBytes(StaticConfigurationValues.DESKey64));
            var value = Encoding.UTF8.GetString(DESHelper.DESDecrypt(secret, Encoding.ASCII.GetBytes(StaticConfigurationValues.DESKey64)));
            Assert.AreEqual(data, value);
        }
        [Test]
        public void DESExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(DESEncryptDataIsNullException);
            Assert.Throws<ArgumentNullException>(DESEncryptKeyIsNullException);
            Assert.Throws<ArgumentNullException>(DESDecryptDataIsNullException);
            Assert.Throws<ArgumentNullException>(DESDecryptKeyIsNullException);
        }
        private void DESEncryptDataIsNullException()
        {
            DESHelper.DESEncrypt(null, new byte[5]);
        }
        private void DESEncryptKeyIsNullException()
        {
            DESHelper.DESEncrypt(new byte[5], null);
        }
        private void DESDecryptDataIsNullException()
        {
            DESHelper.DESDecrypt(null, new byte[5]);
        }
        private void DESDecryptKeyIsNullException()
        {
            DESHelper.DESDecrypt(new byte[5], null);
        }
    }
}
