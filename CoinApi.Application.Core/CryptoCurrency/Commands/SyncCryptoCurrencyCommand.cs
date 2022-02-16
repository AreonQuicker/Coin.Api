using MediatR;

namespace CoinApi.Application.Core.CryptoCurrency.Commands
{
    public class SyncCryptoCurrencyCommand : IRequest<Unit>
    {
        public SyncCryptoCurrencyCommand(params string[] symbols)
        {
            Symbols = symbols;
        }

        public string[] Symbols { get; }
    }
}