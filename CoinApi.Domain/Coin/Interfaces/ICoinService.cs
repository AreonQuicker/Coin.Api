using System.Threading.Tasks;
using CoinApi.Domain.Coin.Models;

namespace CoinApi.Domain.Coin.Interfaces
{
    /// <summary>
    /// Coin CRUD service
    /// </summary>
    public interface ICoinService
    {
        /// <summary>
        /// Get coin by symbol
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <returns>Coin result</returns>
        Task<CoinResult> GetBySymbolAsync(string symbol);
    }
}