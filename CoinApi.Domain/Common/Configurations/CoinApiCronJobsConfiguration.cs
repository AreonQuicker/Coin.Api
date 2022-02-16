namespace CoinApi.Domain.Common.Configurations
{
    public class CoinApiCronJobsConfiguration
    {
        public string SyncCoinsCronJob { get; set; }
        public string SyncCurrenciesCronJob { get; set; }
        public string SyncCryptoCurrenciesCronJob { get; set; }
    }
}