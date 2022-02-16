using System.Net;
using System.Threading.Tasks;
using CoinApi.Application.Core.Currency.Queries;
using CoinApi.Domain.Common.Models;
using CoinApi.Domain.Currency.Models;
using CoinApi.Filters;
using CoinApi.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CoinApi.Controllers
{
    public class CurrencyController : ApiControllerBase
    {
        /// <summary>
        /// Get single currency by symbol
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <returns>Single currency</returns>
        [HttpGet("Symbol/{symbol}")]
        [ProducesResponseType(typeof(ApiResult<CurrencyResult>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResult<CurrencyResult>), (int) HttpStatusCode.BadRequest)]
        [SwaggerOperation(Tags = new[] {"Currency"}, OperationId = "GetCurrencyBySymbol",
            Description = "GetCurrencyBySymbol")]
        [TypeFilter(typeof(ApiResultFilterAttribute<CurrencyResult>))]
        public async Task<CurrencyResult> GetCurrencyBySymbol(string symbol)
        {
            var result = await Mediator.Send(new GetCurrencyBySymbolQuery(symbol));

            return result;
        }

        /// <summary>
        /// Get paginated list of currencies
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns>Paginated list of currencies</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<PaginatedList<CurrencyResult>>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResult<PaginatedList<CurrencyResult>>), (int) HttpStatusCode.BadRequest)]
        [SwaggerOperation(Tags = new[] {"Currency"}, OperationId = "GetCurrencies",
            Description = "GetCurrencies")]
        [TypeFilter(typeof(ApiResultFilterAttribute<PaginatedList<CurrencyResult>>))]
        public async Task<PaginatedList<CurrencyResult>> GetCurrencies([FromQuery] string symbol = null,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
        {
            var result = await Mediator.Send(new GetCurrenciesQuery(pageNumber, pageSize)
            {
                Symbol = symbol
            });

            return result;
        }
    }
}