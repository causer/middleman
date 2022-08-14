namespace Middleman.Queries;

/// <summary>
/// Specifies the contract for a query with a result.
/// </summary>
/// <typeparam name="TResult">Type of the result.</typeparam>
public interface IQuery<out TResult> where TResult : class
{
}
