using System.Threading;
using System.Threading.Tasks;
using CoinApi.Application.Core.Coin.Commands;
using CoinApi.Domain.Coin.Interfaces;
using MediatR;

namespace CoinApi.Application.Coin.Commands
{
    /// <summary>
    /// Delete and And coins in bulk in database
    /// </summary>
    public class DeleteAndAddCoinsCommandHandler : IRequestHandler<DeleteAndAddCoinsCommand>
    {
        private readonly IDeleteAndAddCoinFluentService _deleteAndAddCoinFluentService;

        public DeleteAndAddCoinsCommandHandler(IDeleteAndAddCoinFluentService deleteAndAddCoinFluentService)
        {
            _deleteAndAddCoinFluentService = deleteAndAddCoinFluentService;
        }

        public async Task<Unit> Handle(DeleteAndAddCoinsCommand request, CancellationToken cancellationToken)
        {
            await _deleteAndAddCoinFluentService
                .WithRequest(request.CoinCreateRequests)
                //.WithTransaction(IsolationLevel.RepeatableRead)
                .ExecuteAsync();

            return Unit.Value;
        }
    }
}