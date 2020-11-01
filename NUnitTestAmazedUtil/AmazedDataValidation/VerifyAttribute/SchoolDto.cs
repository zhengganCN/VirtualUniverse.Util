using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NUnitTestAmazedUtil.AmazedDataValidation.VerifyAttribute
{
    public class SchoolDto
    {
        [Required]
        public int Year { get; set; }
        [Required]
        public int? Month { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime? End { get; set; }
        [Required]
        public byte Value1 { get; set; }
        [Required]
        public byte? Value2 { get; set; }
        [Required]
        public bool Value3 { get; set; }
        [Required]
        public bool? Value4 { get; set; }
        [Required]
        public long Value5 { get; set; }
        [Required]
        public long? Value6 { get; set; }
        [Required]
        public char Value7 { get; set; }
        [Required]
        public char? Value8 { get; set; }
        [Required]
        public List<int> Value9 { get; set; }
        [Required]
        public ClassDto Class { get; set; }
        [Required]
        public IList<ClassDto> Classes { get; set; }
    }
}
