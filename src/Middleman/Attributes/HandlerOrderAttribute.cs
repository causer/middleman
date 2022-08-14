namespace Middleman.Attributes;

/// <summary>
/// Configures an order of handler execution.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class HandlerOrderAttribute : Attribute
{
    public const int DefaultHandlerOrder = 1000;

    /// <summary>
    /// Initializes a new instance of the <see cref="HandlerOrderAttribute"/>.
    /// </summary>
    /// <param name="order">The execution order of the corresponding event handler.</param>
    public HandlerOrderAttribute(int order)
    {
        Order = order;
    }

    /// <summary>
    /// Gets the execution order of the corresponding event handler.
    /// </summary>
    public int Order { get; }
}

