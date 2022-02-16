using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CoinApi.Integration.Core.Responses
{
    public class CryptoCurrencyIntegrationClientResponse
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("symbol")] public string Symbol { get; set; }

        [JsonProperty("slug")] public string Slug { get; set; }

        [JsonProperty("cmc_rank")] public decimal CmcRank { get; set; }

        [JsonProperty("num_market_pairs")] public decimal NumMarketPairs { get; set; }

        [JsonProperty("last_updated")] public DateTime LastUpdated { get; set; }

        [JsonProperty("quote")]
        public Dictionary<string, CryptoCurrencyQuoteIntegrationClientResponse> Quotes { get; set; }
    }
}