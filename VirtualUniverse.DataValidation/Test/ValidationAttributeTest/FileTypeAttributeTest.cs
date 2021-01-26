using Microsoft.AspNetCore.Http.Internal;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VirtualUniverse.DataValidation.ValidationAttributes;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/18 10:53:47；更新时间：
************************************************************************************/
namespace Test.ValidationAttributeTest
{
    /// <summary>
    /// 类 描 述：文件类型测试
    /// </summary>
    class FileTypeAttributeTest
    {
        [Test]
        public void SingleFileTypeValid()
        {
            var stream_1 = new FileStream("Files/appsettings-1.json", FileMode.Open, FileAccess.Read);
            var file_1 = new FormFile(stream_1, 0, stream_1.Length, "appsettings-1", "appsettings-1.json");
            FileTypeAttribute fileTypeAttribute = new FileTypeAttribute
            {
                FileType = new string[] { "json" }
            };
            Assert.IsTrue(fileTypeAttribute.IsValid(file_1));

            fileTypeAttribute = new FileTypeAttribute
            {
                FileType = new string[] { "txt" }
            };
            Assert.IsFalse(fileTypeAttribute.IsValid(file_1));
        }

        /// <summary>
        /// 未指定文件类型
        /// </summary>
        [Test]
        public void NoSpecialFileTypeValid()
        {
            var stream_1 = new FileStream("Files/appsettings-1.json", FileMode.Open, FileAccess.Read);
            var file_1 = new FormFile(stream_1, 0, stream_1.Length, "appsettings-1", "appsettings-1");
            FileTypeAttribute fileTypeAttribute = new FileTypeAttribute
            {
                FileType = new string[] { "json" }
            };
            Assert.IsFalse(fileTypeAttribute.IsValid(file_1));
        }

        [Test]
        public void MutilFileTypeValid()
        {
            var stream_1 = new FileStream("Files/appsettings-1.json", FileMode.Open, FileAccess.Read);
            var stream_2 = new FileStream("Files/appsettings-2.json", FileMode.Open, FileAccess.Read);
            var file_1 = new FormFile(stream_1, 0, stream_1.Length, "appsettings-1", "appsettings-1.json");
            var file_2 = new FormFile(stream_2, 0, stream_1.Length, "appsettings-2", "appsettings-1.json");
            var files = new FormFileCollection
            {
                file_1,
                file_2
            };
            FileTypeAttribute fileTypeAttribute = new FileTypeAttribute
            {
                FileType = new string[] { "json" }
            };
            Assert.IsTrue(fileTypeAttribute.IsValid(files));
            fileTypeAttribute = new FileTypeAttribute
            {
                FileType = new string[] { "jpg" }
            };
            Assert.IsFalse(fileTypeAttribute.IsValid(files));
        }

        [Test]
        public void EmptyValid()
        {
            FileTypeAttribute fileTypeAttribute = new FileTypeAttribute
            {
                FileType = new string[] { "json" }
            };
            Assert.IsTrue(fileTypeAttribute.IsValid(""));
        }

        [Test]
        public void NullValid()
        {
            FileTypeAttribute fileTypeAttribute = new FileTypeAttribute
            {
                FileType = new string[] { "json" }
            };
            Assert.IsTrue(fileTypeAttribute.IsValid(null));
        }
    }
}
