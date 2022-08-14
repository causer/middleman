namespace Middleman.Events;

/// <summary>
/// Event dispatcher.
/// </summary>
public interface IEventDispatcher
{
    /// <summary>
    /// Dispatches events to appropriate event handlers.
    /// </summary>
    /// <param name="event">An instance of an <see cref="IEvent"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task DispatchAsync(IEvent @event, CancellationToken cancellationToken = default);
}