using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AmazedSecurity.Cryptography
{
    /// <summary>
    /// hash算法
    /// </summary>
    public static class SHAHelper
    {
        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="data">需加密的数据</param>
        /// <returns>16进制字符串</returns>
        public static string SHA1Encrypt(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            using SHA1 sha1 = new SHA1CryptoServiceProvider();
            var hashBytes = sha1.ComputeHash(data);
            StringBuilder hashString = new StringBuilder();
            foreach (var item in hashBytes)
            {
                hashString.Append(item.ToString("X"));
            }
            return hashString.ToString();
        }
        /// <summary>
        /// SHA256加密
        /// </summary>
        /// <param name="data">需加密的数据</param>
        /// <returns>16进制字符串</returns>
        public static string SHA256Encrypt(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            using SHA256 sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(data);
            StringBuilder hashString = new StringBuilder();
            foreach (var item in hashBytes)
            {
                hashString.Append(item.ToString("X"));
            }
            return hashString.ToString();

        }
        /// <summary>
        /// SHA384加密
        /// </summary>
        /// <param name="data">需加密的数据</param>
        /// <returns>16进制字符串</returns>
        public static string SHA384Encrypt(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            using SHA384 sha384 = SHA384.Create();
            var hashBytes = sha384.ComputeHash(data);
            StringBuilder hashString = new StringBuilder();
            foreach (var item in hashBytes)
            {
                hashString.Append(item.ToString("X"));
            }
            return hashString.ToString();
        }
        /// <summary>
        /// SHA512加密
        /// </summary>
        /// <param name="data">需加密的数据</param>
        /// <returns>16进制字符串</returns>
        public static string SHA512Encrypt(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            using SHA512 sha512 = SHA512.Create();
            var hashBytes = sha512.ComputeHash(data);
            StringBuilder hashString = new StringBuilder();
            foreach (var item in hashBytes)
            {
                hashString.Append(item.ToString("X"));
            }
            return hashString.ToString();
        }
    }
}
