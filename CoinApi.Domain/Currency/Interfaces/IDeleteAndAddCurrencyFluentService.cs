using System.Collections.Generic;
using CoinApi.Domain.Common.Interfaces;
using CoinApi.Domain.Currency.Models;

namespace CoinApi.Domain.Currency.Interfaces
{
    /// <summary>
    /// Delete and Add currencies
    /// Fluent Builder service will be used
    /// </summary>
    public interface IDeleteAndAddCurrencyFluentService : IFluentActionService
    {
        /// <summary>
        /// Specify the request for performing the delete and add action
        /// </summary>
        /// <param name="CurrencyDeleteAndAddRequest">Delete and Add Request</param>
        /// <returns></returns>
        IDeleteAndAddCurrencyFluentService WithRequest(List<CurrencyDeleteAndAddRequest> request);
    }
}