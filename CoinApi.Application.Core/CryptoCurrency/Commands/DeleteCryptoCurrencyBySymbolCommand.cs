using MediatR;

namespace CoinApi.Application.Core.CryptoCurrency.Commands
{
    public class DeleteCryptoCurrencyBySymbolCommand : IRequest<Unit>
    {
        public DeleteCryptoCurrencyBySymbolCommand(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; }
    }
}