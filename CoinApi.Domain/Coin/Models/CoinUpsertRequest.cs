using CoinApi.Domain.Common.Mapping.Interfaces;
using CoinApi.Domain.Models.DomainModels;

namespace CoinApi.Domain.Coin.Models
{
    public class CoinUpsertRequest : CoinDomainModel, IMapFrom<CoinCreateRequest>, IMapFrom<CoinUpdateRequest>
    {
    }
}