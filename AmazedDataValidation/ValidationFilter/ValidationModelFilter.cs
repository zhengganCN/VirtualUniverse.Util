using AmazedGeneralData;
using AmazedModelResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmazedDataValidation.ValidationFilter
{
    /// <summary>
    /// 模型验证拦截器
    /// </summary>
    public sealed class ValidationModelFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 在函数执行前验证
        /// </summary>
        /// <param name="context">上下文</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var badRequestObjectResult = new BadRequestObjectResult(context.ModelState);
                var result = new ModelResult<object>();
                result.FailedResult(EnumModelError.ModelVaildFaild, badRequestObjectResult.Value);
                context.Result = new JsonResult(result);
            }
        }
    }
}
