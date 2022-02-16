using CoinApi.Domain.Common.Mapping.Interfaces;
using CoinApi.Domain.Models.DomainModels;

namespace CoinApi.Domain.Coin.Models
{
    public class CoinCreateRequest : CoinDomainModel, IMapFrom<CoinDomainModel>
    {
    }
}