using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoinApi.DataAccess;
using CoinApi.Domain.Common.Interfaces;
using CoinApi.Domain.Currency.Interfaces;
using CoinApi.Domain.Currency.Models;
using CoinApi.Domain.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CoinApi.Domain.Logic.Currency.Services
{
    /// <summary>
    /// Delete and Add currencies
    /// Fluent Builder service will be used
    /// </summary>
    public class DeleteAndAddCurrencyFluentService : IDeleteAndAddCurrencyFluentService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        private List<CurrencyDeleteAndAddRequest> _request;

        public DeleteAndAddCurrencyFluentService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public IDbContextTransaction Transaction { get; private set; }

        #region IDeleteAndAddCurrencyFluentService

        /// <summary>
        /// Specify the request for performing the delete and add action
        /// </summary>
        /// <param name="CurrencyDeleteAndAddRequest">Delete and Add Request</param>
        /// <returns></returns>
        public IDeleteAndAddCurrencyFluentService WithRequest(List<CurrencyDeleteAndAddRequest> request)
        {
            _request = request;

            return this;
        }


        /// <summary>
        /// Specify if a sql transaction needs to be used
        /// </summary>
        /// <param name="isolationLevel">Isolation Level</param>
        /// <returns></returns>
        public IFluentActionService WithTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            Transaction = _dataContext.Database.BeginTransaction(isolationLevel);

            return this;
        }

        /// <summary>
        /// Execute action
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteAsync()
        {
            if (_request is null)
                throw new ArgumentNullException(nameof(_request));

            var symbols = _request.Select(s => s.Symbol).Distinct().ToList();

            var entriesToBeRemoved = await _dataContext.Currencies.Where(w => symbols.Contains(w.Symbol)).ToListAsync();

            _dataContext.Currencies.RemoveRange(entriesToBeRemoved);

            await _dataContext.SaveChangesAsync();

            var newEntries = _mapper.Map<List<CurrencyDomainModel>>(_request);

            await _dataContext.Currencies.AddRangeAsync(newEntries);

            await _dataContext.SaveChangesAsync();

            if (Transaction != null)
                await Transaction.CommitAsync();
        }

        #endregion
    }
}