using CoinApi.Domain.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoinApi.DataAccess.Configurations
{
    public class CryptoCurrencyQuoteDomainModelConfiguration : IEntityTypeConfiguration<CryptoCurrencyQuoteDomainModel>
    {
        public void Configure(EntityTypeBuilder<CryptoCurrencyQuoteDomainModel> builder)
        {
            builder.ToTable("CryptoCurrencyQuote");

            builder.Property(t => t.Symbol)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Price)
                .HasColumnType("decimal(29,10)");

            builder.Property(t => t.Volume24H)
                .HasColumnType("decimal(29,10)");

            builder.Property(t => t.MarketCap)
                .HasColumnType("decimal(29,10)");

            builder.HasOne(a => a.CryptoCurrency)
                .WithMany(a => a.Quotes)
                .HasForeignKey(a => a.CryptoCurrencyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}