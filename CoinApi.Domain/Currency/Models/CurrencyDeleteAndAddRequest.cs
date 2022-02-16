using CoinApi.Domain.Common.Mapping.Interfaces;
using CoinApi.Domain.Models.DomainModels;

namespace CoinApi.Domain.Currency.Models
{
    public class CurrencyDeleteAndAddRequest : CurrencyDomainModel, IMapFrom<CurrencyDomainModel>
    {
    }
}