using System.Threading;
using System.Threading.Tasks;
using E.S.Logging.Enums;
using E.S.Logging.Extensions;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace CoinApi.Application.Common.Behaviours
{
    /// <summary>
    /// log all request details passing through the application
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;

        public LoggingBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            var message = $"New Request: {requestName}";

            _logger.LogInformationOperation(LoggerStatusEnum.InProgress, "Application", requestName, null, null,
                message);
        }
    }
}