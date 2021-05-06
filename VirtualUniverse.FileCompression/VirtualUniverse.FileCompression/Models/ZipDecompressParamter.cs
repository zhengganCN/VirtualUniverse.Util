using System;
using System.Collections.Generic;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/5/6 10:36:24；更新时间：
************************************************************************************/
namespace VirtualUniverse.FileCompression.Models
{
    /// <summary>
    /// 类 描 述：Zip解压参数
    /// </summary>
    public class ZipDecompressParamter
    {
        /// <summary>
        /// 解压文件的输出目录，可为空字符床，如果为空字符串，则解压缩文件的输出目录为压缩文件所在目录
        /// </summary>
        public string DecompressdDirectoryPath { get; set; }
    }
}
