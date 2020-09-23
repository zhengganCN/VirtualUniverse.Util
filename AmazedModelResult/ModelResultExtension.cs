using AmazedExtension;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmazedModelResult
{
    /// <summary>
    /// 
    /// </summary>
    public static class ModelResultExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="data"></param>
        /// <param name="code"></param>
        /// <param name="pagination"></param>
        public static void SuccessResult<T>(this ModelResult<T> result, T data, Enum code, Pagination pagination = null)
        {
            result.Data = data;
            result.Message = code.GetDescription();
            result.Success = true;
            result.Pagination = pagination;
            if (code == null)
            {
                result.Code = null;
            }
            else
            {
                result.Code = (int)Enum.Parse(code.GetType(), code.ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="code"></param>
        /// <param name="errorInfo"></param>
        public static void FailedResult<T>(this ModelResult<T> result, Enum code, object errorInfo = null)
        {
            result.Message = code.GetDescription();
            result.ErrorInfo = errorInfo;
            result.Success = false;
            if (code == null)
            {
                result.Code = null;
            }
            else
            {
                result.Code = (int)Enum.Parse(code.GetType(), code.ToString());
            }
        }
    }
}
