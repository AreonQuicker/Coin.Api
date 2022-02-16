using System.Threading.Tasks;
using CoinApi.Domain.CryptoCurrency.Models;

namespace CoinApi.Domain.CryptoCurrency.Interfaces
{
    /// <summary>
    /// Crypto rate CRUD service
    /// </summary>
    public interface ICryptoCurrencyService
    {
        /// <summary>
        /// Create or update crypto rate
        /// </summary>
        /// <param name="cryptoCurrencyUpsertRequests">Upsert request</param>
        /// <returns></returns>
        Task UpsertAsync(params CryptoCurrencyUpsertRequest[] cryptoCurrencyUpsertRequests);
        
        /// <summary>
        /// Get crypto rate by symbol
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <returns></returns>
        Task<CryptoCurrencyResult> GetBySymbolAsync(string symbol);
        
        /// <summary>
        /// Delete crypto rate by symbol
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <returns></returns>
        Task DeleteBySymbolAsync(string symbol);
    }
}