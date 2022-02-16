using CoinApi.Domain.Common.Mapping.Interfaces;
using CoinApi.Domain.Models.DomainModels;

namespace CoinApi.Domain.CoinFavorite.Models
{
    public class CurrencyFavoriteCreateRequest : IMapFrom<CurrencyFavoriteDomainModel>
    {
        public string Symbol { get; set; }
    }
}