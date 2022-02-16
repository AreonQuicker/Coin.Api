using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoinApi.DataAccess;
using CoinApi.Domain.CoinFavorite.Interfaces;
using CoinApi.Domain.CoinFavorite.Models;
using CoinApi.Domain.Common.Constants.ErrorCodes;
using CoinApi.Domain.Common.Exceptions;
using CoinApi.Domain.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace CoinApi.Domain.Logic.CoinFavorite.Services
{
    /// <summary>
    /// Coind favorite CRUD service
    /// </summary>
    public class CoinFavoriteService : ICoinFavoriteService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public CoinFavoriteService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all coin favorites
        /// </summary>
        /// <returns></returns>
        public async Task<IList<CoinFavoriteResult>> GetAsync()
        {
            var entries = await _dataContext
                .CoinFavorites
                .Include(i => i.FavoriteCurrencies)
                .Where(w => w.FavoriteCurrencies.Any())
                .OrderBy(a => a.Symbol)
                .ToListAsync();

            var result = _mapper.Map<IList<CoinFavoriteResult>>(entries);

            return result;
        }

        /// <summary>
        /// Get coin favorite by symbol
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <returns>Coin favorite</returns>
        public async Task<CoinFavoriteResult> GetBySymbolAsync(string symbol)
        {
            var entries = await _dataContext
                .CoinFavorites
                .Include(i => i.FavoriteCurrencies)
                .Where(w => w.FavoriteCurrencies.Any())
                .Where(w => w.Symbol == symbol)
                .FirstOrDefaultAsync();

            var result = _mapper.Map<CoinFavoriteResult>(entries);

            return result;
        }

        /// <summary>
        /// Create a new coin favorite
        /// </summary>
        /// <param name="coinFavoriteCreateRequest">Create request</param>
        /// <returns>Created Id</returns>
        public async Task<int> CreateAsync(CoinFavoriteCreateRequest coinFavoriteCreateRequest)
        {
            var coinFavorite = await _dataContext
                .CoinFavorites
                .Where(w => w.Symbol == coinFavoriteCreateRequest.Symbol)
                .FirstOrDefaultAsync();

            if (coinFavorite is not null)
                throw new AlreadyExistException(nameof(coinFavorite), coinFavoriteCreateRequest.Symbol,
                    ServiceErrorCodesConstant.CoinFavoriteAlreadyExist);

            if (coinFavoriteCreateRequest.FavoriteCurrencies is null ||
                !coinFavoriteCreateRequest.FavoriteCurrencies.Any())
                throw new ServiceException("Can not create a coin favorite without currencies",
                    ServiceErrorCodesConstant.NotSpecified);

            var coin = await _dataContext.Coins.FirstOrDefaultAsync(f => f.Symbol == coinFavoriteCreateRequest.Symbol);

            if (coin is null)
                throw new ServiceException($"Coin {coinFavoriteCreateRequest.Symbol} does not exists",
                    ServiceErrorCodesConstant.CoinNotFound);

            foreach (var favoriteCurrency in coinFavoriteCreateRequest.FavoriteCurrencies)
            {
                var currency =
                    await _dataContext.Currencies.FirstOrDefaultAsync(f => f.Symbol == favoriteCurrency.Symbol);

                if (currency is null)
                    throw new ServiceException($"Currency {favoriteCurrency.Symbol} does not exists",
                        ServiceErrorCodesConstant.CurrencyNotFound);
            }

            coinFavorite = _mapper.Map<CoinFavoriteDomainModel>(coinFavoriteCreateRequest);

            await _dataContext.AddAsync(coinFavorite);

            await _dataContext.SaveChangesAsync();

            return coinFavorite.Id;
        }

        /// <summary>
        /// Update coin favorite by symbol
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="coinFavoriteCreateRequest">Update request</param>
        /// <returns>Updated Id</returns>
        public async Task<int> UpdateAsync(string symbol, CoinFavoriteUpdateRequest coinFavoriteUpdateRequest)
        {
            var coinFavorite = await _dataContext
                .CoinFavorites
                .Include(i => i.FavoriteCurrencies)
                .Where(w => w.Symbol == symbol)
                .FirstOrDefaultAsync();

            if (coinFavorite is null)
                throw new NotFoundException(nameof(coinFavorite), symbol,
                    ServiceErrorCodesConstant.CoinFavoriteNotFound);

            if (coinFavoriteUpdateRequest.FavoriteCurrencies is null ||
                !coinFavoriteUpdateRequest.FavoriteCurrencies.Any())
                throw new ServiceException("Can not create a coin favorite without currencies",
                    ServiceErrorCodesConstant.NotSpecified);

            foreach (var favoriteCurrency in coinFavoriteUpdateRequest.FavoriteCurrencies)
            {
                var currency =
                    await _dataContext.Currencies.FirstOrDefaultAsync(f => f.Symbol == favoriteCurrency.Symbol);

                if (currency is null)
                    throw new ServiceException($"Currency {favoriteCurrency.Symbol} does not exists",
                        ServiceErrorCodesConstant.CurrencyNotFound);
            }
            
            foreach (var coinFavoriteFavoriteCurrency in coinFavorite.FavoriteCurrencies)
            {
                coinFavorite.FavoriteCurrencies.Remove(coinFavoriteFavoriteCurrency);
            }

            _mapper.Map(coinFavoriteUpdateRequest, coinFavorite);
            _dataContext.Entry(coinFavorite).State = EntityState.Modified;

            await _dataContext.SaveChangesAsync();

            return coinFavorite.Id;
        }

        /// <summary>
        /// Delete coin favorite by symbol
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <returns>Deleted Id</returns>
        public async Task<int> DeleteBySymbolAsync(string symbol)
        {
            var coinFavorite = await _dataContext
                .CoinFavorites
                .Where(w => w.Symbol == symbol)
                .FirstOrDefaultAsync();

            if (coinFavorite is null)
                throw new NotFoundException(nameof(coinFavorite), symbol,
                    ServiceErrorCodesConstant.CoinFavoriteNotFound);

            _dataContext.CoinFavorites.Remove(coinFavorite);

            await _dataContext.SaveChangesAsync();

            return coinFavorite.Id;
        }
    }
}