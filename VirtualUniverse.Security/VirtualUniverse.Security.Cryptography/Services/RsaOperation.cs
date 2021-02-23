using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace VirtualUniverse.Security.Cryptography.Services
{
    /// <summary>
    /// RSA加密/解密,非对称算法
    /// </summary>
    public static class RsaOperation
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
