using System;
using VirtualUniverse.Extension.Extensions;
using VirtualUniverse.ModelResultStandard.Services;

namespace VirtualUniverse.ModelResultStandard.Extensions
{
    /// <summary>
    /// 模型结果扩展
    /// </summary>
    public static class ModelResultExtension
    {
        /// <summary>
        /// 成功结果
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="result">扩展类</param>
        /// <param name="data">数据</param>
        /// <param name="code">成功/错误代码</param>
        /// <param name="pagination">分页信息</param>
        public static void SuccessResult<T>(this ModelResult<T> result, T data, Enum code, Pagination pagination = null)
        {
            result.Data = data;
            result.Message = code.GetDescription();
            result.Success = true;
            result.Pagination = pagination;
            result.Code = ModelResult.GetCodeInfo(code);
        }
        /// <summary>
        /// 成功结果
        /// </summary>
        /// <param name="result">扩展类</param>
        /// <param name="data">数据</param>
        /// <param name="code">成功/错误代码</param>
        /// <param name="pagination">分页信息</param>
        public static void SuccessResult(this ModelResult result, object data, Enum code, Pagination pagination = null)
        {
            result.Data = data;
            result.Message = code.GetDescription();
            result.Success = true;
            result.Pagination = pagination;
            result.Code = ModelResult.GetCodeInfo(code);
        }
        /// <summary>
        /// 失败结果
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="result">扩展类</param>
        /// <param name="code">成功/错误代码</param>
        /// <param name="errorInfo">错误信息</param>
        public static void FailedResult<T>(this ModelResult<T> result, Enum code, object errorInfo = null)
        {
            result.Message = code.GetDescription();
            result.ErrorInfo = errorInfo;
            result.Success = false;
            result.Code = ModelResult.GetCodeInfo(code);
        }
        /// <summary>
        /// 失败结果
        /// </summary>
        /// <param name="result">扩展类</param>
        /// <param name="code">成功/错误代码</param>
        /// <param name="errorInfo">错误信息</param>
        public static void FailedResult(this ModelResult result, Enum code, object errorInfo = null)
        {
            result.Message = code.GetDescription();
            result.ErrorInfo = errorInfo;
            result.Success = false;
            result.Code = ModelResult.GetCodeInfo(code);
        }
    }
}
