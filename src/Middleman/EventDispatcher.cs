using System.Collections.Concurrent;

namespace Middleman;

/// <summary>
/// Asynchronously dispatches an event to corresponding event handler. 
/// </summary>
/// <typeparam name="TEvent">Type of the event.</typeparam>
public class EventDispatcher<TEvent> : IEventDispatcher where TEvent : class, IEvent
{
    private readonly IEnumerable<IEventHandler<TEvent>> _handlers;
    private readonly ConcurrentDictionary<Type, int> _handlerOrderMap = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="EventDispatcher{TEvent}"/>.
    /// </summary>
    /// <param name="handlers">A collection of <see cref="IEventHandler{TEvent}"/> handlers.</param>
    public EventDispatcher(IEnumerable<IEventHandler<TEvent>> handlers)
    {
        _handlers = handlers;
    }

    /// <inheritdoc />
    public async Task DispatchAsync(IEvent @event, CancellationToken cancellationToken = default)
    {
        var sortedHandlers = _handlers.OrderBy(GetHandlerOrder);

        foreach (var eventHandler in sortedHandlers)
        {
            await eventHandler.HandleAsync((TEvent)@event, cancellationToken);
        }
    }

    /// <summary>
    /// Tries to get order values of the <see cref="HandlerOrderAttribute"/> of the event handler.
    /// </summary>
    /// <param name="handler">An instance of the <inheritdoc cref="IEventHandler{TEvent}"/> handler.</param>
    /// <returns>Order value of the attribute if exists, otherwise <see cref="HandlerOrderAttribute.DefaultHandlerOrder"/> value.</returns>
    private int GetHandlerOrder(IEventHandler<TEvent> handler)
    {
        var handlerType = handler.GetType();
        return _handlerOrderMap.GetOrAdd(handlerType, ht =>
        {
            var orderAttributes = handlerType
                .GetCustomAttributes(typeof(HandlerOrderAttribute), false)
                .Cast<HandlerOrderAttribute>()
                .ToArray();

            if (!orderAttributes.Any())
            {
                return HandlerOrderAttribute.DefaultHandlerOrder;
            }

            return orderAttributes[0].Order;
        });
    }
}