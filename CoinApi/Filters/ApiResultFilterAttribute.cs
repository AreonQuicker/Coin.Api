using CoinApi.Models;
using E.S.Api.Helpers.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoinApi.Filters
{
    /// <summary>
    /// Action filter to wrap the result in a Api Result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResultFilterAttribute<T> : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
                return;

            if (!(context.Result is ObjectResult objectResult))
                return;

            switch (objectResult.Value)
            {
                case null:
                    context.Result = new ApiResult<T>(default)
                        .ToActionResult();
                    ;
                    break;

                case T data:
                    context.Result = new ApiResult<T>(data)
                        .ToActionResult();
                    break;
            }
        }
    }
}