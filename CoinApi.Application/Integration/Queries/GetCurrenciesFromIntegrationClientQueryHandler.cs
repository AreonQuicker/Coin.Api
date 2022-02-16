using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoinApi.Application.Core.Integration.Queries;
using CoinApi.Integration.Core.Interfaces;
using CoinApi.Integration.Core.Responses;
using MediatR;

namespace CoinApi.Application.Integration.Queries
{
    /// <summary>
    /// Fetch currencies from outside client
    /// </summary>
    public class GetCurrenciesFromIntegrationClientQueryHandler : IRequestHandler<
        GetCurrenciesFromIntegrationClientQuery,
        IntegrationClientResponseBase<IList<CurrencyIntegrationClientResponse>>>
    {
        private readonly ICoinIntegrationClientService _coinIntegrationClientService;

        public GetCurrenciesFromIntegrationClientQueryHandler(
            ICoinIntegrationClientService coinIntegrationClientService)
        {
            _coinIntegrationClientService = coinIntegrationClientService;
        }

        public Task<IntegrationClientResponseBase<IList<CurrencyIntegrationClientResponse>>> Handle(
            GetCurrenciesFromIntegrationClientQuery request, CancellationToken cancellationToken)
        {
            return _coinIntegrationClientService.GetCurrenciesAsync();
        }
    }
}