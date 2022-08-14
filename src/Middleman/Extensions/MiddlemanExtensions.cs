using System.Reflection;

namespace Middleman.Extensions;

/// <summary>
/// Extension methods for sending messages to <see cref="IMiddleman"/>.
/// </summary>
public static class MiddlemanExtensions
{
    /// <summary>
    /// Asynchronously sends a message to an appropriate message handler.
    /// </summary>
    /// <typeparam name="TResult">Type of a result.</typeparam>
    /// <param name="middleman">The instance of <see cref="IMiddleman"/>.</param>
    /// <param name="query">A query instance.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the instance of <see cref="TResult"/>.</returns>
    public static Task<TResult> QueryAsync<TResult>(this IMiddleman middleman,
        IQuery<TResult> query, CancellationToken cancellationToken = default)
        where TResult : class
    {
        Type messageType = query.GetType();
        Type middlemanType = middleman.GetType();
        Type resultType = typeof(TResult);

        MethodInfo sendAsyncMethodInfo = middlemanType.GetTypeInfo().GetMethod(nameof(IQueryBus.QueryAsync))!;
        MethodInfo sendAsyncMethodGeneric = sendAsyncMethodInfo.MakeGenericMethod(messageType, resultType);

        Task<TResult> result = (Task<TResult>) sendAsyncMethodGeneric
            .Invoke(middleman, new object[] { query, default(CancellationToken) })!;

        return result;
    }

    /// <summary>
    /// Asynchronously sends a query to an appropriate message handler.
    /// </summary>
    /// <typeparam name="TResult">Type of a result.</typeparam>
    /// <typeparam name="TQuery">Type of a message.</typeparam>
    /// <param name="middleman">The instance of <see cref="IMiddleman"/>.</param>
    /// <param name="queryAction">An action for creating the instance of a query.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation, containing the instance of <see cref="TResult"/>.</returns>
    public static Task<TResult> SendAsync<TQuery, TResult>(this IMiddleman middleman, Action<TQuery> queryAction, CancellationToken cancellationToken = default)
        where TQuery : class, IQuery<TResult>, new()
        where TResult : class
    {
        if (queryAction == null)
        {
            throw new ArgumentNullException(nameof(queryAction));
        }

        var message = new TQuery();
        queryAction(message);

        return middleman.QueryAsync<TQuery, TResult>(message, cancellationToken);
    }

    /// <summary>
    /// Asynchronously sends an event to corresponding event handlers.
    /// </summary>
    /// <param name="eventBus">The instance of <see cref="IEventBus"/>.</param>
    /// <param name="event">An instance of an <see cref="IEvent"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    public static Task NotifyAsync(this IEventBus eventBus, object @event, CancellationToken cancellationToken = default)
    {
        Type messageType = @event.GetType();
        Type middlemanType = eventBus.GetType();

        MethodInfo sendAsyncMethodInfo = middlemanType.GetTypeInfo().GetMethod("NotifyAsync")!;
        MethodInfo sendAsyncMethodGeneric = sendAsyncMethodInfo.MakeGenericMethod(messageType);
        Task result = (Task)sendAsyncMethodGeneric.Invoke(eventBus, new object[] { @event, cancellationToken })!;

        return result;
    }
}
