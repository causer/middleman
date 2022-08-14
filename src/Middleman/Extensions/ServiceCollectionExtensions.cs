using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Middleman.Extensions;

/// <summary>
/// Extensions for add Middleman dependencies.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Middleman services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">A <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns><see cref="IServiceCollection"/> containing added services.</returns>
    public static IServiceCollection AddMiddleman(this IServiceCollection services)
    {
        services.TryAddTransient<IMiddleman, DefaultMiddleman>();
        services.TryAddTransient<IEventBus>(sp => sp.GetRequiredService<IMiddleman>());
        services.TryAddTransient<ICommandBus>(sp => sp.GetRequiredService<IMiddleman>());
        services.TryAddTransient<IQueryBus>(sp => sp.GetRequiredService<IMiddleman>());
        return services;
    }
}
