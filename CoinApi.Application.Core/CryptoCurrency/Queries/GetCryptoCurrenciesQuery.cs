using CoinApi.Domain.Common.Enums;
using CoinApi.Domain.Common.Models;
using CoinApi.Domain.CryptoCurrency.Models;
using MediatR;

namespace CoinApi.Application.Core.CryptoCurrency.Queries
{
    public class GetCryptoCurrenciesQuery : IRequest<PaginatedList<CryptoCurrencyResult>>
    {
        public GetCryptoCurrenciesQuery(int pageNumber = 1, int? pageSize = null, string sortKey = null,SortOrderTypeEnum sortType = SortOrderTypeEnum.ASC)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SortKey = sortKey;
            SortType = sortType;
        }

        public int PageNumber { get; }
        public int? PageSize { get; }
        public string SortKey { get; }
        public SortOrderTypeEnum SortType { get; }
        public string Symbol { get; init; }
    }
}