using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoinApi.Application.Core.Currency.Commands;
using CoinApi.Application.Core.Integration.Queries;
using CoinApi.Domain.Currency.Models;
using MediatR;

namespace CoinApi.Application.Currency.Commands
{
    /// <summary>
    /// Sync currencies from outside client to database
    /// </summary>
    public class SyncCurrenciesCommandHandler : IRequestHandler<SyncCurrenciesCommand>
    {
        private readonly ISender _sender;

        public SyncCurrenciesCommandHandler(ISender sender)
        {
            _sender = sender;
        }

        public async Task<Unit> Handle(SyncCurrenciesCommand request, CancellationToken cancellationToken)
        {
            //Fetch currencies from outside client
            var results = await _sender.Send(new GetCurrenciesFromIntegrationClientQuery(), cancellationToken);

            if (results?.Data is null)
                return Unit.Value;

            //Split data in chunks of 100 for better performance
            var dataChunks = results.Data.Chunk(1000);

            foreach (var chunk in dataChunks)
            {
                if (cancellationToken.IsCancellationRequested)
                    continue;

                var requests = chunk
                    .Select(s => new CurrencyDeleteAndAddRequest
                    {
                        Name = s.Name,
                        Sign = s.Sign,
                        Symbol = s.Symbol
                    })
                    .ToList();

                //Creat currencies in database
                await _sender.Send(new DeleteAndAddCurrenciesCommand(requests), cancellationToken);
            }

            return Unit.Value;
        }
    }
}