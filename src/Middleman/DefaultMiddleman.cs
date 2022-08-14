using Microsoft.Extensions.Logging;

namespace Middleman;

/// <summary>
/// Default implementation of the <see cref="IMiddleman"/>.
/// </summary>
public class DefaultMiddleman : IMiddleman
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultMiddleman"/>.
    /// </summary>
    /// <param name="serviceProvider">Instance of the <see cref="IServiceProvider"/> for resolving handlers.</param>
    /// <param name="logger">A logger.</param>
    public DefaultMiddleman(IServiceProvider serviceProvider, ILogger<DefaultMiddleman> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    /// <inheritdoc />
    public Task NotifyAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) 
        where TEvent : class, IEvent
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }

        return NotifyIntAsync(@event, cancellationToken);
    }

    /// <inheritdoc />
    public Task NotifyAsync(object @event, CancellationToken cancellationToken = default) => @event switch
    {
        null => throw new ArgumentNullException(nameof(@event)),
        IEvent e => NotifyIntAsync(e, cancellationToken),
        _ => throw new ArgumentNullException(nameof(@event))
    };

    /// <inheritdoc />
    public async Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) 
        where TCommand : class, ICommand
    {
        _logger.LogDebug($"Command was fired [{command.GetType().Name}]");
        await using var scope = _serviceProvider.CreateAsyncScope();
        var handler = scope.ServiceProvider.GetService<ICommandHandler<TCommand>>();

        if (handler == null)
        {
            throw new InvalidOperationException($"Handler for request type {typeof(TCommand)} is not registered.");
        }

        _logger.LogTrace($"Executing command handler: [{handler.GetType().Name}]");
        await handler.HandleAsync(command, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
        where TQuery : class, IQuery<TResult>
        where TResult : class
    {
        _logger.LogDebug($"Query was fired [{query.GetType().Name}]");
        await using var scope = _serviceProvider.CreateAsyncScope();
        var handler = scope.ServiceProvider.GetService<IQueryHandler<TQuery, TResult>>();
        if (handler == null)
        {
            throw new InvalidOperationException($"Handler for request type {typeof(TQuery)} is not registered.");
        }

        _logger.LogTrace($"Executing query handler: [{handler.GetType().Name}]");
        return await handler.HandleAsync(query);
    }

    private Task NotifyIntAsync(IEvent @event, CancellationToken cancellationToken)
    {
        var eventType = @event.GetType();
        _logger.LogDebug($"Event was fired [{eventType.Name}]");

        var dispatcher = (IEventDispatcher)
            ActivatorUtilities.CreateInstance(_serviceProvider, typeof(EventDispatcher<>).MakeGenericType(eventType));

        return dispatcher.DispatchAsync(@event, cancellationToken);
    }
}
