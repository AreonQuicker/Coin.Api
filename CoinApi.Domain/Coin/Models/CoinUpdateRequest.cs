using CoinApi.Domain.Common.Mapping.Interfaces;
using CoinApi.Domain.Models.DomainModels;

namespace CoinApi.Domain.Coin.Models
{
    public class CoinUpdateRequest : IMapFrom<CoinDomainModel>
    {
        public int Rank { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Symbol { get; set; }
    }
}