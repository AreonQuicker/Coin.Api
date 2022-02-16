using CoinApi.Domain.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoinApi.DataAccess.Configurations
{
    public class CryptoCurrencyDomainModelConfiguration : IEntityTypeConfiguration<CryptoCurrencyDomainModel>
    {
        public void Configure(EntityTypeBuilder<CryptoCurrencyDomainModel> builder)
        {
            builder.ToTable("CryptoCurrency");


            builder.Property(t => t.CmcRank)
                .HasColumnType("decimal(29,10)");

            builder.Property(t => t.NumMarketPairs)
                .HasColumnType("decimal(29,10)");

            builder.Property(t => t.Symbol)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Slug)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}