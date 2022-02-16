using CoinApi.Domain.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoinApi.DataAccess.Configurations
{
    public class CurrencyDomainModelConfiguration : IEntityTypeConfiguration<CurrencyDomainModel>
    {
        public void Configure(EntityTypeBuilder<CurrencyDomainModel> builder)
        {
            builder.ToTable("Currency");

            builder.HasIndex(a => new {a.Sign, a.Symbol}).IsUnique();

            builder.Property(t => t.Symbol)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(t => t.Sign)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}