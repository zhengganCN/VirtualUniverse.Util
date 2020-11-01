using System;
using System.Collections.Generic;
using System.Text;

namespace AmazedDataValidation.ValidationModel
{
    /// <summary>
    /// 验证结果
    /// </summary>
    public class AmazedValidationResult
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 字段验证结果
        /// </summary>
        public object FieldVerifyResult { get; set; }
    }
}
