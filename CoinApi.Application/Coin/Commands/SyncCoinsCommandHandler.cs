using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoinApi.Application.Core.Coin.Commands;
using CoinApi.Application.Core.Integration.Queries;
using CoinApi.Domain.Coin.Models;
using MediatR;

namespace CoinApi.Application.Coin.Commands
{
    /// <summary>
    /// Sync coins by fetching coins from outside/client and save in database
    /// </summary>
    public class SyncCoinsCommandHandler : IRequestHandler<SyncCoinsCommand>
    {
        private readonly ISender _sender;

        public SyncCoinsCommandHandler(ISender sender)
        {
            _sender = sender;
        }

        public async Task<Unit> Handle(SyncCoinsCommand request, CancellationToken cancellationToken)
        {
            //Fetch coins from client
            var coins = await _sender.Send(new GetCoinsFromIntegrationClientQuery(), cancellationToken);

            if (coins?.Data is null)
                return Unit.Value;

            //Split all data into chunks of 100 for better performance
            var dataChunks = coins.Data.Chunk(1000);

            foreach (var chunk in dataChunks)
            {
                if (cancellationToken.IsCancellationRequested)
                    continue;
                
                var requests = chunk
                    .Select(s => new CoinCreateRequest
                    {
                        Name = s.Name,
                        Rank = s.Rank,
                        Slug = s.Slug,
                        Symbol = s.Symbol
                    })
                    .ToList();

                //Delete and save coins in database
                await _sender.Send(new DeleteAndAddCoinsCommand(requests), cancellationToken);
            }

            return Unit.Value;
        }
    }
}