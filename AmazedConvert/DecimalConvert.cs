using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmazedConvert
{
    /// <summary>
    /// 进制转换
    /// </summary>
    public static class DecimalConvert
    {
        /// <summary>
        /// 十进制转十六进制
        /// </summary>
        /// <param name="dec">十进制数</param>
        /// <returns></returns>
        public static string ToHex(int dec)
        {
            return BinaryConvert.ToHex(ToBinary(dec));
        }
        /// <summary>
        /// 十进制转二进制
        /// </summary>
        /// <param name="dec">十进制数</param>
        /// <returns></returns>
        public static string ToBinary(int dec)
        {
            string binary = "";
            if (dec==0)
            {
                return "0";
            }
            else if (dec==1)
            {
                return "1";
            }
            while (true)
            {
                if (dec == 1)
                {
                    binary += "1";
                    StringBuilder builder = new StringBuilder();
                    var result = binary.Reverse();
                    foreach (var item in result)
                    {
                        builder.Append(item);
                    }
                    return builder.ToString();
                }
                else
                {
                    if (dec%2==0)
                    {
                        binary += "0";
                    }
                    else
                    {
                        binary += "1";
                    }
                    dec /= 2;
                }
            }
        }
        /// <summary>
        /// 十进制转八进制
        /// </summary>
        /// <param name="dec">十进制数</param>
        /// <returns></returns>
        public static string ToOctal(int dec)
        {
            return BinaryConvert.ToOctal(ToBinary(dec));
        }
    }
}
