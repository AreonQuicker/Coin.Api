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
    /// Fetch crypto rates from outside client
    /// </summary>
    public class GetCryptocurrencyFromIntegrationClientQueryHandler : IRequestHandler<
        GetCryptocurrencyFromIntegrationClientQuery,
        IntegrationClientResponseBase<Dictionary<string, CryptoCurrencyIntegrationClientResponse>>>
    {
        private readonly ICoinIntegrationClientService _coinIntegrationClientService;

        public GetCryptocurrencyFromIntegrationClientQueryHandler(
            ICoinIntegrationClientService coinIntegrationClientService)
        {
            _coinIntegrationClientService = coinIntegrationClientService;
        }

        public Task<IntegrationClientResponseBase<Dictionary<string, CryptoCurrencyIntegrationClientResponse>>> Handle(
            GetCryptocurrencyFromIntegrationClientQuery request, CancellationToken cancellationToken)
        {
            return _coinIntegrationClientService.GetCryptocurrencyAsync(request.Symbol, request.Currencies);
        }
    }
}