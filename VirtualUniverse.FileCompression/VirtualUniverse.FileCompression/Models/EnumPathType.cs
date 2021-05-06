using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/5/5 22:45:32；更新时间：
************************************************************************************/
namespace VirtualUniverse.FileCompression.Models
{
    /// <summary>
    /// 类 描 述：枚举路径类型
    /// </summary>
    internal enum EnumPathType
    {
        [Description("文件路径")]
        FilePath = 1,
        [Description("目录路径")]
        DirectoryPath = 2,
        [Description("路径不存在")]
        NoExits = 3
    }
}
