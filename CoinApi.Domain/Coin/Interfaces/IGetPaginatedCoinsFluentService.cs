using CoinApi.Domain.Coin.Models;
using CoinApi.Domain.Common.Interfaces;

namespace CoinApi.Domain.Coin.Interfaces
{
    /// <summary>
    /// Get paginated list of coins
    /// Fluent builder service be used
    /// </summary>
    public interface IGetPaginatedCoinsFluentService : IFluentGetPaginatedService<CoinResult>
    {
        /// <summary>
        /// Specify the rank to be used to get the results
        /// </summary>
        /// <param name="rank">Rank</param>
        /// <returns></returns>
        IGetPaginatedCoinsFluentService WithRank(int? rank);
        
        /// <summary>
        /// Specify the symbol to be used to get the results
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <returns></returns>
        IGetPaginatedCoinsFluentService WithSymbol(string symbol);
    }
}