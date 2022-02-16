using System.Collections.Generic;
using CoinApi.Integration.Core.Responses;
using MediatR;

namespace CoinApi.Application.Core.Integration.Queries
{
    public class
        GetCryptocurrencyFromIntegrationClientQuery : IRequest<
            IntegrationClientResponseBase<Dictionary<string, CryptoCurrencyIntegrationClientResponse>>>
    {
        public GetCryptocurrencyFromIntegrationClientQuery(string symbol, List<string> currencies)
        {
            Symbol = symbol;
            Currencies = currencies;
        }

        public string Symbol { get; }
        public List<string> Currencies { get; }
    }
}