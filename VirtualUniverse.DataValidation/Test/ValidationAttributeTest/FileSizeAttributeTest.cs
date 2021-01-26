using Microsoft.AspNetCore.Http.Internal;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VirtualUniverse.DataValidation.ValidationAttributes;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/17 23:13:24；更新时间：
************************************************************************************/
namespace Test.ValidationAttributeTest
{
    /// <summary>
    /// 类说明：文件大小特性测试
    /// </summary>
    class FileSizeAttributeTest
    {
        [Test]
        public void SingleByteValid()
        {
            var stream_1 = new FileStream("Files/appsettings-1.json", FileMode.Open, FileAccess.Read);
            var file_1 = new FormFile(stream_1, 0, stream_1.Length, "appsettings-1", "appsettings-1.json");
            FileSizeAttribute fileSizeAttribute = new FileSizeAttribute
            {
                Unit = FileSizeAttribute.EnumSizeUnit.UnitByte
            };
            Assert.IsTrue(fileSizeAttribute.IsValid(file_1));
            fileSizeAttribute = new FileSizeAttribute
            {
                Unit = FileSizeAttribute.EnumSizeUnit.UnitByte,
                Size = 1
            };
            Assert.IsFalse(fileSizeAttribute.IsValid(file_1));
        }

        [Test]
        public void SingleKBValid()
        {
            var stream_1 = new FileStream("Files/appsettings-1.json", FileMode.Open, FileAccess.Read);
            var file_1 = new FormFile(stream_1, 0, stream_1.Length, "appsettings-1", "appsettings-1.json");
            FileSizeAttribute fileSizeAttribute = new FileSizeAttribute
            {
                Unit = FileSizeAttribute.EnumSizeUnit.UnitKB
            };
            Assert.IsTrue(fileSizeAttribute.IsValid(file_1));
        }

        [Test]
        public void SingleMBValid()
        {
            var stream_1 = new FileStream("Files/appsettings-1.json", FileMode.Open, FileAccess.Read);
            var file_1 = new FormFile(stream_1, 0, stream_1.Length, "appsettings-1", "appsettings-1.json");
            FileSizeAttribute fileSizeAttribute = new FileSizeAttribute
            {
                Unit = FileSizeAttribute.EnumSizeUnit.UnitMB
            };
            Assert.IsTrue(fileSizeAttribute.IsValid(file_1));
        }

        [Test]
        public void MutilValid()
        {
            var stream_1 = new FileStream("Files/appsettings-1.json", FileMode.Open, FileAccess.Read);
            var stream_2 = new FileStream("Files/appsettings-2.json", FileMode.Open, FileAccess.Read);
            var file_1 = new FormFile(stream_1, 0, stream_1.Length, "appsettings-1", "appsettings-1.json");
            var file_2 = new FormFile(stream_2, 0, stream_1.Length, "appsettings-2", "appsettings-2.json");
            var files = new FormFileCollection
            {
                file_1,
                file_2
            };
            FileSizeAttribute fileSizeAttribute = new FileSizeAttribute();
            Assert.IsTrue(fileSizeAttribute.IsValid(files));
            fileSizeAttribute = new FileSizeAttribute
            {
                Unit = FileSizeAttribute.EnumSizeUnit.UnitByte,
                Size = 1
            };
            Assert.IsFalse(fileSizeAttribute.IsValid(files));
        }

        [Test]
        public void EmptyValid()
        {
            FileSizeAttribute fileSizeAttribute = new FileSizeAttribute();
            Assert.IsTrue(fileSizeAttribute.IsValid(""));
        }

        [Test]
        public void NullValid()
        {
            FileSizeAttribute fileSizeAttribute = new FileSizeAttribute();
            Assert.IsTrue(fileSizeAttribute.IsValid(null));
        }
    }
}
