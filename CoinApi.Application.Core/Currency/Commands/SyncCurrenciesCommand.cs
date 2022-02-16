using MediatR;

namespace CoinApi.Application.Core.Currency.Commands
{
    public class SyncCurrenciesCommand : IRequest<Unit>
    {
    }
}