using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoinApi.DataAccess;
using CoinApi.Domain.Coin.Interfaces;
using CoinApi.Domain.Coin.Models;
using CoinApi.Domain.Common.Interfaces;
using CoinApi.Domain.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CoinApi.Domain.Logic.Coin.Services
{
    /// <summary>
    /// Delete and Add coins
    /// Fluent Builder service will be used
    /// </summary>
    public class DeleteAndAddCoinFluentService : IDeleteAndAddCoinFluentService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        private List<CoinCreateRequest> _coinCreateRequests;

        public DeleteAndAddCoinFluentService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public IDbContextTransaction Transaction { get; private set; }

        #region IDeleteAndAddCoinFluentService
        
        /// <summary>
        /// Specify the request for performing the delete and add action
        /// </summary>
        /// <param name="coinCreateRequests">Delete and Add request</param>
        /// <returns></returns>
        public IDeleteAndAddCoinFluentService WithRequest(List<CoinCreateRequest> coinCreateRequests)
        {
            _coinCreateRequests = coinCreateRequests;

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
            if (_coinCreateRequests is null)
                throw new ArgumentNullException(nameof(_coinCreateRequests));

            var symbols = _coinCreateRequests.Select(s => s.Symbol).Distinct().ToList();

            var coins = await _dataContext.Coins.Where(w => symbols.Contains(w.Symbol)).ToListAsync();

            _dataContext.Coins.RemoveRange(coins);

            await _dataContext.SaveChangesAsync();

            var newCoins = _mapper.Map<List<CoinDomainModel>>(_coinCreateRequests);

            await _dataContext.Coins.AddRangeAsync(newCoins);

            await _dataContext.SaveChangesAsync();

            if (Transaction != null)
                await Transaction.CommitAsync();
        }

        #endregion
    }
}