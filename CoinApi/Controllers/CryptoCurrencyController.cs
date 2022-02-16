using System.Net;
using System.Threading.Tasks;
using CoinApi.Application.Core.CryptoCurrency.Queries;
using CoinApi.Domain.Common.Models;
using CoinApi.Domain.CryptoCurrency.Models;
using CoinApi.Filters;
using CoinApi.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CoinApi.Controllers
{
    public class CryptoCurrencyController : ApiControllerBase
    {
        /// <summary>
        /// Get paginated list of crypto rates
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Paginated list of crypto rates</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<PaginatedList<CryptoCurrencyResult>>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResult<PaginatedList<CryptoCurrencyResult>>),
            (int) HttpStatusCode.BadRequest)]
        [SwaggerOperation(Tags = new[] {"CryptoCurrency"}, OperationId = "GetCryptoCurrencies",
            Description = "GetCryptoCurrencies")]
        [TypeFilter(typeof(ApiResultFilterAttribute<PaginatedList<CryptoCurrencyResult>>))]
        public async Task<PaginatedList<CryptoCurrencyResult>> GetCryptoCurrencies(
            [FromQuery] string symbol = null, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
        {
            var result = await Mediator.Send(new GetCryptoCurrenciesQuery(pageNumber, pageSize)
            {
                Symbol = symbol
            });

            return result;
        }

        /// <summary>
        /// Get single crypto rate
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <returns>Single crypto rate</returns>
        [HttpGet("Symbol/{symbol}")]
        [ProducesResponseType(typeof(ApiResult<CryptoCurrencyResult>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResult<CryptoCurrencyResult>),
            (int) HttpStatusCode.BadRequest)]
        [SwaggerOperation(Tags = new[] {"CryptoCurrency"}, OperationId = "GetCryptoCurrencyBySymbol",
            Description = "GetCryptoCurrencyBySymbol")]
        [TypeFilter(typeof(ApiResultFilterAttribute<CryptoCurrencyResult>))]
        public async Task<CryptoCurrencyResult> GetCryptoCurrencyBySymbol(string symbol)
        {
            var result = await Mediator.Send(new GetCryptoCurrencyBySymbolQuery(symbol));

            return result;
        }
    }
}