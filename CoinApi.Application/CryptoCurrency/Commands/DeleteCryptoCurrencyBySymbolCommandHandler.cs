using System.Threading;
using System.Threading.Tasks;
using CoinApi.Application.Core.CryptoCurrency.Commands;
using CoinApi.Domain.CryptoCurrency.Interfaces;
using MediatR;

namespace CoinApi.Application.CryptoCurrency.Commands
{
    /// <summary>
    /// Delete crypto rate by symbol in database
    /// </summary>
    public class DeleteCryptoCurrencyBySymbolCommandHandler : IRequestHandler<DeleteCryptoCurrencyBySymbolCommand>
    {
        private readonly ICryptoCurrencyService _cryptoCurrencyService;

        public DeleteCryptoCurrencyBySymbolCommandHandler(ICryptoCurrencyService cryptoCurrencyService)
        {
            _cryptoCurrencyService = cryptoCurrencyService;
        }

        public async Task<Unit> Handle(DeleteCryptoCurrencyBySymbolCommand request, CancellationToken cancellationToken)
        {
            await _cryptoCurrencyService.DeleteBySymbolAsync(request.Symbol);

            return Unit.Value;
        }
    }
}