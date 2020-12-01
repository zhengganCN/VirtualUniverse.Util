using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Test.AmazedDataValidation.VerifyAttribute
{
    public class StudentDto
    {
        /// <summary>
        /// 分数
        /// </summary>
        [Required(ErrorMessage ="必填")]
        public int Scout { get; set; }
    }
}
