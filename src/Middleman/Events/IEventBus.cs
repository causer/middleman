namespace Middleman.Events;

/// <summary>
/// A dispatcher that coordinates events to appropriate handlers.
/// </summary>
public interface IEventBus
{
    /// <summary>
    /// Asynchronously sends an event to appropriate event handlers.
    /// </summary>
    /// <typeparam name="TEvent">Type of an event.</typeparam>
    /// <param name="event">An instance of <see cref="IEvent"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task NotifyAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : class, IEvent;

    /// <summary>
    /// Asynchronously sends an event to appropriate event handlers.
    /// </summary>
    /// <param name="event">An instance of an event derived from the <see cref="IEvent"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task NotifyAsync(object @event, CancellationToken cancellationToken = default);
}
