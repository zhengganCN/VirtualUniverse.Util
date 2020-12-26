using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualUniverse.FileCompression.Models
{
    /// <summary>
    /// GZip解压参数
    /// </summary>
    public class GZipDecompressParamterModel
    {
        /// <summary>
        /// 解压文件的输出目录，可为空字符床，如果为空字符串，则解压缩文件的输出目录为压缩文件所在目录
        /// </summary>
        public string DecompressdDirectoryPath { get; set; }
    }
}
