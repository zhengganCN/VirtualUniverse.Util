using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/18 9:11:06；更新时间：
************************************************************************************/
namespace Test.Models
{
    /// <summary>
    /// 类 描 述：验证模型
    /// </summary>
    public class ValidModel
    {
        [Required]
        public string SchoolName { get; set; }
        public int SchoolAddress { get; set; }
    }
}
