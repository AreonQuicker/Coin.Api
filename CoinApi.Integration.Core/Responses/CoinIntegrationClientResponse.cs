using Newtonsoft.Json;

namespace CoinApi.Integration.Core.Responses
{
    public class CoinIntegrationClientResponse
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("rank")] public int Rank { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("symbol")] public string Symbol { get; set; }

        [JsonProperty("slug")] public string Slug { get; set; }
    }
}