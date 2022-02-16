using System.Collections.Generic;
using CoinApi.Domain.Common.Mapping.Interfaces;
using CoinApi.Domain.Models.DomainModels;

namespace CoinApi.Domain.CoinFavorite.Models
{
    public class CoinFavoriteCreateRequest : IMapFrom<CoinFavoriteDomainModel>
    {
        public string Symbol { get; set; }
        public List<CurrencyFavoriteCreateRequest> FavoriteCurrencies { get; set; }
    }
}