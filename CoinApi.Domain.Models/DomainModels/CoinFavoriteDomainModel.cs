using System.Collections.Generic;

namespace CoinApi.Domain.Models.DomainModels
{
    public class CoinFavoriteDomainModel : AuditDomainModel
    {
        public CoinFavoriteDomainModel()
        {
            FavoriteCurrencies = new HashSet<CurrencyFavoriteDomainModel>();
        }

        public int Id { get; set; }
        public string Symbol { get; set; }
        public ICollection<CurrencyFavoriteDomainModel> FavoriteCurrencies { get; set; }
    }
}