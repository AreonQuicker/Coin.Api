using System.Net.Http;

namespace CoinApi.Integration.Core.Interfaces
{
    /// <summary>
    /// HttpClient Service
    /// </summary>
    public interface ICoinClientService
    {
        HttpClient HttpClient { get; }
    }
}