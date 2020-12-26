using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;

namespace VirtualUniverse.FileCompression.Models
{
    /// <summary>
    /// GZip压缩参数
    /// </summary>
    public class GZipCompressParamterModel
    {
        /// <summary>
        /// 压缩文件的输出路径，会自动在文件的末尾添加.gz扩展名；如（CompressedFilePath属性值为“C:\temp\demo”,则输出的压缩文件名称为“C:\temp\demo.gz”）
        /// </summary>
        public string CompressedFilePath { get; set; }
        /// <summary>
        /// 压缩等级，默认Optimal以最佳方式完成压缩操作，不过，需要耗费更长的时间
        /// </summary>
        public CompressionLevel CompressionLevel { get; set; }
    }
}
