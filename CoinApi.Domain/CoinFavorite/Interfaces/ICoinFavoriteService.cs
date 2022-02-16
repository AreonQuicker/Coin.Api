using System.Collections.Generic;
using System.Threading.Tasks;
using CoinApi.Domain.CoinFavorite.Models;

namespace CoinApi.Domain.CoinFavorite.Interfaces
{
    /// <summary>
    /// Coind favorite CRUD service
    /// </summary>
    public interface ICoinFavoriteService
    {
        /// <summary>
        /// Get all coin favorites
        /// </summary>
        /// <returns></returns>
        Task<IList<CoinFavoriteResult>> GetAsync();
        
        /// <summary>
        /// Get coin favorite by symbol
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <returns>Coin favorite</returns>
        Task<CoinFavoriteResult> GetBySymbolAsync(string symbol);
        
        /// <summary>
        /// Create a new coin favorite
        /// </summary>
        /// <param name="coinFavoriteCreateRequest">Create request</param>
        /// <returns>Created Id</returns>
        Task<int> CreateAsync(CoinFavoriteCreateRequest coinFavoriteCreateRequest);
        
        /// <summary>
        /// Update coin favorite by symbol
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="coinFavoriteCreateRequest">Update request</param>
        /// <returns>Updated Id</returns>
        Task<int> UpdateAsync(string symbol, CoinFavoriteUpdateRequest coinFavoriteCreateRequest);
        
        /// <summary>
        /// Delete coin favorite by symbol
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <returns>Deleted Id</returns>
        Task<int> DeleteBySymbolAsync(string symbol);
    }
}