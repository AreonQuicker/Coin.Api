using System.Net;
using System.Threading.Tasks;
using CoinApi.Application.Core.Coin.Queries;
using CoinApi.Domain.Coin.Models;
using CoinApi.Domain.Common.Enums;
using CoinApi.Domain.Common.Models;
using CoinApi.Filters;
using CoinApi.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CoinApi.Controllers
{
    public class CoinController : ApiControllerBase
    {
        /// <summary>
        /// Get single coin by symbol
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <returns>Single coin</returns>
        [HttpGet("Symbol/{symbol}")]
        [ProducesResponseType(typeof(ApiResult<CoinResult>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResult<CoinResult>), (int) HttpStatusCode.BadRequest)]
        [SwaggerOperation(Tags = new[] {"Coin"}, OperationId = "GetCoinBySymbol",
            Description = "GetCoinBySymbol")]
        [TypeFilter(typeof(ApiResultFilterAttribute<CoinResult>))]
        public async Task<CoinResult> GetCoinBySymbol(string symbol)
        {
            var result = await Mediator.Send(new GetCoinBySymbolQuery(symbol));

            return result;
        }

        /// <summary>
        /// Get paginated list of coins
        /// </summary>
        /// <param name="rank">Rank</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sortKey">Sort Key (Any available key)</param>
        /// <param name="sortType">Sort Type (0 = ASC, 1 = DESC)</param>
        /// <returns>Paginated list of coins</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<PaginatedList<CoinResult>>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResult<PaginatedList<CoinResult>>), (int) HttpStatusCode.BadRequest)]
        [SwaggerOperation(Tags = new[] {"Coin"}, OperationId = "GetCoins",
            Description = "GetCoins")]
        [TypeFilter(typeof(ApiResultFilterAttribute<PaginatedList<CoinResult>>))]
        public async Task<PaginatedList<CoinResult>> GetCoins([FromQuery] int? rank = null,
            [FromQuery] string symbol = null, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100,
            [FromQuery] string sortKey = null, [FromQuery] SortOrderTypeEnum sortType = SortOrderTypeEnum.ASC)
        {
            var result = await Mediator.Send(new GetCoinsQuery(pageNumber, pageSize, sortKey, sortType)
            {
                Rank = rank,
                Symbol = symbol
            });

            return result;
        }
    }
}