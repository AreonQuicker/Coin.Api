using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoinApi.Domain.Common.Models
{
    /// <summary>
    /// Paginated list object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginatedList<T>
    {
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            TotalCount = count;
            Items = items;
        }

        public List<T> Items { get; }
        public int PageIndex { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        /// <summary>
        /// Create a new paginated list object from IQueryable source
        /// </summary>
        /// <param name="source">IQueryable source</param>
        /// <param name="pageIndex">Page number</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns></returns>
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex = 1,
            int? pageSize = null)
        {
            var count = await source.CountAsync();
            if (!pageSize.HasValue)
                pageSize = count;

            var items = await source.Skip((pageIndex - 1) * pageSize.Value).Take(pageSize.Value).ToListAsync();

            return new PaginatedList<T>(items, count, pageIndex, pageSize.Value);
        }
    }
}