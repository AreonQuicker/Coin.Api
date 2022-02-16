using Newtonsoft.Json;

namespace CoinApi.Integration.Core.Responses
{
    public class IntegrationClientResponseBase<T> where T : class
    {
        [JsonProperty("data")] public T Data { get; set; }

        [JsonProperty("status")] public StatusIntegrationClientResponse Status { get; set; }
    }
}