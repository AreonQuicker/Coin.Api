using System;
using System.Threading;
using System.Threading.Tasks;
using CoinApi.Background.Queue.Core.Enums;
using CoinApi.Background.Queue.Core.Interfaces;
using CoinApi.Domain.Common.Configurations;
using CoinApi.Hangfire.Core.Interfaces;
using E.S.Logging.Enums;
using E.S.Logging.Extensions;
using Hangfire;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CoinApi.Background.Queue.Services
{
    /// <summary>
    /// Hosted service be use to listen for new queued up requests
    /// </summary>
    public sealed class QueuedHostedService : BackgroundService
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IOptions<CoinApiCronJobsConfiguration> _cronjobOptions;
        private readonly ILogger<QueuedHostedService> _logger;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly IBackgroundQueue _taskQueue;

        public QueuedHostedService(
            IBackgroundQueue taskQueue,
            ILogger<QueuedHostedService> logger,
            IRecurringJobManager recurringJobManager,
            IOptions<CoinApiCronJobsConfiguration> cronjobOptions,
            IBackgroundJobClient backgroundJobClient)
        {
            _recurringJobManager = recurringJobManager;
            _cronjobOptions = cronjobOptions;
            _backgroundJobClient = backgroundJobClient;
            (_taskQueue, _logger) = (taskQueue, logger);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return ProcessTaskQueueAsync(stoppingToken);
        }

        private async Task ProcessTaskQueueAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
                try
                {
                    //Dequeue request from queue
                    var request =
                        await _taskQueue.DequeueAsync(stoppingToken);

                    //Depending on the request type, trigger hangfire to process the job by firing the hangfire action
                    switch (request.Type)
                    {
                        case BackgroundQueueRequestTypeEnum.SyncCoinsInBackground:
                            _recurringJobManager.AddOrUpdate<ICoinHangfireService>(
                                BackgroundQueueRequestTypeEnum.SyncCoinsInBackground.ToString(),
                                x => x.SyncCoins(stoppingToken), _cronjobOptions.Value.SyncCoinsCronJob);
                            break;
                        case BackgroundQueueRequestTypeEnum.SyncCurrenciesInBackground:
                            _recurringJobManager.AddOrUpdate<ICoinHangfireService>(
                                BackgroundQueueRequestTypeEnum.SyncCurrenciesInBackground.ToString(),
                                x => x.SyncCurrencies(stoppingToken), _cronjobOptions.Value.SyncCurrenciesCronJob);
                            break;
                        case BackgroundQueueRequestTypeEnum.SyncCryptoCurrenciesInBackground:
                            _recurringJobManager.AddOrUpdate<ICoinHangfireService>(
                                BackgroundQueueRequestTypeEnum.SyncCryptoCurrenciesInBackground.ToString(),
                                x => x.SyncCryptoCurrencies(stoppingToken),
                                _cronjobOptions.Value.SyncCryptoCurrenciesCronJob);
                            break;
                        case BackgroundQueueRequestTypeEnum.SyncCryptoCurrency:
                            _backgroundJobClient.Enqueue<ICoinHangfireService>(s =>
                                s.SyncCryptoCurrency(request.Keys, stoppingToken));
                            break;
                        case BackgroundQueueRequestTypeEnum.SyncCoins:
                            _backgroundJobClient.Enqueue<ICoinHangfireService>(s =>
                                s.SyncCoins(stoppingToken));
                            break;
                        case BackgroundQueueRequestTypeEnum.SyncCurrencies:
                            _backgroundJobClient.Enqueue<ICoinHangfireService>(s =>
                                s.SyncCurrencies(stoppingToken));
                            break;
                    }
                }
                catch (OperationCanceledException)
                {
                    // Prevent throwing if stoppingToken was signaled
                }
                catch (Exception ex)
                {
                    _logger.LogErrorOperation(LoggerStatusEnum.Error, "Queue Background", null, null, null,
                        "Error occurred executing task work item.", ex);
                }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            //On stop of application remove all recurring jobs
            _recurringJobManager.RemoveIfExists(BackgroundQueueRequestTypeEnum.SyncCoinsInBackground.ToString());
            _recurringJobManager.RemoveIfExists(BackgroundQueueRequestTypeEnum.SyncCurrenciesInBackground.ToString());
            _recurringJobManager.RemoveIfExists(
                BackgroundQueueRequestTypeEnum.SyncCryptoCurrenciesInBackground.ToString());

            _logger.LogInformationOperation(LoggerStatusEnum.EndWithSucces, "Queue Background",
                nameof(QueuedHostedService), null, null,
                $"{nameof(QueuedHostedService)} is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}