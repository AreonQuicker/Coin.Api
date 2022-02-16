using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoinApi.DataAccess;
using CoinApi.Domain.Common.Constants.ErrorCodes;
using CoinApi.Domain.Common.Exceptions;
using CoinApi.Domain.CryptoCurrency.Interfaces;
using CoinApi.Domain.CryptoCurrency.Models;
using CoinApi.Domain.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace CoinApi.Domain.Logic.CryptoCurrency.Services
{
    /// <summary>
    /// Crypto rate CRUD service
    /// </summary>
    public class CryptoCurrencyService : ICryptoCurrencyService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public CryptoCurrencyService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Get crypto rate by symbol
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <returns></returns>

        public async Task<CryptoCurrencyResult> GetBySymbolAsync(string symbol)
        {
            var entry = await _dataContext
                .CryptoCurrencies.Include(i => i.Quotes)
                .FirstOrDefaultAsync(f => f.Symbol == symbol);

            return _mapper.Map<CryptoCurrencyResult>(entry);
        }

        /// <summary>
        /// Delete crypto rate by symbol
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <returns></returns>
        public async Task DeleteBySymbolAsync(string symbol)
        {
            var cryptoCurrency = await _dataContext
                .CryptoCurrencies.FirstOrDefaultAsync(f => f.Symbol == symbol);

            if (cryptoCurrency is null)
                throw new NotFoundException(nameof(cryptoCurrency), symbol,
                    ServiceErrorCodesConstant.CryptoCurrencyNotFound);

            _dataContext.CryptoCurrencies.Remove(cryptoCurrency);

            await _dataContext.SaveChangesAsync();
        }

        /// <summary>
        /// Create or update crypto rate
        /// </summary>
        /// <param name="cryptoCurrencyUpsertRequests">Upsert request</param>
        /// <returns></returns>
        public async Task UpsertAsync(params CryptoCurrencyUpsertRequest[] cryptoCurrencyUpsertRequests)
        {
            //Loop through request and create or update
            foreach (var cryptoCurrencyUpsertRequest in cryptoCurrencyUpsertRequests)
            {
                //Check if the coin exists first
                var coin = await _dataContext
                    .Coins.AsNoTracking().FirstOrDefaultAsync(f => f.Symbol == cryptoCurrencyUpsertRequest.Symbol);

                if (coin is null)
                    throw new NotFoundException(nameof(coin), cryptoCurrencyUpsertRequest.Symbol,
                        ServiceErrorCodesConstant.CoinNotFound);
                
                foreach (var quote in cryptoCurrencyUpsertRequest.Quotes)
                {
                    //Check if the currency exists
                    var currency = await _dataContext.Currencies
                        .AsNoTracking()
                        .FirstOrDefaultAsync(f => f.Symbol == quote.Symbol);

                    if (currency is null)
                        throw new NotFoundException(nameof(currency), quote.Symbol,
                            ServiceErrorCodesConstant.CurrencyNotFound);
                }
                
                //Get crypto currency by symbol and create or update
                var entry = _dataContext.CryptoCurrencies
                    .Include(i => i.Quotes)
                    .FirstOrDefault(f => f.Symbol == cryptoCurrencyUpsertRequest.Symbol);
                
                if (entry != null)
                {
                    foreach (var entryQuote in entry.Quotes)
                    {
                        entry.Quotes.Remove(entryQuote);
                    }
                    
                    // _dataContext.CryptoCurrencyQuotes.RemoveRange(entry.Quotes);
                    //
                    // await _dataContext.SaveChangesAsync();

                    _mapper.Map(cryptoCurrencyUpsertRequest, entry);
                    _dataContext.Entry(entry).State = EntityState.Modified;

                    // await _dataContext.SaveChangesAsync();

                    return;
                }

                entry = _mapper.Map<CryptoCurrencyDomainModel>(cryptoCurrencyUpsertRequest);
                await _dataContext.AddAsync(entry);
            }
            
            await _dataContext.SaveChangesAsync();
        }
    }
}