using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using E.S.Logging.Enums;
using E.S.Logging.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CoinApi.Application.Common.Behaviours
{
    /// <summary>
    /// Log requests that exceed the execution time specified. For logging purposes
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly Stopwatch _timer;

        public PerformanceBehaviour(
            ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();

            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds <= 800) return response;

            var requestName = typeof(TRequest).Name;

            var message = $"New Request: {requestName} ({elapsedMilliseconds} milliseconds)";

            _logger.LogWarningOperation(LoggerStatusEnum.InProgress, "Application", requestName, null, null,
                message);

            return response;
        }
    }
}