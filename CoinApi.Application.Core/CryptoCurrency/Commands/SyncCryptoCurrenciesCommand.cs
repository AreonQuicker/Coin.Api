using MediatR;

namespace CoinApi.Application.Core.CryptoCurrency.Commands
{
    public class SyncCryptoCurrenciesCommand : IRequest<Unit>
    {
    }
}