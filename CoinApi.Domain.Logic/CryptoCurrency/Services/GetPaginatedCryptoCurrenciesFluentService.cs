using System.Linq;
using AutoMapper;
using CoinApi.DataAccess;
using CoinApi.Domain.CryptoCurrency.Interfaces;
using CoinApi.Domain.CryptoCurrency.Models;
using CoinApi.Domain.Logic.Common.Services;
using CoinApi.Domain.Models.DomainModels;

namespace CoinApi.Domain.Logic.CryptoCurrency.Services
{
    /// <summary>
    ///     Get paginated list of crypto rates
    ///     Fluent builder service be used
    /// </summary>
    public class GetPaginatedCryptoCurrenciesFluentService :
        FluentGetPaginatedServiceBase<CryptoCurrencyResult, CryptoCurrencyDomainModel>,
        IGetPaginatedCryptoCurrenciesFluentService
    {
        private readonly DataContext _dataContext;

        private string _symbol;

        public GetPaginatedCryptoCurrenciesFluentService(IMapper mapper, DataContext dataContext) : base(mapper)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        ///     Specify the symbol to be used to get the results
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <returns></returns>
        public IGetPaginatedCryptoCurrenciesFluentService WithSymbol(string symbol)
        {
            _symbol = symbol;

            return this;
        }

        protected override IQueryable<CryptoCurrencyDomainModel> GetData()
        {
            var data = _dataContext.CryptoCurrencies
                .AsQueryable();

            if (_symbol != null)
                data = data.Where(w => w.Symbol == _symbol);

            return data;
        }

        protected override string DefaultSortKey => "Symbol";
    }
}