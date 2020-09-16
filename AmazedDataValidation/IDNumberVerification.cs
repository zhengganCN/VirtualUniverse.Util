using System;

namespace AmazedDataValidation
{
    /// <summary>
    /// 身份证号验证
    /// </summary>
    public static class IDNumberVerification
    {
        /// <summary>
        /// 省份代码
        /// </summary>
        private static readonly string ProvinceCode = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
        private static readonly string[] ArrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
        private static readonly string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
        /// <summary>
        /// 验证身份证号
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        public static bool ValidIDNumber(string idNumber)
        {
            if (idNumber.Length == 18)
            {
                return ValidIDNumber18(idNumber);
            }
            else if (idNumber.Length == 15)
            {
                return ValidIDNumber15(idNumber);
            }
            else
            {
                return false;
            }
        }

        /// <summary> 
        /// 验证18位身份证号
        /// </summary>
        /// <param name="idNumber">身份证号</param>
        /// <returns>验证成功为True，否则为False</returns>
        private static bool ValidIDNumber18(string idNumber)
        {
            var result = true;
            if (long.TryParse(idNumber.Remove(17), out long n) == false || n < Math.Pow(10, 16) || long.TryParse(idNumber.Replace('x', '0').Replace('X', '0'), out _) == false)//数字验证 
            {
                result = false;
            }
            if (!ValidProvince(idNumber) || !VaildDate(idNumber.Substring(6, 8)) || !Valid18Code(idNumber))
            {
                result = false;
            }
            return result;
        }

        /// <summary> 
        /// 验证15位身份证号 
        /// </summary> 
        /// <param name="idNumber">身份证号</param> 
        /// <returns>验证成功为True，否则为False</returns> 
        private static bool ValidIDNumber15(string idNumber)
        {
            bool result = true;
            if (long.TryParse(idNumber, out long n) == false || n < Math.Pow(10, 14))//数字验证 
            {
                result = false;
            }
            if (!ValidProvince(idNumber) || !VaildDate(idNumber.Substring(6, 6)))
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 验证18位身份证的第18位的合法性
        /// </summary>
        /// <param name="idNumber">身份证号</param>
        /// <returns></returns>
        private static bool Valid18Code(string idNumber)
        {
            
            var ai = idNumber.Remove(17).ToCharArray();
            var sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(ai[i].ToString());
            }
            Math.DivRem(sum, 11, out int y);
            if (ArrVarifyCode[y] != idNumber.Substring(17, 1).ToLower())
            {
                return false;//校验码验证 
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 验证日期格式是否正确
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        private static bool VaildDate(string date)
        {
            return DateTime.TryParse(date, out _);
        }

        /// <summary>
        /// 省份验证
        /// </summary>
        /// <param name="idNumber">身份证号</param>
        /// <returns></returns>
        private static bool ValidProvince(string idNumber)
        {
            return ProvinceCode.IndexOf(idNumber.Remove(2)) >= 0;
        }
    }
}
