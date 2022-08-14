namespace Middleman;

/// <summary>
/// Represents a combined service for commands, queries and events.
/// </summary>
public interface IMiddleman : IQueryBus, ICommandBus, IEventBus
{
}
