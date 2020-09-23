using System.ComponentModel;

namespace AmazedModelResult
{
    /// <summary>
    /// 结果枚举
    /// </summary>
    public static class ResultEnum
    {
        /// <summary>
        /// 模型验证结果提示枚举
        /// </summary>
        public enum ModelVaildEnum
        {
            /// <summary>
            /// 模型验证失败
            /// </summary>
            [Description("模型验证失败")]
            ModelVaildFaild = 101001
        }
    }
}
