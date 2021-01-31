using Microsoft.AspNetCore.Http.Internal;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VirtualUniverse.HttpOperationTest.Models;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/27 11:42:53；更新时间：
************************************************************************************/
namespace VirtualUniverse.HttpOperationTest
{
    /// <summary>
    /// 类 描 述：
    /// </summary>
    class SendTest
    {
        [Test]
        public void SendFormTest()
        {
            var stream = new FileStream("test.png", FileMode.Open);
            FormFile formFile = new FormFile(stream, 0, stream.Length, "test", "test.png");
            FormDataWithFile form = new FormDataWithFile
            {
                FileTypeId = "-7277363352598506115",
                Files = formFile
            };
        }
    }
}
