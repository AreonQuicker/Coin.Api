using CoinApi.Domain.Common.Interfaces;
using CoinApi.Domain.Currency.Models;

namespace CoinApi.Domain.Currency.Interfaces
{
    /// <summary>
    /// Get paginated list of currencies
    /// Fluent builder service be used
    /// </summary>
    public interface IGetPaginatedCurrenciesFluentService : IFluentGetPaginatedService<CurrencyResult>
    {
    
        /// <summary>
        /// Specify the symbol to be used to get the results
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <returns></returns>
        IGetPaginatedCurrenciesFluentService WithSymbol(string symbol);
    }
}