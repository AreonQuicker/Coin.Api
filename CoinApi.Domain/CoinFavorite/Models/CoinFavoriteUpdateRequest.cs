using System.Collections.Generic;
using CoinApi.Domain.Common.Mapping.Interfaces;
using CoinApi.Domain.Models.DomainModels;

namespace CoinApi.Domain.CoinFavorite.Models
{
    public class CoinFavoriteUpdateRequest : IMapFrom<CoinFavoriteDomainModel>
    {
        public List<CurrencyFavoriteCreateRequest> FavoriteCurrencies { get; set; }
    }
}