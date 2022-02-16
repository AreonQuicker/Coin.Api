using CoinApi.Domain.Common.Interfaces;
using CoinApi.Domain.CryptoCurrency.Models;

namespace CoinApi.Domain.CryptoCurrency.Interfaces
{
    /// <summary>
    /// Get paginated list of crypto rates
    /// Fluent builder service be used
    /// </summary>
    public interface IGetPaginatedCryptoCurrenciesFluentService : IFluentGetPaginatedService<CryptoCurrencyResult>
    {
        /// <summary>
        /// Specify the symbol to be used to get the results
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <returns></returns>
        IGetPaginatedCryptoCurrenciesFluentService WithSymbol(string symbol);
    }
}