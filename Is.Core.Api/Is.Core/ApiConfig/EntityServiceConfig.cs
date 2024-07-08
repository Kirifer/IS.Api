using Microsoft.Extensions.DependencyInjection;
using Is.Core.Extensions;

namespace Is.Core.ApiConfig
{
    public static class EntityServiceConfig
    {
        public static IServiceCollection AddCoreEntityServices<TService>(
            this IServiceCollection services,
            string assemblyName)
        {
            services.RegisterAssemblies<TService>(assemblyName, DependencyLifetime.Scoped);
            return services;
        }
    }
}
