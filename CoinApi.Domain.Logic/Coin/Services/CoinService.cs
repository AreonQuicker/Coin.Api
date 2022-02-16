using System.Threading.Tasks;
using AutoMapper;
using CoinApi.DataAccess;
using CoinApi.Domain.Coin.Interfaces;
using CoinApi.Domain.Coin.Models;
using Microsoft.EntityFrameworkCore;

namespace CoinApi.Domain.Logic.Coin.Services
{
    /// <summary>
    /// Coin CRUD service
    /// </summary>
    public class CoinService : ICoinService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public CoinService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Get coin by symbol
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <returns>Coin result</returns>
        public async Task<CoinResult> GetBySymbolAsync(string symbol)
        {
            var coin = await _dataContext.Coins.FirstOrDefaultAsync(f => f.Symbol == symbol);

            return _mapper.Map<CoinResult>(coin);
        }
    }
}