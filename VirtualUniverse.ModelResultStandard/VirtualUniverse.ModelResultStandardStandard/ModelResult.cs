using System;
using VirtualUniverse.Extension.Extensions;
using VirtualUniverse.ModelResultStandard.Services;

namespace VirtualUniverse.ModelResultStandard
{
    /// <summary>
    /// 返回的视图模型以及结果信息
    /// </summary>
    public class ModelResult
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; internal set; }
        /// <summary>
        /// 分页信息
        /// </summary>
        public Pagination Pagination { get; internal set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; internal set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public object ErrorInfo { get; internal set; }
        /// <summary>
        /// 成功/错误代码
        /// </summary>
        public int? Code { get; internal set; }
        /// <summary>
        /// 成功标识,操作执行是否成功
        /// </summary>
        public bool Success { get; internal set; }
        /// <summary>
        /// 返回成功时调用
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="code">成功代码</param>
        /// <param name="pagination">分页信息</param>
        /// <returns></returns>
        public ModelResult SuccessResult(object data, Enum code, Pagination pagination = null)
        {
            var model = new ModelResult
            {
                Data = data,
                Message = code.GetDescription(),
                Success = true,
                Pagination = pagination,
                Code = GetCodeInfo(code)
            };
            return model;
        }
        /// <summary>
        /// 返回失败时调用
        /// </summary>
        /// <param name="code">错误代码</param>
        /// <param name="errorInfo">错误信息</param>
        /// <returns></returns>
        public ModelResult FailedResult(Enum code, string errorInfo = "")
        {
            var model = new ModelResult
            {
                Message = code.GetDescription(),
                ErrorInfo = errorInfo,
                Success = false,
                Code = GetCodeInfo(code)
            };
            return model;
        }
        /// <summary>
        /// 获取Code信息
        /// </summary>
        /// <param name="code">成功/错误枚举</param>
        /// <returns></returns>
        internal static int? GetCodeInfo(Enum code)
        {
            if (code != null)
            {
                return (int)Enum.Parse(code.GetType(), code.ToString());
            }
            return null;
        }
    }
    /// <summary>
    /// 返回的视图模型以及结果信息
    /// </summary>
    /// <typeparam name="T">返回的数据类型</typeparam>
    public class ModelResult<T> : ModelResult
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public new T Data { get; internal set; }
        /// <summary>
        /// 返回成功时调用
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="code">成功代码</param>
        /// <param name="pagination">分页信息</param>
        /// <returns></returns>
        public ModelResult<T> SuccessResult(T data, Enum code, Pagination pagination = null)
        {
            var model = new ModelResult<T>
            {
                Data = data,
                Message = code.GetDescription(),
                Success = true,
                Pagination = pagination,
                Code = GetCodeInfo(code)
            };
            return model;
        }
    }
}
