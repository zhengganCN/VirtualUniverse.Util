using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using VirtualUniverse.FileCompression;
using VirtualUniverse.FileCompression.Models;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/5/6 8:52:29；更新时间：
************************************************************************************/
namespace Test
{
    /// <summary>
    /// 类 描 述：
    /// </summary>
    class ZipTest
    {

        [Test]
        public void FileZipCompressionTest()
        {
            ZipCompression.Compression("./Compression/2021-03-21.log", new ZipCompressParamter
            {
                CompressedFilePath = "./Compression/2021-03-21"
            });
            Assert.Pass();
        }

        [Test]
        public void DirectoryZipCompressionTest()
        {
            ZipCompression.Compression("./Compression", new ZipCompressParamter
            {
                CompressedFilePath = "./Compression"
            });
            Assert.Pass();
        }

        [Test]
        public void ZipDecompressTest()
        {
            ZipCompression.Decompress("./Compression/2021-03-21.zip", new ZipDecompressParamter());
            Assert.Pass();
        }
    }
}
