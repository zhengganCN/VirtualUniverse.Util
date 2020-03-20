using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmazedConvert
{
    /// <summary>
    /// 二进制转换
    /// </summary>
    public static class BinaryConvert
    {
        public static string ToHex(string binary)
        {
            if (!string.IsNullOrEmpty(binary))
            {
                var ints = binary.ToCharArray().ToList();
                if (ints.Count % 3 != 0)
                {
                    while (ints.Count % 3 > 0)
                    {
                        ints.Insert(0, '0');
                    }
                }
                var result = 0;
                for (int i = 0; i < ints.Count; i += 3)
                {
                    var temp = ints.GetRange(i, 3);
                    
                }
                return result.ToString();
            }
            else
            {
                throw new ArgumentException(nameof(binary));
            }
        }
        /// <summary>
        /// 转换成十进制
        /// </summary>
        /// <param name="binary">二进制字符串</param>
        /// <returns></returns>
        public static int ToDecimal(string binary)
        {
            if (!string.IsNullOrEmpty(binary))
            {
               var ints= binary.ToCharArray().ToList();
                ints.Reverse();
                var result = 0;
                for(int i=0;i<ints.Count;i++)
                {
                   result += int.Parse(ints[i].ToString()) * (int)Math.Pow(2, i);
                }
                return result;
            }
            else
            {
                throw new ArgumentException(nameof(binary));
            }
        }
        public static string ToOctal(string binary)
        {
            if (!string.IsNullOrEmpty(binary))
            {
                var ints = binary.ToCharArray().ToList();
                ints.Reverse();
                var result = 0;
                for (int i = 0; i < ints.Count; i++)
                {
                    result += int.Parse(ints[i].ToString()) * (int)Math.Pow(2, i);
                }
                return result.ToString();
            }
            else
            {
                throw new ArgumentException(nameof(binary));
            }
        }
        private static bool CheckBinaryData(string binary)
        {
            var bytes= Encoding.ASCII.GetBytes(binary);
            foreach (var item in bytes)
            {
                return true;
            }
            return false;
        }
    }
}
