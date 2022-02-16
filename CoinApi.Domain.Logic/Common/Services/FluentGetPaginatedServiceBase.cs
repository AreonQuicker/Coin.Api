using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoinApi.Domain.Common.Extensions;
using CoinApi.Domain.Common.Interfaces;
using CoinApi.Domain.Common.Models;
using CoinApi.Domain.Models;

namespace CoinApi.Domain.Logic.Common.Services
{
    public abstract class FluentGetPaginatedServiceBase<T, D> : IFluentGetPaginatedService<T> where D : AuditDomainModel
    {
        protected readonly IMapper Mapper;

        private int _pageNumber = 1;
        private int? _pageSize;

        protected FluentGetPaginatedServiceBase(IMapper mapper)
        {
            Mapper = mapper;
        }

        public IFluentGetPaginatedService<T> WithPaginatedInfo((int? PageNumber, int? PageSize) pageInfo)
        {
            _pageNumber = pageInfo.PageNumber ?? 1;
            _pageSize = pageInfo.PageSize;

            return this;
        }

        public async Task<PaginatedList<T>> GetAsync()
        {
            var data = GetData();

            return await data
                .ProjectTo<T>(Mapper.ConfigurationProvider)
                .ToPaginatedListAsync(_pageNumber, _pageSize);
        }

        protected abstract IQueryable<D> GetData();
    }
}