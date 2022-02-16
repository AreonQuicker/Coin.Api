using Newtonsoft.Json;

namespace CoinApi.Integration.Core.Responses
{
    public class CurrencyIntegrationClientResponse
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("symbol")] public string Symbol { get; set; }

        [JsonProperty("sign")] public string Sign { get; set; }
    }
}