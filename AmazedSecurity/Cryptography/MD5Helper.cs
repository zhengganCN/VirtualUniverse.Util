using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AmazedSecurity.Cryptography
{
    /// <summary>
    /// hash算法，MD5
    /// </summary>
    public static class MD5Helper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="data">需加密的数据</param>
        /// <returns>16进制字符串</returns>
        public static string Encrypt(byte[] data)
        {
            if (data==null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var hashBytes = md5.ComputeHash(data);
                StringBuilder hashString = new StringBuilder();
                foreach (var item in hashBytes)
                {
                    hashString.Append(item.ToString("X"));
                }
                return hashString.ToString();
            }
        }
    }
}
