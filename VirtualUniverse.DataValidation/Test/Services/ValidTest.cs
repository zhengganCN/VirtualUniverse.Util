using System;
using System.Collections.Generic;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/17 23:55:46；更新时间：
************************************************************************************/
namespace Test.Services
{
    /// <summary>
    /// 类说明：验证测试服务
    /// </summary>
    internal static class ValidTest
    {
        public static bool AllowNullTest(object value)
        {
            return true;
        }

        public static bool DontAllowNullTest(object value)
        {
            if (value is null)
            {
                return false;
            }
            else if (value is string)
            {
                if(string.IsNullOrEmpty(value as string))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
    }
}
