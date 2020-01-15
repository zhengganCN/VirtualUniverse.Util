using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Util.Security.Cryptography;

namespace UtilTest.Security.Cryptography
{
    class RSATest
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void GeneratePkcs8EncryptedPrivateKeyTest()
        {
            PbeParameters pbeParameters = new PbeParameters(PbeEncryptionAlgorithm.Aes256Cbc,
                HashAlgorithmName.SHA512, 10);
            var keys1024 = RSAHelper.GeneratePkcs8EncryptedPrivateKey(Encoding.ASCII.GetBytes(
                StaticConfigurationValues.RSAPassword), pbeParameters);
            Assert.IsNotEmpty(keys1024);
            var keys2048 = RSAHelper.GeneratePkcs8EncryptedPrivateKey(Encoding.ASCII.GetBytes(
                StaticConfigurationValues.RSAPassword), pbeParameters, RSAHelper.RSAKeySizeInBits.Bits2048);
            Assert.IsNotEmpty(keys2048);
        }

        [Test]
        public void GeneratePkcs8PrivateKeyTest()
        {
            var key1024 = RSAHelper.GeneratePkcs8PrivateKey();
            Assert.IsNotEmpty(key1024);
            var key2048 = RSAHelper.GeneratePkcs8PrivateKey(RSAHelper.RSAKeySizeInBits.Bits2048);
            Assert.IsNotEmpty(key2048);
        }

        [Test]
        public void GeneratePkcs1PrivateKeyTest()
        {
            var key1024 = RSAHelper.GeneratePkcs1PrivateKey();
            Assert.IsNotEmpty(key1024);
            var key2048 = RSAHelper.GeneratePkcs1PrivateKey(RSAHelper.RSAKeySizeInBits.Bits2048);
            Assert.IsNotEmpty(key2048);
        }
        [Test]
        public void GetPkcs1PublicKeyFromPkcs1PrivateKeyTest()
        {
            var publicKey1024 = RSAHelper.GetPkcs1PublicKeyFromPkcs1PrivateKey(
                Convert.FromBase64String(StaticConfigurationValues.RSAPkcs1PrivateKey1024));
            Assert.AreEqual(publicKey1024, StaticConfigurationValues.RSAPkcs1PublicKey1024);
            var publicKey2048 = RSAHelper.GetPkcs1PublicKeyFromPkcs1PrivateKey(
                Convert.FromBase64String(StaticConfigurationValues.RSAPkcs1PrivateKey2048));
            Assert.AreEqual(publicKey2048, StaticConfigurationValues.RSAPkcs1PublicKey2048);
        }
        [Test]
        public void GetPkcs1PublicKeyFromPkcs1PrivateKeyExceptionTest()
        {
            Assert.Throws<ArgumentException>(GetPkcs1PublicKeyFromPkcs1PrivateKeyException);
        }
        private void GetPkcs1PublicKeyFromPkcs1PrivateKeyException()
        {
            RSAHelper.GetPkcs1PublicKeyFromPkcs1PrivateKey(null);
        }
        [Test]
        public void GetPkcs8PublicKeyFromPkcs8PrivateKeyTest()
        {
            var publicKey1024 = RSAHelper.GetPkcs1PublicKeyFromPkcs8PrivateKey(
                Convert.FromBase64String(StaticConfigurationValues.RSAPkcs8PrivateKey1024));
            Assert.AreEqual(publicKey1024, StaticConfigurationValues.RSAPkcs1PublicKey1024FromPkcs8PrivateKey1024);
            var publicKey2048 = RSAHelper.GetPkcs1PublicKeyFromPkcs8PrivateKey(
                Convert.FromBase64String(StaticConfigurationValues.RSAPkcs8PrivateKey2048));
            Assert.AreEqual(publicKey2048, StaticConfigurationValues.RSAPkcs1PublicKey2048FromPkcs8PrivateKey2048);
        }
        [Test]
        public void GetPkcs1PublicKeyFromPkcs8PrivateKeyExceptionTest()
        {
            Assert.Throws<ArgumentException>(GetPkcs1PublicKeyFromPkcs8PrivateKeyException);
        }
        private void GetPkcs1PublicKeyFromPkcs8PrivateKeyException()
        {
            RSAHelper.GetPkcs1PublicKeyFromPkcs8PrivateKey(null);
        }
        [Test]
        public void GetPkcs8PrivateKeyFromEncryptedPrivatedPrivateKey()
        {
            var privateKey1024= RSAHelper.GetPkcs8PrivateKeyFromEncryptedPrivatedPrivateKey(
                Encoding.UTF8.GetBytes(StaticConfigurationValues.RSAPassword),
                StaticConfigurationValues.RSAEncryptedPkcs8PrivateKey1024);
            Assert.AreEqual(privateKey1024, StaticConfigurationValues.RSADecryptedPkcs8PrivateKey1024);
            var privateKey2048 = RSAHelper.GetPkcs8PrivateKeyFromEncryptedPrivatedPrivateKey(
                Encoding.UTF8.GetBytes(StaticConfigurationValues.RSAPassword),
                StaticConfigurationValues.RSAEncryptedPkcs8PrivateKey2048);
            Assert.AreEqual(privateKey2048, StaticConfigurationValues.RSADecryptedPkcs8PrivateKey2048);
        }
        [Test]
        public void ImportPkcs1PrivateKeyTest()
        {
            var rsa= RSAHelper.ImportPkcs1PrivateKey(Convert.FromBase64String(StaticConfigurationValues.RSAPkcs1PrivateKey1024));
            Assert.NotNull(rsa);
        }
        [Test]
        public void ImportPkcs1PrivateKeyExecptionTest()
        {
            Assert.Throws<ArgumentException>(ImportPkcs1PrivateKeyExecption);
        }
        private void ImportPkcs1PrivateKeyExecption()
        {
            RSAHelper.ImportPkcs1PrivateKey(null);
        }
        [Test]
        public void ImportPkcs1PublicKey()
        {
            var rsa = RSAHelper.ImportPkcs1PublicKey(Convert.FromBase64String(StaticConfigurationValues.RSAPkcs1PublicKey1024));
            Assert.NotNull(rsa);
        }
        [Test]
        public void ImportPkcs1PublicKeyExecptionTest()
        {
            Assert.Throws<ArgumentException>(ImportPkcs1PublicKeyExecption);
        }
        private void ImportPkcs1PublicKeyExecption()
        {
            RSAHelper.ImportPkcs1PublicKey(null);
        }
        [Test]
        public void ImportPkcs8PrivateKey()
        {
            var rsa = RSAHelper.ImportPkcs8PrivateKey(Convert.FromBase64String(StaticConfigurationValues.RSAPkcs8PrivateKey1024));
            Assert.NotNull(rsa);
        }
        [Test]
        public void ImportPkcs8PrivateKeyExecptionTest()
        {
            Assert.Throws<ArgumentException>(ImportPkcs8PrivateKeyExecption);
        }
        private void ImportPkcs8PrivateKeyExecption()
        {
            RSAHelper.ImportPkcs8PrivateKey(null);
        }
        [Test]
        public void RSAEncryptDecryptTest()
        {
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            var privateKey = StaticConfigurationValues.RSAPkcs8PrivateKey1024;
            var importPrivateKeyBytes = Convert.FromBase64String(privateKey);
            rsa.ImportPkcs8PrivateKey(importPrivateKeyBytes, out _);
            var dataToEncrypt = StaticConfigurationValues.RSAData;
            var encryptedDataBytes = RSAHelper.Encrypt(Encoding.UTF8.GetBytes(dataToEncrypt), rsa.ExportParameters(false));
            var decryptedDataBytes = RSAHelper.Decrypt(encryptedDataBytes, rsa.ExportParameters(true));
            Assert.AreEqual(dataToEncrypt, Encoding.UTF8.GetString(decryptedDataBytes));
        }
        [Test]
        public void RSAEncryptDecryptExtraLongDataTest()
        {
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            var privateKey = StaticConfigurationValues.RSAPkcs8PrivateKey1024;
            var importPrivateKeyBytes = Convert.FromBase64String(privateKey);
            rsa.ImportPkcs8PrivateKey(importPrivateKeyBytes, out _);
            var dataToEncrypt = StaticConfigurationValues.RSADataExtraLong;
            var encryptedDataBytes = RSAHelper.Encrypt(Encoding.UTF8.GetBytes(dataToEncrypt), rsa.ExportParameters(false));
            var decryptedDataBytes = RSAHelper.Decrypt(encryptedDataBytes, rsa.ExportParameters(true));
            Assert.AreEqual(dataToEncrypt, Encoding.UTF8.GetString(decryptedDataBytes));
        }

        [Test]
        public void RSAEncryptDecryptExceptionTest()
        {
           Assert.Throws<ArgumentNullException>(RSAEncryptException);
            Assert.Throws<ArgumentNullException>(RSADecryptException);
        }
        private void RSAEncryptException()
        {
            RSAHelper.Encrypt(null, new RSAParameters());
        }
        private void RSADecryptException()
        {
            RSAHelper.Decrypt(null, new RSAParameters());
        }

        [Test]
        public void RSASignatureVerifyTest()
        {
            var dataToSign = StaticConfigurationValues.RSAData;
            var rsaPara = RSAHelper.ImportPkcs1PrivateKey(
                Convert.FromBase64String(StaticConfigurationValues.RSAPkcs1PrivateKey1024));
            var sign = RSAHelper.SignatureData(Encoding.UTF8.GetBytes(dataToSign), rsaPara.ExportParameters(true));
            var result = RSAHelper.VerifyData(Encoding.UTF8.GetBytes(dataToSign), sign, rsaPara.ExportParameters(true));
            Assert.IsTrue(result);
        }
        [Test]
        public void RSASignatureVerifyWith512Test()
        {
            var dataToSign = StaticConfigurationValues.RSAData;
            var rsaPara = RSAHelper.ImportPkcs1PrivateKey(
                Convert.FromBase64String(StaticConfigurationValues.RSAPkcs1PrivateKey1024));
            var sign = RSAHelper.SignatureData(Encoding.UTF8.GetBytes(dataToSign), rsaPara.ExportParameters(true),new SHA512CryptoServiceProvider());
            var result = RSAHelper.VerifyData(Encoding.UTF8.GetBytes(dataToSign), sign, rsaPara.ExportParameters(true),new SHA512CryptoServiceProvider());
            Assert.IsTrue(result);
        }
        [Test]
        public void RSASignatureExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(SignatureDataException);
        }
        private void SignatureDataException()
        {
            RSAHelper.SignatureData(null, new RSAParameters());
        }
        [Test]
        public void RSAVerifyDataExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(VerifyDataDataToVerifyIsNullException);
            Assert.Throws<ArgumentNullException>(VerifyDataSignaturedDataIsNullException);
        }
        private void VerifyDataDataToVerifyIsNullException()
        {
            RSAHelper.VerifyData(null, new byte[5], new RSAParameters());
        }
        private void VerifyDataSignaturedDataIsNullException()
        {
            RSAHelper.VerifyData(new byte[5], null, new RSAParameters());
        }
    }
}
