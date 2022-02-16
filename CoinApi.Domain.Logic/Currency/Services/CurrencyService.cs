using System.Threading.Tasks;
using AutoMapper;
using CoinApi.DataAccess;
using CoinApi.Domain.Currency.Interfaces;
using CoinApi.Domain.Currency.Models;
using Microsoft.EntityFrameworkCore;

namespace CoinApi.Domain.Logic.Currency.Services
{
    /// <summary>
    /// Currency CRUD service
    /// </summary>
    public class CurrencyService : ICurrencyService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public CurrencyService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Get currency by symbol
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <returns>Currency result</returns>
        public async Task<CurrencyResult> GetBySymbolAsync(string symbol)
        {
            var result = await _dataContext.Currencies.FirstOrDefaultAsync(f => f.Symbol == symbol);

            return _mapper.Map<CurrencyResult>(result);
        }
    }
}