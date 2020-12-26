using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VirtualUniverse.Convert.BaseConvert
{
    /// <summary>
    /// 二进制转换
    /// </summary>
    public static class BinaryConvert
    {
        /// <summary>
        /// 二进制转十六进制
        /// </summary>
        /// <param name="binary">二进制字符串</param>
        /// <returns></returns>
        public static string ToHex(string binary)
        {
            if (!string.IsNullOrEmpty(binary))
            {
                if (!CheckBinaryData(binary))
                {
                    throw new ArgumentException($"参数{nameof(binary)}的值只能包含0和1");
                }
                if (binary.Length % 4 != 0)
                {
                    while (binary.Length % 4 > 0)
                    {
                        binary=binary.Insert(0, "0");
                    }
                }
                string result = "";
                for (int i = 0; i < binary.Length; i += 4)
                {
                    result += GetHexCharFromFourBinaryChar(binary.Substring(i, 4).ToString());            
                }
                return result.TrimStart('0');
            }
            else
            {
                throw new ArgumentException($"参数{nameof(binary)}的值不能为Null或Empty");
            }
        }
        /// <summary>
        /// 二进制转十进制
        /// </summary>
        /// <param name="binary">二进制字符串</param>
        /// <returns></returns>
        public static int ToDecimal(string binary)
        {
            if (!string.IsNullOrEmpty(binary))
            {
                if (!CheckBinaryData(binary))
                {
                    throw new ArgumentException($"参数{nameof(binary)}的值只能包含0和1");
                }
                var ints = binary.ToCharArray().ToList();
                ints.Reverse();
                var result = 0;
                for (int i = 0; i < ints.Count; i++)
                {
                    result += int.Parse(ints[i].ToString()) * (int)Math.Pow(2, i);
                }
                return result;
            }
            else
            {
                throw new ArgumentException($"参数{nameof(binary)}的值不能为Null或Empty");
            }
        }
        /// <summary>
        /// 二进制转八进制
        /// </summary>
        /// <param name="binary">二进制字符串</param>
        /// <returns></returns>
        public static string ToOctal(string binary)
        {
            if (!string.IsNullOrEmpty(binary))
            {
                if (!CheckBinaryData(binary))
                {
                    throw new ArgumentException($"参数{nameof(binary)}的值只能包含0和1");
                }
                if (binary.Length % 3 != 0)
                {
                    while (binary.Length % 3 > 0)
                    {
                        binary = binary.Insert(0, "0");
                    }
                }
                string result = "";
                for (int i = 0; i < binary.Length; i += 3)
                {
                    result += GetOctalCharFromThreeBinaryChar(binary.Substring(i, 3).ToString());
                }
                return result.TrimStart('0');
            }
            else
            {
                throw new ArgumentException($"参数{nameof(binary)}的值不能为Null或Empty");
            }
        }
        /// <summary>
        /// 检查二进制字符串是否只包含0或1
        /// </summary>
        /// <param name="binary">二进制字符串</param>
        /// <returns></returns>
        private static bool CheckBinaryData(string binary)
        {
            var bytes= Encoding.ASCII.GetBytes(binary);
            foreach (var item in bytes)
            {
                if (!(item==48||item==49))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 通过4位二进制数获取1位16进制数
        /// 如：1111 => F
        /// </summary>
        /// <param name="fourBinary">4位二进制数</param>
        /// <returns></returns>
        private static char GetHexCharFromFourBinaryChar(string fourBinary)
        {
            char result = ' ';
            if (fourBinary.Length != 4)
            {
                throw new ArgumentException($"参数{nameof(fourBinary)}的长度必须为4");
            }
            switch (fourBinary)
            {
                case "0000":
                    result = '0';
                    break;
                case "0001":
                    result = '1';
                    break;
                case "0010":
                    result = '2';
                    break;
                case "0011":
                    result = '3';
                    break;
                case "0100":
                    result = '4';
                    break;
                case "0101":
                    result = '5';
                    break;
                case "0110":
                    result = '6';
                    break;
                case "0111":
                    result = '7';
                    break;
                case "1000":
                    result = '8';
                    break;
                case "1001":
                    result = '9';
                    break;
                case "1010":
                    result = 'A';
                    break;
                case "1011":
                    result = 'B';
                    break;
                case "1100":
                    result = 'C';
                    break;
                case "1101":
                    result = 'D';
                    break;
                case "1110":
                    result = 'E';
                    break;
                case "1111":
                    result = 'F';
                    break;
            }
            return result;
        }
        /// <summary>
        /// 通过3位二进制数获取1位8进制数
        /// 如：111 => 7
        /// </summary>
        /// <param name="threeBinary">3位二进制数</param>
        /// <returns></returns>
        private static char GetOctalCharFromThreeBinaryChar(string threeBinary)
        {
            char result = ' ';
            if (threeBinary.Length != 3)
            {
                throw new ArgumentException($"参数{nameof(threeBinary)}的长度必须为3");
            }
            switch (threeBinary)
            {
                case "000":
                    result = '0';
                    break;
                case "001":
                    result = '1';
                    break;
                case "010":
                    result = '2';
                    break;
                case "011":
                    result = '3';
                    break;
                case "100":
                    result = '4';
                    break;
                case "101":
                    result = '5';
                    break;
                case "110":
                    result = '6';
                    break;
                case "111":
                    result = '7';
                    break;
            }
            return result;
        }
    }
}
