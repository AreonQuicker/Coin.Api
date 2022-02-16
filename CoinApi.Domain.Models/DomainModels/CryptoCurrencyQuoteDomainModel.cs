using Newtonsoft.Json;

namespace CoinApi.Domain.Models.DomainModels
{
    public class CryptoCurrencyQuoteDomainModel : AuditDomainModel
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public decimal Volume24H { get; set; }
        public decimal MarketCap { get; set; }

        public int CryptoCurrencyId { get; set; }

        [JsonIgnore] public CryptoCurrencyDomainModel CryptoCurrency { get; set; }
    }
}