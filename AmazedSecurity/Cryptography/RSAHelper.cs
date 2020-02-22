using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace AmazedSecurity.Cryptography
{
    /// <summary>
    /// RSA加密/解密,非对称算法
    /// </summary>
    public static class RSAHelper
    {
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="dataToEncrypt"></param>
        /// <param name="rsaParameters"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] dataToEncrypt, RSAParameters rsaParameters)
        {
            if (dataToEncrypt == null)
            {
                throw new ArgumentNullException(nameof(dataToEncrypt));
            }
            byte[] encryptedData;  //加密后的数据
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(rsaParameters);
                //验证要加密的数据是否超长
                if (VaildDataIsMoreLength(dataToEncrypt, rsa))
                {
                    var length = rsa.KeySize / 8 - 11;//分段加密时，每段能加密的字符字节数
                    //当需要加密的数据过长时，需要循环加密的次数
                    var time = System.Math.Ceiling((double)dataToEncrypt.Length / length);
                    List<byte> temp = new List<byte>();//临时变量，存储分段加密的加密后数据
                    for (int i = 0; i < time; i++)
                    {
                        temp.AddRange(rsa.Encrypt(dataToEncrypt.Skip(i * length).Take(length).ToArray(), RSAEncryptionPadding.Pkcs1));
                    }
                    encryptedData = temp.ToArray();
                }
                else
                {
                    encryptedData = rsa.Encrypt(dataToEncrypt, RSAEncryptionPadding.Pkcs1);
                }
            }
            return encryptedData;
        }
        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="dataToDecrypt"></param>
        /// <param name="rsaParameters"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] dataToDecrypt, RSAParameters rsaParameters)
        {
            if (dataToDecrypt==null)
            {
                throw new ArgumentNullException(nameof(dataToDecrypt));
            }
            byte[] decryptedData = null;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(rsaParameters);
                //当需要解密的数据过长时，需要循环解密的次数
                var time = System.Math.Ceiling((double)dataToDecrypt.Length / (rsa.KeySize/8));
                List<byte> temp = new List<byte>();//临时变量，存储分段解密后的数据
                var length = rsaParameters.D.Length;//分段解密时，每段能解密的字符字节数
                for (int i = 0; i < time; i++)
                {
                    var data = dataToDecrypt.Skip(i * length).Take(length).ToArray();
                    temp.AddRange(rsa.Decrypt(data, RSAEncryptionPadding.Pkcs1));
                }
                decryptedData = temp.ToArray();
            }
            return decryptedData;
        }
        /// <summary>
        /// RSA签名
        /// </summary>
        /// <param name="dataToSignature">待签名数据</param>
        /// <param name="rsaParameters">rsa参数</param>
        /// <param name="hashAlgorithm">hash算法实例，如果hashAlgorithm==null，则默认使用sha256算法</param>
        /// <returns>已签名数据</returns>
        public static byte[] SignatureData(byte[] dataToSignature, RSAParameters rsaParameters,
            object hashAlgorithm = null)
        {
            if (dataToSignature == null)
            {
                throw new ArgumentNullException(nameof(dataToSignature));
            }
            byte[] signaturedData = null;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(rsaParameters);
                if (hashAlgorithm != null)
                {
                    signaturedData = rsa.SignData(dataToSignature, hashAlgorithm);
                }
                else
                {
                    using var sha256 = new SHA256CryptoServiceProvider();
                    signaturedData = rsa.SignData(dataToSignature, sha256);
                }
            }
            return signaturedData;
        }
        /// <summary>
        /// RSA验签
        /// </summary>
        /// <param name="dataToVerify">待验签数据</param>
        /// <param name="signaturedData">已签名数据</param>
        /// <param name="rsaParameters">rsa参数</param>
        /// <param name="hashAlgorithm">hash算法实例，如果hashAlgorithm==null，则默认使用sha256算法</param>
        /// <returns>验签是否通过</returns>
        public static bool VerifyData(byte[] dataToVerify, byte[] signaturedData, RSAParameters rsaParameters, 
            object hashAlgorithm = null)
        {
            if (dataToVerify == null)
            {
                throw new ArgumentNullException(nameof(dataToVerify));
            }
            if (signaturedData == null)
            {
                throw new ArgumentNullException(nameof(signaturedData));
            }
            var verifyResult = false;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(rsaParameters);
                if (hashAlgorithm != null)
                {
                    verifyResult = rsa.VerifyData(dataToVerify, hashAlgorithm, signaturedData);
                }
                else
                {
                    using var sha256 = new SHA256CryptoServiceProvider();
                    verifyResult = rsa.VerifyData(dataToVerify, sha256, signaturedData);
                }
            }
            return verifyResult;
        }
        /// <summary>
        /// //验证要加密的数据是否超长
        /// </summary>
        /// <param name="data"></param>
        /// <param name="rsa"></param>
        /// <returns></returns>
        private static bool VaildDataIsMoreLength(byte[] data, RSA rsa)
        {
            if (data.Length > rsa.KeySize/8 - 11)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 生成Pkcs8格式的加密后的私钥
        /// </summary>
        /// <param name="password"></param>
        /// <param name="pbeParameters"></param>
        /// <param name="keySizeInBits">密钥长度</param>
        /// <returns>Base64</returns>
        public static string GeneratePkcs8EncryptedPrivateKey(byte[] password, PbeParameters pbeParameters, 
            RSAKeySizeInBits keySizeInBits=RSAKeySizeInBits.Bits1024)
        {
            using RSA rsa = RSA.Create((int)keySizeInBits);
            var keys = rsa.ExportEncryptedPkcs8PrivateKey(password, pbeParameters);
            return Convert.ToBase64String(keys);
        }
        /// <summary>
        /// 生成Pkcs8格式的私钥
        /// <param name="keySizeBits">密钥长度</param>
        /// </summary>
        /// <returns>Base64</returns>
        public static string GeneratePkcs8PrivateKey(RSAKeySizeInBits keySizeBits = RSAKeySizeInBits.Bits1024)
        {
            using var rsa = RSA.Create((int)keySizeBits);
            var keys = rsa.ExportPkcs8PrivateKey();
            return Convert.ToBase64String(keys);
        }
        /// <summary>
        /// 生成Pkcs1格式的私钥
        /// </summary>
        /// <param name="keySizeBits">密钥长度</param>
        /// <returns>Base64</returns>
        public static string GeneratePkcs1PrivateKey(RSAKeySizeInBits keySizeBits = RSAKeySizeInBits.Bits1024)
        {
            using var rsa = RSA.Create((int)keySizeBits);
            var keys = rsa.ExportRSAPrivateKey();
            return Convert.ToBase64String(keys);
        }
        /// <summary>
        /// 从加密的Pkcs8格式的私钥中获取未加密的Pkcs8格式的私钥
        /// </summary>
        /// <param name="password"></param>
        /// <param name="encryptedPrivateKey"></param>
        /// <returns>Base64</returns>
        public static string GetPkcs8PrivateKeyFromEncryptedPrivatedPrivateKey(byte[] password, string encryptedPrivateKey)
        {
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportEncryptedPkcs8PrivateKey(password, Convert.FromBase64String(encryptedPrivateKey), out int bytesRead);
            var privateKey = rsa.ExportPkcs8PrivateKey();
            return Convert.ToBase64String(privateKey);
        }
        ///// <summary>
        ///// 从Pkcs8格式的私钥中获取Pkcs8格式的公钥
        ///// </summary>
        ///// <param name="privateKey">pkcs8格式的私钥</param>
        ///// <returns>Base64</returns>
        //public static string GetPkcs8PublicKeyFromPkcs8PrivateKey(byte[] privateKey)
        //{
        //    if (privateKey == null||privateKey.Length<=0)
        //    {
        //        throw new ArgumentException(nameof(privateKey)+StringResource._47B50496_4949_443F_8D22_15D008056E32);
        //    }
        //    using RSA rsa = RSA.Create();
        //    rsa.ImportPkcs8PrivateKey(privateKey, out _);
        //    var publicKey= rsa.ExportRSAPublicKey();
        //    return Convert.ToBase64String(publicKey);
        //}
        /// <summary>
        /// 从Pkcs8格式的私钥中获取Pkcs1格式的公钥
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns>Base64</returns>
        public static string GetPkcs1PublicKeyFromPkcs8PrivateKey(byte[] privateKey)
        {
            if (privateKey == null || privateKey.Length <= 0)
            {
                throw new ArgumentException(nameof(privateKey) + StringResource._47B50496_4949_443F_8D22_15D008056E32);
            }
            using RSA rsa = RSA.Create();
            rsa.ImportPkcs8PrivateKey(privateKey, out _);
            var publicKey = rsa.ExportRSAPublicKey();
            return Convert.ToBase64String(publicKey);
        }
        ///// <summary>
        ///// 从Pkcs1格式的私钥中获取Pkcs8格式的公钥
        ///// </summary>
        ///// <param name="privateKey"></param>
        ///// <returns>Base64</returns>
        //public static string GetPkcs8PublicKeyFromPkcs1PrivateKey(byte[] privateKey)
        //{
        //    if (privateKey == null || privateKey.Length <= 0)
        //    {
        //        throw new ArgumentException(nameof(privateKey) + StringResource._47B50496_4949_443F_8D22_15D008056E32);
        //    }
        //    using RSA rsa = RSA.Create();
        //    rsa.ImportRSAPrivateKey(privateKey, out _);
        //    var publicKey = rsa.ExportRSAPublicKey();
        //    return Convert.ToBase64String(publicKey);
        //}
        /// <summary>
        /// 从Pkcs1格式的私钥中获取Pkcs1格式的公钥
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns>Base64</returns>
        public static string GetPkcs1PublicKeyFromPkcs1PrivateKey(byte[] privateKey)
        {
            if (privateKey == null || privateKey.Length <= 0)
            {
                throw new ArgumentException(nameof(privateKey) + StringResource._47B50496_4949_443F_8D22_15D008056E32);
            }
            using RSA rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(privateKey, out _);
            var publicKey = rsa.ExportRSAPublicKey();
            return Convert.ToBase64String(publicKey);
        }
        /// <summary>
        /// 导入Pkcs1格式的公钥
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <returns>rsa</returns>
        public static RSA ImportPkcs1PublicKey(byte[] publicKey)
        {
            if (publicKey == null || publicKey.Length <= 0)
            {
                throw new ArgumentException(nameof(publicKey) + StringResource._47B50496_4949_443F_8D22_15D008056E32);
            }
            var rsa = RSA.Create();
            rsa.ImportRSAPublicKey(publicKey, out _);
            return rsa;
        }
        ///// <summary>
        ///// 导入Pkcs8格式的公钥
        ///// </summary>
        ///// <param name="publicKey">公钥</param>
        ///// <returns>rsa</returns>
        //public static RSA ImportPkcs8PublicKey(byte[] publicKey)
        //{
        //    if (publicKey == null || publicKey.Length <= 0)
        //    {
        //        throw new ArgumentException(nameof(publicKey) + StringResource._47B50496_4949_443F_8D22_15D008056E32);
        //    }
        //    var rsa = RSA.Create();
        //    rsa.ImportRSAPublicKey(publicKey, out _);
        //    return rsa;
        //}

        /// <summary>
        /// 导入Pkcs1格式的私钥
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <returns>rsa</returns>
        public static RSA ImportPkcs1PrivateKey(byte[] privateKey)
        {
            if (privateKey == null || privateKey.Length <= 0)
            {
                throw new ArgumentException(nameof(privateKey) + StringResource._47B50496_4949_443F_8D22_15D008056E32);
            }
            var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(privateKey, out _);
            return rsa;
        }
        /// <summary>
        /// 导入Pkcs8格式的私钥
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <returns>rsa</returns>
        public static RSA ImportPkcs8PrivateKey(byte[] privateKey)
        {
            if (privateKey == null || privateKey.Length <= 0)
            {
                throw new ArgumentException(nameof(privateKey) + StringResource._47B50496_4949_443F_8D22_15D008056E32);
            }
            var rsa = RSA.Create();
            rsa.ImportPkcs8PrivateKey(privateKey, out _);
            return rsa;
        }

        /// <summary>
        /// 密钥长度
        /// </summary>
        [Flags]
        public enum RSAKeySizeInBits
        {
            /// <summary>
            /// 密钥长度为1024
            /// </summary>
            [Description("密钥长度为1024")]
            Bits1024=1024,
            /// <summary>
            /// 密钥长度为2048
            /// </summary>
            [Description("密钥长度为2048")]
            Bits2048 =2048
        }

    }
}
