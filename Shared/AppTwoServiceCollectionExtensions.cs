using Microsoft.Extensions.DependencyInjection;
using Shared.Clients;

namespace Shared
{
    public static class AppClientServiceCollectionExtensions
    {
        public static IServiceCollection AddAppOne(this IServiceCollection services)
        {
            services.AddHostedService<AppClientHostedService<IAppOneClient>>();
            services.AddSingleton<IAppOneClient, AppOneClient>();
            
            return services;
        }

        public static IServiceCollection AddAppTwo(this IServiceCollection services)
        {
            services.AddHostedService<AppClientHostedService<IAppTwoClient>>();
            services.AddSingleton<IAppTwoClient, AppTwoClient>();

            return services;
        }

    }
}
