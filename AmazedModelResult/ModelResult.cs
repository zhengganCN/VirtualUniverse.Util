using AmazedExtension;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmazedModelResult
{
    /// <summary>
    /// 返回的视图模型以及结果信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ModelResult<T>
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; private set; }
        /// <summary>
        /// 分页信息
        /// </summary>
        public Pagination Pagination { get; private set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; private set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorInfo { get; private set; }
        /// <summary>
        /// 成功/错误代码
        /// </summary>
        public int? Code { get; private set; }
        /// <summary>
        /// 成功标识,操作执行是否成功
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// 返回成功时调用
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="code">成功代码</param>
        /// <param name="pagination">分页信息</param>
        /// <returns></returns>
        public ModelResult<T> SuccessResult(T data, Enum code, Pagination pagination = null)
        {
            var model= new ModelResult<T>
            {
                Data = data,
                Message = code.GetDescription(),
                Success = true,
                Pagination = pagination
            };
            if (code==null)
            {
                model.Code = null;
            }
            else
            {
                model.Code = (int)Enum.Parse(code.GetType(), code.ToString());
            }
            return model;
        }
        /// <summary>
        /// 返回失败时调用
        /// </summary>
        /// <param name="code">错误代码</param>
        /// <param name="errorInfo">错误信息</param>
        /// <returns></returns>
        public ModelResult<T> FailedResult(Enum code, string errorInfo = "")
        {
            var model= new ModelResult<T>
            {
                Message = code.GetDescription(),
                ErrorInfo = errorInfo,
                Success = false
            };
            if (code == null)
            {
                model.Code = null;
            }
            else
            {
                model.Code = (int)Enum.Parse(code.GetType(), code.ToString());
            }
            return model;
        }
    }
}
