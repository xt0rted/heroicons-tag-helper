namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods to <see cref="IServiceCollection"/> for configuring heroicons.
/// </summary>
public static class HeroiconsExtensions
{
    /// <summary>
    /// Adds the default heroicons configuration.
    /// </summary>
    /// <param name="services">The services available in the application.</param>
    /// <param name="configuration">The configuration available in the application.</param>
    /// <returns>The services.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddHeroicons(this IServiceCollection services, IConfiguration configuration)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));
        if (configuration is null) throw new ArgumentNullException(nameof(configuration));

        services.Configure<HeroiconOptions>(configuration.GetSection("Heroicons"));

        return services;
    }

    /// <summary>
    /// Adds a custom heroicons configuration.
    /// </summary>
    /// <param name="services">The services available in the application.</param>
    /// <param name="configureOptions">An action to configure the <see cref="HeroiconOptions"/>.</param>
    /// <returns>The services.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddHeroicons(this IServiceCollection services, Action<HeroiconOptions> configureOptions)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));
        if (configureOptions is null) throw new ArgumentNullException(nameof(configureOptions));

        services.Configure(configureOptions);

        return services;
    }
}
