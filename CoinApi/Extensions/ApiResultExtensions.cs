using System;
using CoinApi.Models;
using E.S.Api.Helpers.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CoinApi.Extensions
{
    /// <summary>
    /// Transform api result to object result
    /// </summary>
    public static class ApiResultExtensions
    {
        public static IActionResult ToActionResult<T>(this ApiResult<T> result)
        {
            return ((IConvertToActionResult) ToExplicitActionResult(result)).Convert();
        }

        public static IActionResult ToActionResult(this ApiResult result)
        {
            return ((IConvertToActionResult) ToExplicitActionResult(result)).Convert();
        }

        public static ActionResult<ApiResult<T>> ToExplicitActionResult<T>(this ApiResult<T> result)
        {
            switch (result.ResultType)
            {
                case ApiResultTypeEnum.Success:
                    return result;
                case ApiResultTypeEnum.Failed:
                    return new BadRequestObjectResult(result);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static ActionResult<ApiResult> ToExplicitActionResult(this ApiResult result)
        {
            switch (result.ResultType)
            {
                case ApiResultTypeEnum.Success:
                    return result;
                case ApiResultTypeEnum.Failed:
                    return new BadRequestObjectResult(result);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}