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
    /// Fetch coins fron outside client
    /// </summary>
    public class GetCoinsFromIntegrationClientQueryHandler : IRequestHandler<GetCoinsFromIntegrationClientQuery,
        IntegrationClientResponseBase<IList<CoinIntegrationClientResponse>>>
    {
        private readonly ICoinIntegrationClientService _coinIntegrationClientService;

        public GetCoinsFromIntegrationClientQueryHandler(ICoinIntegrationClientService coinIntegrationClientService)
        {
            _coinIntegrationClientService = coinIntegrationClientService;
        }

        public Task<IntegrationClientResponseBase<IList<CoinIntegrationClientResponse>>> Handle(
            GetCoinsFromIntegrationClientQuery request, CancellationToken cancellationToken)
        {
            return _coinIntegrationClientService.GetCoinsAsync();
        }
    }
}