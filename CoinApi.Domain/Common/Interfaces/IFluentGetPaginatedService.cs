using System.Threading.Tasks;
using CoinApi.Domain.Common.Models;

namespace CoinApi.Domain.Common.Interfaces
{
    /// <summary>
    /// Fluent builder service to get paginated results
    /// </summary>
    public interface IFluentGetPaginatedService<T>
    {
        /// <summary>
        /// Specify the paging info for paginated results
        /// </summary>
        /// <param name="pageInfo">Paging info</param>
        /// <returns></returns>
        IFluentGetPaginatedService<T> WithPaginatedInfo((int? PageNumber, int? PageSize) pageInfo);

        /// <summary>
        /// Get results
        /// </summary>
        /// <returns></returns>
        Task<PaginatedList<T>> GetAsync();

        /// <summary>
        /// Specify the sort criteria
        /// </summary>  
        /// <param name="SortInfo">Sort Info</param>
        /// <returns></returns>
        IFluentGetPaginatedService<T> WithSortInfo(SortInfo sortInfo);
    }
}