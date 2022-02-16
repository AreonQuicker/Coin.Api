using CoinApi.Domain.Common.Models;
using CoinApi.Domain.CryptoCurrency.Models;
using MediatR;

namespace CoinApi.Application.Core.CryptoCurrency.Queries
{
    public class GetCryptoCurrenciesQuery : IRequest<PaginatedList<CryptoCurrencyResult>>
    {
        public GetCryptoCurrenciesQuery(int pageNumber = 1, int? pageSize = null)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; }
        public int? PageSize { get; }

        public string Symbol { get; init; }
    }
}