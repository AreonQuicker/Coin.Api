using Newtonsoft.Json;

namespace CoinApi.Integration.Core.Responses
{
    public class CryptoCurrencyQuoteIntegrationClientResponse
    {
        [JsonProperty("price")] public decimal Price { get; set; }
        [JsonProperty("volume_24h")] public decimal Volume24H { get; set; }
        [JsonProperty("market_cap")] public decimal MarketCap { get; set; }
    }
}