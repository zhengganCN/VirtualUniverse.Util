using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NUnitTestAmazedUtil.AmazedDataValidation.VerifyAttribute
{
    public class ClassDto
    {
        [Required]
        public string ClassName { get; set; }
        public StudentDto Student { get; set; }
        public IList<StudentDto> Students { get; set; }
    }
}
