using System.Linq.Expressions;

namespace AuthServer.Core.Application.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> Paged<T>(this IQueryable<T> source, int pageNum, int pageSize)
    {
        return source
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize);
    }
    
    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> queryable,
        bool condition,
        Expression<Func<T, bool>> predicate) =>
        condition ? queryable.Where(predicate) : queryable;
}