using System;
using System.Threading;
using System.Threading.Tasks;
using CoinApi.Domain.Common.Exceptions;
using E.S.Logging.Enums;
using E.S.Logging.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CoinApi.Application.Common.Behaviours
{
    /// <summary>
    /// Log unhandled requests that pass through the application
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (IntegrationClientException ex)
            {
                var requestName = typeof(TRequest).Name;
                var message = $"Integration Client Exception for New Request {requestName}";

                _logger.LogErrorOperationWithExtraFormat(LoggerStatusEnum.Error, "Application", requestName,
                    ex.ErrorCode, null, message, ex,
                    "StatusCode:{statusCode} InternalErrorCode:{internalErrorCode} InternalErrorMessage{}",
                    ex.StatusCode?.ToString(), ex.InternalErrorCode?.ToString(),
                    ex.InternalErrorMessage);

                throw;
            }
            catch (AlreadyExistException ex)
            {
                var requestName = typeof(TRequest).Name;
                var message = $"Already Exist Exception for New Request {requestName}";

                _logger.LogErrorOperation(LoggerStatusEnum.Error, "Application", requestName, ex.ErrorCode, null,
                    message, ex);

                throw;
            }
            catch (NotFoundException ex)
            {
                var requestName = typeof(TRequest).Name;
                var message = $"Not Found Exception for New Request {requestName}";

                _logger.LogErrorOperation(LoggerStatusEnum.Error, "Application", requestName, ex.ErrorCode, null,
                    message, ex);

                throw;
            }
            catch (ValidationException ex)
            {
                var requestName = typeof(TRequest).Name;
                var message = $"Validation Exception for New Request {requestName}";

                _logger.LogErrorOperation(LoggerStatusEnum.Error, "Application", requestName, ex.ErrorCode, null,
                    message, ex);

                throw;
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                var message = $"Unhandled Exception for New Request {requestName}";

                _logger.LogErrorOperation(LoggerStatusEnum.Error, "Application", requestName, null, null,
                    message, ex);

                throw;
            }
        }
    }
}