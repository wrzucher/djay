namespace DjayLanguage.Core;

/// <summary>
/// Extension for <see cref="IQueryable"/>.
/// </summary>
public static class IQueryableExtensions
{
    /// <summary>
    /// Get page with offset.
    /// </summary>
    /// <param name="source">Object for extension.</param>
    /// <param name="offset">Offset for returned collection.</param>
    /// <param name="page">Number of page for returned collection.</param>
    /// <param name="pageSize">Page size for returned collection.</param>
    /// <typeparam name="TSource">Generic type.</typeparam>
    /// <returns>Collection of needed page size from database.</returns>
    public static IQueryable<TSource> Page<TSource>(
        this IQueryable<TSource> source,
        int offset,
        int page,
        int pageSize)
    {
        return source.Skip(offset + ((page - 1) * pageSize)).Take(pageSize);
    }
}