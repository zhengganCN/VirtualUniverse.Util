﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Util.Security.Cryptography
{
    /// <summary>
    /// 对称加密算法，算法支持的密钥长度为128、192、256位。IV长度位128位
    /// </summary>
    public static class AES
    {
        /// <summary>
        /// AES加密算法
        /// </summary>
        /// <param name="data">需加密的数据</param>
        /// <param name="key">密钥</param>
        /// <param name="keyType">密钥长度</param>
        /// <returns></returns>
        public static string AESEncrypt(byte[] data, byte[] key, KeyType keyType = KeyType.Key128)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            using var aes = new AesCryptoServiceProvider
            {
                Key = GetKey(key, keyType),
                IV = key.Take(16).ToArray()
            };
            using MemoryStream memoryStream = new MemoryStream();
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(data, 0, data.Length);
            cryptoStream.FlushFinalBlock();
            return Convert.ToBase64String(memoryStream.ToArray());
        }
        /// <summary>
        /// AES解密算法
        /// </summary>
        /// <param name="data">需解密的数据</param>
        /// <param name="key">密钥</param>
        /// <param name="keyType">密钥长度</param>
        /// <returns></returns>
        public static string AESDecrypt(byte[] data, byte[] key, KeyType keyType = KeyType.Key128)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            using AesCryptoServiceProvider aes = new AesCryptoServiceProvider
            {
                Key = GetKey(key, keyType),
                IV = key.Take(16).ToArray()
            };
            using MemoryStream memoryStream = new MemoryStream();
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(data, 0, data.Length);
            cryptoStream.FlushFinalBlock();
            return Encoding.Default.GetString(memoryStream.ToArray());
        }
        private static byte[] GetKey(byte[] key, KeyType keyType)
        {
            return keyType switch
            {
                KeyType.Key128 => key.Take(128).ToArray(),
                KeyType.Key192 => key.Take(128).ToArray(),
                KeyType.Key256 => key.Take(128).ToArray(),
                _ => key,
            };
        }

        /// <summary>
        /// 密钥长度
        /// </summary>
        public enum KeyType
        {
            /// <summary>
            /// 密钥长度为128
            /// </summary>
            [Description("密钥长度为128")]
            Key128 = 1,
            /// <summary>
            /// 密钥长度为192
            /// </summary>
            [Description("密钥长度为192")]
            Key192 = 2,
            /// <summary>
            /// 密钥长度为256
            /// </summary>
            [Description("密钥长度为256")]
            Key256 = 3,
        }
    }
}
