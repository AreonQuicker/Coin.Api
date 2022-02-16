using System.Threading;

namespace CoinApi.Hangfire.Core.Interfaces
{
    /// <summary>
    /// Hangfire action jobs
    /// </summary>
    public interface ICoinHangfireService
    {
        /// <summary>
        /// Sync coins from client job
        /// </summary>
        /// <param name="token"></param>
        void SyncCoins(CancellationToken token);
        
        /// <summary>
        /// Sync currencies from client job
        /// </summary>
        /// <param name="token"></param>
        void SyncCurrencies(CancellationToken token);
        
        /// <summary>
        /// Sync crypto rates from client job
        /// </summary>
        /// <param name="token"></param>
        void SyncCryptoCurrencies(CancellationToken token);
        
        /// <summary>
        /// Sync crypto currency from client job
        /// </summary>
        /// <param name="key"></param>
        /// <param name="token"></param>
        void SyncCryptoCurrency(string[] keys, CancellationToken token);
    }
}