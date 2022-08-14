namespace Middleman.Commands;

/// <summary>
/// A dispatcher that coordinates commands to appropriate handlers.
/// </summary>
public interface ICommandBus
{
    /// <summary>
    /// Asynchronously send a command to an appropriate command handler.
    /// </summary>
    /// <typeparam name="TCommand">Type of a command.</typeparam>
    /// <param name="command">An instance of <see cref="ICommand"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : class, ICommand;
}
