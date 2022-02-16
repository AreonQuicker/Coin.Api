using Newtonsoft.Json;

namespace CoinApi.Domain.Models.DomainModels
{
    public class CurrencyFavoriteDomainModel : AuditDomainModel
    {
        public int Id { get; set; }

        public string Symbol { get; set; }

        [JsonIgnore] public CoinFavoriteDomainModel CoinFavorite { get; set; }

        public int CoinFavoriteId { get; set; }
    }
}