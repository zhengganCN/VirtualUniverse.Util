using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/5/5 23:03:49；更新时间：
************************************************************************************/
namespace VirtualUniverse.FileCompression.Models
{
    /// <summary>
    /// 类 描 述：Zip压缩参数
    /// </summary>
    public class ZipCompressParamter
    {
        /// <summary>
        /// 压缩文件的输出路径，会自动在文件的末尾添加.zip扩展名；如（CompressedFilePath属性值为“C:\temp\demo”,则输出的压缩文件名称为“C:\temp\demo.zip”）
        /// </summary>
        public string CompressedFilePath { get; set; }
        /// <summary>
        /// 压缩等级，默认Optimal以最佳方式完成压缩操作，不过，需要耗费更长的时间
        /// </summary>
        public CompressionLevel CompressionLevel { get; set; }
        /// <summary>
        /// 是否在压缩文件中创建源目录
        /// </summary>
        public bool CreateDirectoryInCompressionFile { get; set; } = false;
    }
}
