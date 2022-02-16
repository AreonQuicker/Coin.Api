using System.Collections.Generic;
using CoinApi.Integration.Core.Responses;
using MediatR;

namespace CoinApi.Application.Core.Integration.Queries
{
    public class
        GetCurrenciesFromIntegrationClientQuery : IRequest<
            IntegrationClientResponseBase<IList<CurrencyIntegrationClientResponse>>>
    {
    }
}