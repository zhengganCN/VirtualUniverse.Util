using System;
using System.Collections.Generic;
using System.Text;

namespace AmazedConvert
{
    /// <summary>
    /// 八进制转换
    /// </summary>
    public static class OctConvert
    {
        /// <summary>
        /// 八进制转十进制
        /// </summary>
        /// <param name="octal">八进制数</param>
        /// <returns></returns>
        public static int ToDecimal(string octal)
        {
            return BinaryConvert.ToDecimal(ToBinary(octal));
        }
        /// <summary>
        /// 八进制转二进制
        /// </summary>
        /// <param name="octal">八进制数</param>
        /// <returns></returns>
        public static string ToBinary(string octal)
        {
            if (!string.IsNullOrEmpty(octal))
            {
                if (!CheckOctalData(octal))
                {
                    throw new ArgumentException($"参数{nameof(octal)}的值只能包含0-7");
                }
                string result = "";
                foreach (var item in octal)
                {
                    result += GetThreeBinaryCharFromOctalChar(item);
                }
                return result;
            }
            else
            {
                throw new ArgumentException($"参数{nameof(octal)}的值不能为Null或Empty");
            }
        }
        /// <summary>
        /// 八进制转十六进制
        /// </summary>
        /// <param name="octal">八进制数</param>
        /// <returns></returns>
        public static string ToHex(string octal)
        {
            return BinaryConvert.ToOctal(ToBinary(octal));
        }

        /// <summary>
        /// 检查8进制数据
        /// </summary>
        /// <param name="octal">8进制数据</param>
        /// <returns></returns>
        private static bool CheckOctalData(string octal)
        {
            var octalBytes = Encoding.ASCII.GetBytes(octal);
            foreach (var item in octalBytes)
            {
                if (!(48 <= item && item <= 55))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 通过1位8进制数获取3位二进制数
        /// 如：111 => 7
        /// </summary>
        /// <param name="oneOctal">1位8进制数</param>
        /// <returns></returns>
        private static string GetThreeBinaryCharFromOctalChar(char oneOctal)
        {
            string result = "";
            switch (oneOctal)
            {
                case '0':
                    result = "000";
                    break;
                case '1':
                    result = "001";
                    break;
                case '2':
                    result = "010";
                    break;
                case '3':
                    result = "011";
                    break;
                case '4':
                    result = "100";
                    break;
                case '5':
                    result = "101";
                    break;
                case '6':
                    result = "110";
                    break;
                case '7':
                    result = "111";
                    break;
            }
            return result;
        }
    }
}
