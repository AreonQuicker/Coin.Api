using System.Collections.Generic;
using CoinApi.Domain.Currency.Models;
using MediatR;

namespace CoinApi.Application.Core.Currency.Commands
{
    public class DeleteAndAddCurrenciesCommand : IRequest<Unit>
    {
        public DeleteAndAddCurrenciesCommand(List<CurrencyDeleteAndAddRequest> currencyDeleteAndAddRequests)
        {
            CurrencyDeleteAndAddRequests = currencyDeleteAndAddRequests;
        }

        public List<CurrencyDeleteAndAddRequest> CurrencyDeleteAndAddRequests { get; }
    }
}