using System.Linq;
using AutoMapper;
using CoinApi.DataAccess;
using CoinApi.Domain.Coin.Interfaces;
using CoinApi.Domain.Coin.Models;
using CoinApi.Domain.Logic.Common.Services;
using CoinApi.Domain.Models.DomainModels;

namespace CoinApi.Domain.Logic.Coin.Services
{
    /// <summary>
    ///     Get paginated list of coins
    ///     Fluent builder service be used
    /// </summary>
    public class GetPaginatedCoinsFluentService : FluentGetPaginatedServiceBase<CoinResult, CoinDomainModel>,
        IGetPaginatedCoinsFluentService
    {
        private readonly DataContext _dataContext;

        private int? _rank;
        private string _symbol;

        public GetPaginatedCoinsFluentService(IMapper mapper, DataContext dataContext) : base(mapper)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        ///     Specify the rank to be used to get the results
        /// </summary>
        /// <param name="rank">Rank</param>
        /// <returns></returns>
        public IGetPaginatedCoinsFluentService WithRank(int? rank)
        {
            _rank = rank;

            return this;
        }

        /// <summary>
        ///     Specify the symbol to be used to get the results
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <returns></returns>
        public IGetPaginatedCoinsFluentService WithSymbol(string symbol)
        {
            _symbol = symbol;

            return this;
        }

        protected override IQueryable<CoinDomainModel> GetData()
        {
            var data = _dataContext.Coins
                .AsQueryable();

            if (_rank.HasValue)
                data = data.Where(w => w.Rank == _rank.Value);

            if (_symbol != null)
                data = data.Where(w => w.Symbol == _symbol);
            
            return data;
        }

        protected override string DefaultSortKey => "Symbol";
    }
}