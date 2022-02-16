using System.Linq;
using CoinApi.Domain.Common.Exceptions;
using CoinApi.Extensions;
using CoinApi.Models;
using E.S.Logging.Enums;
using E.S.Logging.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace CoinApi.Filters
{
    /// <summary>
    /// Exception attribute filter for logging the exception and returning a wrapped Api result
    /// </summary>
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        public ApiExceptionFilterAttribute(ILoggerFactory loggerFactory)
        {
            var categoryName = GetType()
                .FullName;
            if (categoryName != null)
                _logger = loggerFactory.CreateLogger(categoryName);
        }

        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            _logger.LogErrorOperation(LoggerStatusEnum.Error,
                "Api",
                null,
                null,
                null,
                null,
                exception);

            var actionResult = new ApiResult(null, null, exception.Message, exception.Message);

            if (exception.GetType().GetInterfaces().Any(a => a == typeof(IServiceException)))
            {
                var ex = (IServiceException) exception;

                actionResult = new ApiResult(ex.ErrorCode, "", exception.Message, exception.Message);

                context.Result
                    = actionResult
                        .ToActionResult();

                context.ExceptionHandled = true;

                return;
            }

            if (exception.GetType() == typeof(IntegrationClientException))
            {
                var ex = (IntegrationClientException) exception;
                actionResult = new ApiResult(ex.ErrorCode, ex.InternalErrorCode.ToString(),
                    exception.Message, exception.Message);

                context.Result
                    = actionResult
                        .ToActionResult();

                context.ExceptionHandled = true;

                return;
            }


            context.Result
                = actionResult
                    .ToActionResult();

            context.ExceptionHandled = true;
        }
    }
}