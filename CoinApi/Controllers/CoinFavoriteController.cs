using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CoinApi.Application.Core.CoinFavorite.Commands;
using CoinApi.Application.Core.CoinFavorite.Queries;
using CoinApi.Domain.CoinFavorite.Models;
using CoinApi.Filters;
using CoinApi.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CoinApi.Controllers
{
    public class CoinFavoriteController : ApiControllerBase
    {
        /// <summary>
        /// Get single coin favorite
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <returns>Single coin favorite</returns>
        [HttpGet("Symbol/{symbol}")]
        [ProducesResponseType(typeof(ApiResult<CoinFavoriteResult>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResult<CoinFavoriteResult>), (int) HttpStatusCode.BadRequest)]
        [SwaggerOperation(Tags = new[] {"CoinFavorite"}, OperationId = "GetCoinFavoriteBySymbol",
            Description = "GetCoinFavoriteBySymbol")]
        [TypeFilter(typeof(ApiResultFilterAttribute<CoinFavoriteResult>))]
        public async Task<CoinFavoriteResult> GetCoinFavoriteBySymbol(string symbol)
        {
            var result = await Mediator.Send(new GetCoinFavoriteBySymbolQuery(symbol));

            return result;
        }

        /// <summary>
        /// Get list of coin favorites
        /// </summary>
        /// <returns>List of coin favorites</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<IList<CoinFavoriteResult>>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResult<IList<CoinFavoriteResult>>), (int) HttpStatusCode.BadRequest)]
        [SwaggerOperation(Tags = new[] {"CoinFavorite"}, OperationId = "GetCoinFavorites",
            Description = "GetCoinFavorites")]
        [TypeFilter(typeof(ApiResultFilterAttribute<IList<CoinFavoriteResult>>))]
        public async Task<IList<CoinFavoriteResult>> GetCoinFavorites()
        {
            var result = await Mediator.Send(new GetCoinFavoritesQuery());

            return result;
        }

        /// <summary>
        /// Create favorite coin
        /// </summary>
        /// <param name="request">Create request</param>
        /// <returns>Created Id</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResult<int>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResult<int>), (int) HttpStatusCode.BadRequest)]
        [SwaggerOperation(Tags = new[] {"CoinFavorite"}, OperationId = "CreateCoinFavorite",
            Description = "CreateCoinFavorite")]
        [TypeFilter(typeof(ApiResultFilterAttribute<int>))]
        public async Task<int> CreateCoinFavorite([FromBody] CreateCoinFavoriteCommand request)
        {
            return await Mediator.Send(request);
        }

        /// <summary>
        /// Update coin currencies by symbol
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="request">Update request</param>
        /// <returns>Updated Id</returns>
        [HttpPatch("Symbol/{symbol}")]
        [ProducesResponseType(typeof(ApiResult<int>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResult<int>), (int) HttpStatusCode.BadRequest)]
        [SwaggerOperation(Tags = new[] {"CoinFavorite"}, OperationId = "UpdateCoinFavorite",
            Description = "UpdateCoinFavorite")]
        [TypeFilter(typeof(ApiResultFilterAttribute<int>))]
        public async Task<int> UpdateCoinFavorite(string symbol,
            [FromBody] CoinFavoriteUpdateRequest request)
        {
            var command = Mapper.Map<UpdateCoinFavoriteCommand>(request);
            command.Symbol = symbol;

            return await Mediator.Send(command);
        }

        /// <summary>
        /// Delete coin favorite by symbol
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <returns>Deleted Id</returns>
        [HttpDelete("Symbol/{symbol}")]
        [ProducesResponseType(typeof(ApiResult<int>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResult<int>), (int) HttpStatusCode.BadRequest)]
        [SwaggerOperation(Tags = new[] {"CoinFavorite"}, OperationId = "DeleteCoinFavoriteBySymbol",
            Description = "DeleteCoinFavoriteBySymbol")]
        [TypeFilter(typeof(ApiResultFilterAttribute<int>))]
        public async Task<int> DeleteCoinFavoriteBySymbol(string symbol)
        {
            return await Mediator.Send(new DeleteCoinFavoriteBySymbolCommand(symbol));
        }
    }
}