using System.Collections.Generic;
using CoinApi.Domain.Coin.Models;
using CoinApi.Domain.Common.Interfaces;

namespace CoinApi.Domain.Coin.Interfaces
{
    /// <summary>
    /// Delete and Add coins
    /// Fluent Builder service will be used
    /// </summary>
    public interface IDeleteAndAddCoinFluentService : IFluentActionService
    {
 
        /// <summary>
        /// Specify the request for performing the delete and add action
        /// </summary>
        /// <param name="coinCreateRequests">Delete and Add request</param>
        /// <returns></returns>
        IDeleteAndAddCoinFluentService WithRequest(List<CoinCreateRequest> coinCreateRequests);
    }
}