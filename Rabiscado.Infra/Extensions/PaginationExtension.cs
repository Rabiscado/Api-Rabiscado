using Microsoft.EntityFrameworkCore;
using Rabiscado.Domain.Pagination;

namespace Rabiscado.Infra.Extensions;

public static class PaginationExtension
{
    public static async Task<ResultPaginated<T>> SearchPaginatedAsync<T>(this IQueryable<T> query, int page, int perPage) where T : class
    {
        var result = new ResultPaginated<T>(page, perPage, await query.CountAsync());

        var pageQuantity = (double)result.Pagination.Total / perPage;
        result.Pagination.TotalPages = (int)Math.Ceiling(pageQuantity);

        var skip = (page - 1) * perPage;
        result.Itens = await query.Skip(skip).Take(perPage).ToListAsync();
        result.Pagination.TotalInPage = result.Itens.Count;
        return result;
    }
}
