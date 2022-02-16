using System.Linq;
using System.Threading.Tasks;
using CoinApi.Domain.Common.Models;

namespace CoinApi.Domain.Common.Extensions
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<TDestination>> ToPaginatedListAsync<TDestination>(
            this IQueryable<TDestination> queryable, int pageNumber = 1, int? pageSize = null)
        {
            return PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);
        }
    }
}