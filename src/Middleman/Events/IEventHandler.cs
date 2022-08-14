namespace Middleman.Events;

/// <summary>
/// Represents an interface for event handlers.
/// </summary>
/// <typeparam name="TEvent">Type of the event.</typeparam>
public interface IEventHandler<in TEvent> where TEvent : IEvent
{
    /// <summary>
    /// Asynchronously handles the specified event.
    /// </summary>
    /// <param name="event">The instance of the <see href="IEvent"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}
