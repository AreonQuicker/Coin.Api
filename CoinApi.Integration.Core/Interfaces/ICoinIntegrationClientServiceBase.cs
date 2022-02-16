using System.Collections.Generic;
using System.Threading.Tasks;
using CoinApi.Integration.Core.Responses;

namespace CoinApi.Integration.Core.Interfaces
{
    /// <summary>
    /// Coin integration client
    /// </summary>
    public interface ICoinIntegrationClientServiceBase
    {
        /// <summary>
        /// Fetch all coins from outside
        /// </summary>
        /// <param name="useCache">Indicate to use cache data</param>
        /// <returns>Integration response of coins</returns>
        Task<IntegrationClientResponseBase<IList<CoinIntegrationClientResponse>>> GetCoinsAsync(bool useCache = false);

        /// <summary>
        /// Fetch all currencies from outside
        /// </summary>
        /// <param name="useCache">Indicate to use cache data</param>
        /// <returns>Integration response of currencies</returns>
        Task<IntegrationClientResponseBase<IList<CurrencyIntegrationClientResponse>>> GetCurrenciesAsync(
            bool useCache = false);

        /// <summary>
        /// Fetch single crypto rate from outside by specifying the symbol and currencies
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="currencies">Currencies</param>
        /// <param name="useCache"></param>
        /// <returns>Integration response of crypto rates</returns>
        Task<IntegrationClientResponseBase<Dictionary<string, CryptoCurrencyIntegrationClientResponse>>>
            GetCryptocurrencyAsync(string symbol,
                List<string> currencies, bool useCache = false);
    }
}