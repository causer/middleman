namespace Middleman.Queries;

/// <summary>
/// A handler for processing incoming queries.
/// </summary>
/// <typeparam name="TQuery">Type of a query.</typeparam>
/// <typeparam name="TResult">Type of a result.</typeparam>
public interface IQueryHandler<in TQuery, TResult>
    where TQuery : class, IQuery<TResult>
    where TResult : class
{
    /// <summary>
    /// Asynchronously handles the specified query.
    /// </summary>
    /// <param name="query">The instance of the <see href="IQuery"/>.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the instance of <see cref="TResult"/>.</returns>
    Task<TResult> HandleAsync(TQuery query);
}
