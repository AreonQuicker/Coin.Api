using System.Threading;
using System.Threading.Tasks;
using CoinApi.Application.Core.Currency.Queries;
using CoinApi.Domain.Common.Models;
using CoinApi.Domain.Currency.Interfaces;
using CoinApi.Domain.Currency.Models;
using MediatR;

namespace CoinApi.Application.Currency.Queries
{
    /// <summary>
    /// Get currency from database
    /// </summary>
    public class GetCurrenciesQueryHandler : IRequestHandler<GetCurrenciesQuery, PaginatedList<CurrencyResult>>
    {
        private readonly IGetPaginatedCurrenciesFluentService _getPaginatedCurrenciesFluentService;

        public GetCurrenciesQueryHandler(IGetPaginatedCurrenciesFluentService getPaginatedCurrenciesFluentService)
        {
            _getPaginatedCurrenciesFluentService = getPaginatedCurrenciesFluentService;
        }

        public Task<PaginatedList<CurrencyResult>> Handle(GetCurrenciesQuery request,
            CancellationToken cancellationToken)
        {
            return
                _getPaginatedCurrenciesFluentService
                    .WithSymbol(request.Symbol)
                    .WithPaginatedInfo((request.PageNumber, request.PageSize))
                    .WithSortInfo(new SortInfo(request.SortKey, request.SortType))
                    .GetAsync();
        }
    }
}