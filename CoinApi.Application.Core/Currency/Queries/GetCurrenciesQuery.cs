using CoinApi.Domain.Common.Models;
using CoinApi.Domain.Currency.Models;
using MediatR;

namespace CoinApi.Application.Core.Currency.Queries
{
    public class GetCurrenciesQuery : IRequest<PaginatedList<CurrencyResult>>
    {
        public GetCurrenciesQuery(int pageNumber = 1, int? pageSize = null)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; }
        public int? PageSize { get; }

        public string Symbol { get; init; }
    }
}