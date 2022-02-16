using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CoinApi.Domain.Models;
using CoinApi.Domain.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace CoinApi.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<CurrencyFavoriteDomainModel> CurrencyFavorites { get; set; }
        public DbSet<CoinDomainModel> Coins { get; set; }
        public DbSet<CurrencyDomainModel> Currencies { get; set; }
        public DbSet<CoinFavoriteDomainModel> CoinFavorites { get; set; }

        public DbSet<CryptoCurrencyDomainModel> CryptoCurrencies { get; set; }

        public DbSet<CryptoCurrencyQuoteDomainModel> CryptoCurrencyQuotes { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            var dateNow = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries<AuditDomainModel>())
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = dateNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = dateNow;
                        break;
                }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}