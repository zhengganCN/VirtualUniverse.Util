using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace VirtualUniverse.Security.Cryptography.Services
{
    /// <summary>
    /// 对称加密算法，算法支持的密钥长度为64位。
    /// </summary>
    public static class DesOperation
    {
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="data">需加密的数据</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static byte[] DESEncrypt(byte[] data, byte[] key)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            using DESCryptoServiceProvider des = new DESCryptoServiceProvider
            {
                Key = GetKey(key),
                IV = key
            };
            using MemoryStream memoryStream = new MemoryStream();
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(data, 0, data.Length);
            cryptoStream.FlushFinalBlock();
            return memoryStream.ToArray();
        }
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="data">需解密的数据</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static byte[] DESDecrypt(byte[] data, byte[] key)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            using DESCryptoServiceProvider des = new DESCryptoServiceProvider
            {
                Key = GetKey(key),
                IV = key
            };
            using MemoryStream memoryStream = new MemoryStream();
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(data, 0, data.Length);
            cryptoStream.FlushFinalBlock();
            return memoryStream.ToArray();
        }
        /// <summary>
        /// 获取密钥
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static byte[] GetKey(byte[] key)
        {
            return key.Take(8).ToArray();
        }
    }
}
