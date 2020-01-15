using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Util.Security.Cryptography;

namespace UtilTest.Security.Cryptography
{
    class AESTest
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void AES128Test()
        {
            var data = "hello！小李。";
            var secret = AESHelper.AESEncrypt(Encoding.UTF8.GetBytes(data), Encoding.ASCII.GetBytes(StaticConfigurationValues.AESKey128),
                AESHelper.KeyType.Key128);
            var value = Encoding.UTF8.GetString(AESHelper.AESDecrypt(secret, Encoding.ASCII.GetBytes(StaticConfigurationValues.AESKey128),
                AESHelper.KeyType.Key128));
            Assert.AreEqual(data, value);
        }
        [Test]
        public void AES128ExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(AES128EncryptDataIsNullException);
            Assert.Throws<ArgumentNullException>(AES128EncryptKeyIsNullException);
            Assert.Throws<ArgumentNullException>(AES128DecryptDataIsNullException);
            Assert.Throws<ArgumentNullException>(AES128DecryptKeyIsNullException);
        }
        private void AES128EncryptDataIsNullException()
        {
            AESHelper.AESEncrypt(null, new byte[5], AESHelper.KeyType.Key128);
        }
        private void AES128EncryptKeyIsNullException()
        {
            AESHelper.AESEncrypt(new byte[5], null, AESHelper.KeyType.Key128);
        }
        private void AES128DecryptDataIsNullException()
        {
            AESHelper.AESDecrypt(null, new byte[5], AESHelper.KeyType.Key128);
        }
        private void AES128DecryptKeyIsNullException()
        {
            AESHelper.AESDecrypt(new byte[5], null, AESHelper.KeyType.Key128);
        }
        [Test]
        public void AES192Test()
        {
            var data = "hello！小李。";
            var secret = AESHelper.AESEncrypt(Encoding.UTF8.GetBytes(data), Encoding.UTF8.GetBytes(StaticConfigurationValues.AESKey192),
                AESHelper.KeyType.Key192); 
            var value = Encoding.UTF8.GetString(AESHelper.AESDecrypt(secret, Encoding.ASCII.GetBytes(StaticConfigurationValues.AESKey192),
                 AESHelper.KeyType.Key192));
            Assert.AreEqual(data, value);
        }
        [Test]
        public void AES192ExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(AES192EncryptDataIsNullException);
            Assert.Throws<ArgumentNullException>(AES192EncryptKeyIsNullException);
            Assert.Throws<ArgumentNullException>(AES192DecryptDataIsNullException);
            Assert.Throws<ArgumentNullException>(AES192DecryptKeyIsNullException);
        }
        private void AES192EncryptDataIsNullException()
        {
            AESHelper.AESEncrypt(null, new byte[5], AESHelper.KeyType.Key192);
        }
        private void AES192EncryptKeyIsNullException()
        {
            AESHelper.AESEncrypt(new byte[5], null, AESHelper.KeyType.Key192);
        }
        private void AES192DecryptDataIsNullException()
        {
            AESHelper.AESDecrypt(null, new byte[5], AESHelper.KeyType.Key192);
        }
        private void AES192DecryptKeyIsNullException()
        {
            AESHelper.AESDecrypt(new byte[5], null, AESHelper.KeyType.Key192);
        }
        [Test]
        public void AES256Test()
        {
            var data = "hello！小李。";
            var secret = AESHelper.AESEncrypt(Encoding.UTF8.GetBytes(data), Encoding.UTF8.GetBytes(StaticConfigurationValues.AESKey256),
                AESHelper.KeyType.Key256);
            var value = Encoding.UTF8.GetString(AESHelper.AESDecrypt(secret, Encoding.ASCII.GetBytes(StaticConfigurationValues.AESKey256),
                 AESHelper.KeyType.Key256));
            Assert.AreEqual(data, value);
        }
        [Test]
        public void AES256ExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(AES256EncryptDataIsNullException);
            Assert.Throws<ArgumentNullException>(AES256EncryptKeyIsNullException);
            Assert.Throws<ArgumentNullException>(AES256DecryptDataIsNullException);
            Assert.Throws<ArgumentNullException>(AES256DecryptKeyIsNullException);
        }
        private void AES256EncryptDataIsNullException()
        {
            AESHelper.AESDecrypt(null, new byte[5], AESHelper.KeyType.Key256);
        }
        private void AES256EncryptKeyIsNullException()
        {
            AESHelper.AESDecrypt(new byte[5], null, AESHelper.KeyType.Key256);
        }
        private void AES256DecryptDataIsNullException()
        {
            AESHelper.AESEncrypt(null, new byte[5], AESHelper.KeyType.Key256);
        }
        private void AES256DecryptKeyIsNullException()
        {
            AESHelper.AESEncrypt(new byte[5], null,AESHelper.KeyType.Key256);
        }

    }
}
