namespace Microsoft.Extensions.DependencyInjection
{
    using System;

    using Microsoft.Extensions.Configuration;

    using Tailwind.Heroicons;

    public static class HeroiconsExtensions
    {
        public static IServiceCollection AddHeroicons(this IServiceCollection services, IConfiguration configuration)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));
            if (configuration is null) throw new ArgumentNullException(nameof(configuration));

            services.Configure<HeroiconOptions>(configuration.GetSection("Heroicons"));

            return services;
        }

        public static IServiceCollection AddHeroicons(this IServiceCollection services, Action<HeroiconOptions> configureOptions)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));
            if (configureOptions is null) throw new ArgumentNullException(nameof(configureOptions));

            services.Configure(configureOptions);

            return services;
        }
    }
}
