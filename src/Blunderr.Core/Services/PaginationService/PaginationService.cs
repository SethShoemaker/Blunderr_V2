using Microsoft.EntityFrameworkCore;

namespace Blunderr.Core.Services.PaginationService
{
    public static class PaginationService
    {
        public static async Task<Page<T>> PaginateAsync<T>(this IQueryable<T> items, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            Page<T> p = new();

            p.TotalCount = await items.CountAsync();

            int itemsSkipped = (pageNumber - 1) * pageSize;
            if (itemsSkipped >= p.TotalCount)
                p.Error = Error.PageNumberOutOfRange;

            p.Items = await items
                .Skip(itemsSkipped)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            p.StartPosition = itemsSkipped + 1;
            p.EndPosition = itemsSkipped + p.Items.Count;
            p.HasPrevPage = p.StartPosition > 1;
            p.HasNextPage = p.EndPosition < p.TotalCount;
            p.MaxPageNumber = (int)Math.Ceiling((double)p.TotalCount / pageSize);

            return p;
        }
    }
}