using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using AmazedModelResult;
using System.Collections.Generic;

namespace AmazedFilter
{
    public class ModelValidFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var modelState = context.ModelState;
            if (!modelState.IsValid)
            {
                var result = new ModelResult<Dictionary<string, List<string>>>();
                foreach (var value in modelState.Values)
                {
                    if (value.Errors.Count != 0)
                    {
                        var data = new Dictionary<string, List<string>>();
                        var errors = new List<string>();
                        foreach (var error in value.Errors)
                        {
                            errors.Add(error.ErrorMessage);

                        }
                        data.Add(nameof(value), errors);
                        result.SuccessResult<Dictionary<string, List<string>>>(data, null);
                    }
                }
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
