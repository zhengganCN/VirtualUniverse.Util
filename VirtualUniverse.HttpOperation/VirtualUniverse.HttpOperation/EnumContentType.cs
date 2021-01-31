using System;
using System.Collections.Generic;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/27 10:41:40；更新时间：
************************************************************************************/
namespace VirtualUniverse.HttpOperation
{
    /// <summary>
    /// 类 描 述：提交数据内容格式类型
    /// </summary>
    public enum EnumContentType
    {
        /// <summary>
        /// application/json
        /// </summary>
        ApplicationJson = 1,
        /// <summary>
        /// multipart/form-data
        /// </summary>
        MultipartFormData = 2
    }
}
