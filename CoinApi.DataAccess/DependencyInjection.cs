using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoinApi.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration,
            bool useInMemoryDatabase)
        {
            if (useInMemoryDatabase)
                services.AddDbContext<DataContext>(options =>
                    options.UseInMemoryDatabase("MemoryDb"));
            else
                services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("LOCAL"),
                        b => b.MigrationsAssembly(typeof(DataContext).Assembly.FullName)));


            return services;
        }
    }
}