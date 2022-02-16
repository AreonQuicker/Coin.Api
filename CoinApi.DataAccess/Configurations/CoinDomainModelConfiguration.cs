using CoinApi.Domain.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoinApi.DataAccess.Configurations
{
    public class CoinDomainModelConfiguration : IEntityTypeConfiguration<CoinDomainModel>
    {
        public void Configure(EntityTypeBuilder<CoinDomainModel> builder)
        {
            builder.ToTable("Coin");

            builder.Property(t => t.Symbol)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(t => t.Slug)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}