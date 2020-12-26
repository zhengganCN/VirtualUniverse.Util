using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualUniverse.Convert.BaseConvert
{
    /// <summary>
    /// 16进制转换
    /// </summary>
   public static  class HexConvert
    {
        /// <summary>
        /// 十六进制转十进制
        /// </summary>
        /// <param name="hex">十六进制数</param>
        /// <returns></returns>
        public static int ToDecimal(string hex)
        {
           return BinaryConvert.ToDecimal(ToBinary(hex));
        }
        /// <summary>
        /// 十六进制转二进制
        /// </summary>
        /// <param name="hex">十六进制数</param>
        /// <returns></returns>
        public static string ToBinary(string hex)
        {
            if (!string.IsNullOrEmpty(hex))
            {
                if (!CheckHexData(hex))
                {
                    throw new ArgumentException($"参数{nameof(hex)}的值只能包含0-9和A-F");
                }
                string result = "";
                foreach (var item in hex)
                {
                    result += GetFourBinaryCharFromHexChar(item);
                }
                return result;
            }
            else
            {
                throw new ArgumentException($"参数{nameof(hex)}的值不能为Null或Empty");
            }
        }
        /// <summary>
        /// 十六进制转八进制
        /// </summary>
        /// <param name="hex">十六进制数</param>
        /// <returns></returns>
        public static string ToOctal(string hex)
        {
            return BinaryConvert.ToOctal(ToBinary(hex));
        }
        /// <summary>
        /// 检查16进制数据
        /// </summary>
        /// <param name="hex">16进制数据</param>
        /// <returns></returns>
        private static bool CheckHexData(string hex)
        {
            var hexBytes= Encoding.ASCII.GetBytes(hex);
            foreach (var item in hexBytes)
            {
                if (!((48 <= item && item <= 57) || (65 <= item && item <= 70)))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 通过1位16进制数获取4位二进制数
        /// 如：1111 => F
        /// </summary>
        /// <param name="oneHex">1位16进制数</param>
        /// <returns></returns>
        private static string GetFourBinaryCharFromHexChar(char oneHex)
        {
            string result = "";
            switch (oneHex)
            {
                case '0':
                    result = "0000";
                    break;
                case '1':
                    result = "0001";
                    break;
                case '2':
                    result = "0010";
                    break;
                case '3':
                    result = "0011";
                    break;
                case '4':
                    result = "0100";
                    break;
                case '5':
                    result = "0101";
                    break;
                case '6':
                    result = "0110";
                    break;
                case '7':
                    result = "0111";
                    break;
                case '8':
                    result = "1000";
                    break;
                case '9':
                    result = "1001";
                    break;
                case 'A':
                    result = "1010";
                    break;
                case 'B':
                    result = "1011";
                    break;
                case 'C':
                    result = "1100";
                    break;
                case 'D':
                    result = "1101";
                    break;
                case 'E':
                    result = "1110";
                    break;
                case 'F':
                    result = "1111";
                    break;
            }
            return result;
        }
    }
}
