using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoinApi.Domain.Common.Enums;
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
        private SortOrderTypeEnum _sortOrderType = SortOrderTypeEnum.ASC;
        private string _sortOrderKey;

        protected FluentGetPaginatedServiceBase(IMapper mapper)
        {
            Mapper = mapper;
        }

        /// <summary>
        /// Specify the paging info for paginated results
        /// </summary>
        /// <param name="pageInfo">Paging info</param>
        /// <returns></returns>
        public IFluentGetPaginatedService<T> WithPaginatedInfo((int? PageNumber, int? PageSize) pageInfo)
        {
            _pageNumber = pageInfo.PageNumber ?? 1;
            _pageSize = pageInfo.PageSize;

            return this;
        }
        
        /// <summary>
        /// Specify the sort criteria
        /// </summary>  
        /// <param name="SortInfo">Sort Info</param>
        /// <returns></returns>
        public IFluentGetPaginatedService<T> WithSortInfo(SortInfo sortInfo)
        {
            _sortOrderKey = sortInfo.SoryKey;
            _sortOrderType = sortInfo.SortOrderType;

            return this;
        }

        /// <summary>
        /// Specify the sort criteria
        /// </summary>  
        /// <param name="SortInfo">Sort Info</param>
        /// <returns></returns>
        public async Task<PaginatedList<T>> GetAsync()
        {
            var data = GetData();

            if (!string.IsNullOrWhiteSpace(_sortOrderKey))
            {
                if (_sortOrderType == SortOrderTypeEnum.DESC)
                    data = data.OrderBy(_sortOrderKey + " DESC");
                else
                    data = data.OrderBy(_sortOrderKey);
            }
            else
            {
                data = data.OrderBy(DefaultSortKey);
            }


            return await data
                .ProjectTo<T>(Mapper.ConfigurationProvider)
                .ToPaginatedListAsync(_pageNumber, _pageSize);
        }

        protected abstract IQueryable<D> GetData();
        
        protected abstract string DefaultSortKey { get; }
    }
}