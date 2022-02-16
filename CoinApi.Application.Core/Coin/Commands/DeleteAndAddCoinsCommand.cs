using System.Collections.Generic;
using CoinApi.Domain.Coin.Models;
using MediatR;

namespace CoinApi.Application.Core.Coin.Commands
{
    public class DeleteAndAddCoinsCommand : IRequest<Unit>
    {
        public DeleteAndAddCoinsCommand(List<CoinCreateRequest> coinCreateRequests)
        {
            CoinCreateRequests = coinCreateRequests;
        }

        public List<CoinCreateRequest> CoinCreateRequests { get; }
    }
}