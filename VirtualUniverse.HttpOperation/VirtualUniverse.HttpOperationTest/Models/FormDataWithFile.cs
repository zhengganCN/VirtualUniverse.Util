using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/27 11:45:02；更新时间：
************************************************************************************/
namespace VirtualUniverse.HttpOperationTest.Models
{
    /// <summary>
    /// 类 描 述：
    /// </summary>
    class FormDataWithFile
    {
        public string FileTypeId { get; set; }
        public IFormFile Files { get; set; }
    }
}
