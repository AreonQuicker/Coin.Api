using System.Linq;
using AutoMapper;
using CoinApi.DataAccess;
using CoinApi.Domain.Currency.Interfaces;
using CoinApi.Domain.Currency.Models;
using CoinApi.Domain.Logic.Common.Services;
using CoinApi.Domain.Models.DomainModels;

namespace CoinApi.Domain.Logic.Currency.Services
{
    public class GetPaginatedCurrenciesFluentService :
        FluentGetPaginatedServiceBase<CurrencyResult, CurrencyDomainModel>,
        IGetPaginatedCurrenciesFluentService
    {
        private readonly DataContext _dataContext;

        private string _symbol;

        public GetPaginatedCurrenciesFluentService(IMapper mapper, DataContext dataContext) : base(mapper)
        {
            _dataContext = dataContext;
        }

        public IGetPaginatedCurrenciesFluentService WithSymbol(string symbol)
        {
            _symbol = symbol;

            return this;
        }

        protected override IQueryable<CurrencyDomainModel> GetData()
        {
            var data = _dataContext.Currencies
                .AsQueryable();

            if (_symbol != null)
                data = data.Where(w => w.Symbol == _symbol);

            return data;
        }

        protected override string DefaultSortKey => "Symbol";
    }
}