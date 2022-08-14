namespace Middleman.Commands;

/// <summary>
/// Represents an interface for a command handlers.
/// </summary>
/// <typeparam name="TCommand">Type of the command.</typeparam>
public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
{
    /// <summary>
    /// Asynchronously handles the specified command.
    /// </summary>
    /// <param name="command">The instance of the <see href="ICommand"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}
