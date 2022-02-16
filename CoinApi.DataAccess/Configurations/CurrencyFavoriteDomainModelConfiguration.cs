using CoinApi.Domain.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoinApi.DataAccess.Configurations
{
    public class CurrencyFavoriteDomainModelConfiguration : IEntityTypeConfiguration<CurrencyFavoriteDomainModel>
    {
        public void Configure(EntityTypeBuilder<CurrencyFavoriteDomainModel> builder)
        {
            builder.ToTable("CurrencyFavorite");

            builder.HasIndex(a => new {a.CoinFavoriteId, a.Symbol}).IsUnique();

            builder.Property(t => t.Symbol)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne(a => a.CoinFavorite)
                .WithMany(a => a.FavoriteCurrencies)
                .HasForeignKey(a => a.CoinFavoriteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}