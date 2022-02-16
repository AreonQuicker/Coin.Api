using System.Threading.Tasks;
using CoinApi.Domain.Currency.Models;

namespace CoinApi.Domain.Currency.Interfaces
{
    /// <summary>
    /// Currency CRUD service
    /// </summary>
    public interface ICurrencyService
    {
        /// <summary>
        /// Get currency by symbol
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <returns>Currency result</returns>
        Task<CurrencyResult> GetBySymbolAsync(string symbol);
    }
}