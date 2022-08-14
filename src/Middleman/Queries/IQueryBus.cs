namespace Middleman.Queries;

/// <summary>
/// A dispatcher that coordinates queries to appropriate handlers.
/// </summary>
public interface IQueryBus
{
    /// <summary>
    /// Asynchronously send a query to an appropriate message handler.
    /// </summary>
    /// <typeparam name="TQuery">Type of a query.</typeparam>
    /// <typeparam name="TResult">Type of a result.</typeparam>
    /// <param name="query">Instance of <see cref="IQuery{TResult}"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the instance of <see cref="TResult"/>.</returns>
    Task<TResult> QueryAsync<TQuery, TResult>(TQuery query,
        CancellationToken cancellationToken = default)
        where TQuery : class, IQuery<TResult>
        where TResult : class;
}
