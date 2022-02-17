using System.Threading;
using System.Threading.Tasks;
using CoinApi.Application.Core.CryptoCurrency.Queries;
using CoinApi.Domain.Common.Models;
using CoinApi.Domain.CryptoCurrency.Interfaces;
using CoinApi.Domain.CryptoCurrency.Models;
using MediatR;

namespace CoinApi.Application.CryptoCurrency.Queries
{
    /// <summary>
    /// Get crypto rates from database
    /// </summary>
    public class CryptoCurrencyQueryHandler : IRequestHandler<GetCryptoCurrencyBySymbolQuery, CryptoCurrencyResult>,
        IRequestHandler<GetCryptoCurrenciesQuery, PaginatedList<CryptoCurrencyResult>>
    {
        private readonly ICryptoCurrencyService _cryptoCurrencyService;
        private readonly IGetPaginatedCryptoCurrenciesFluentService _getPaginatedCryptoCurrenciesFluentService;

        public CryptoCurrencyQueryHandler(ICryptoCurrencyService cryptoCurrencyService,
            IGetPaginatedCryptoCurrenciesFluentService getPaginatedCryptoCurrenciesFluentService)
        {
            _cryptoCurrencyService = cryptoCurrencyService;
            _getPaginatedCryptoCurrenciesFluentService = getPaginatedCryptoCurrenciesFluentService;
        }

        
        /// <summary>
        /// Get paginated crypto rates from database
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<PaginatedList<CryptoCurrencyResult>> Handle(GetCryptoCurrenciesQuery request,
            CancellationToken cancellationToken)
        {
            return
                _getPaginatedCryptoCurrenciesFluentService
                    .WithSymbol(request.Symbol)
                    .WithPaginatedInfo((request.PageNumber, request.PageSize))
                    .WithSortInfo(new SortInfo(request.SortKey, request.SortType))
                    .GetAsync();
        }

        /// <summary>
        /// Get crypto rate by symbol
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<CryptoCurrencyResult> Handle(GetCryptoCurrencyBySymbolQuery request,
            CancellationToken cancellationToken)
        {
            return _cryptoCurrencyService.GetBySymbolAsync(request.Symbol);
        }
    }
}