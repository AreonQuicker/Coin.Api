using CoinApi.Domain.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoinApi.DataAccess.Configurations
{
    public class CoinFavoriteDomainModelConfiguration : IEntityTypeConfiguration<CoinFavoriteDomainModel>
    {
        public void Configure(EntityTypeBuilder<CoinFavoriteDomainModel> builder)
        {
            builder.ToTable("CoinFavorite");

            builder.Property(t => t.Symbol)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}