namespace VirtualUniverse.DataValidation.ValidationModel
{
    /// <summary>
    /// 验证结果
    /// </summary>
    public class ValidationResult
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
