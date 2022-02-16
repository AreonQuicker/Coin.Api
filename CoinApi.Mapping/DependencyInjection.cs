using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace CoinApi.Mapping
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}