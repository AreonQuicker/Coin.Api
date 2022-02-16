using System.Threading;
using System.Threading.Tasks;
using CoinApi.Application.Core.Currency.Commands;
using CoinApi.Domain.Currency.Interfaces;
using MediatR;

namespace CoinApi.Application.Currency.Commands
{
    /// <summary>
    /// Delete and add currencies in database
    /// </summary>
    public class DeleteAndAddCurrenciesCommandHandler : IRequestHandler<DeleteAndAddCurrenciesCommand>
    {
        private readonly IDeleteAndAddCurrencyFluentService _deleteAndAddCurrencyFluentService;


        public DeleteAndAddCurrenciesCommandHandler(
            IDeleteAndAddCurrencyFluentService deleteAndAddCurrencyFluentService)
        {
            _deleteAndAddCurrencyFluentService = deleteAndAddCurrencyFluentService;
        }

        public async Task<Unit> Handle(DeleteAndAddCurrenciesCommand request, CancellationToken cancellationToken)
        {
            await _deleteAndAddCurrencyFluentService
                .WithRequest(request.CurrencyDeleteAndAddRequests)
                //.WithTransaction(IsolationLevel.RepeatableRead)
                .ExecuteAsync();

            return Unit.Value;
        }
    }
}