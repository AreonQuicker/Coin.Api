// <auto-generated />
using System;
using CoinApi.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoinApi.DataAccess.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CoinApi.Domain.Models.DomainModels.CoinDomainModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Coin", (string)null);
                });

            modelBuilder.Entity("CoinApi.Domain.Models.DomainModels.CoinFavoriteDomainModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("CoinFavorite", (string)null);
                });

            modelBuilder.Entity("CoinApi.Domain.Models.DomainModels.CryptoCurrencyDomainModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("CmcRank")
                        .HasColumnType("decimal(29,10)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("NumMarketPairs")
                        .HasColumnType("decimal(29,10)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("CryptoCurrency", (string)null);
                });

            modelBuilder.Entity("CoinApi.Domain.Models.DomainModels.CryptoCurrencyQuoteDomainModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("CryptoCurrencyId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("MarketCap")
                        .HasColumnType("decimal(29,10)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(29,10)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Volume24H")
                        .HasColumnType("decimal(29,10)");

                    b.HasKey("Id");

                    b.HasIndex("CryptoCurrencyId");

                    b.ToTable("CryptoCurrencyQuote", (string)null);
                });

            modelBuilder.Entity("CoinApi.Domain.Models.DomainModels.CurrencyDomainModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Sign")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Sign", "Symbol")
                        .IsUnique();

                    b.ToTable("Currency", (string)null);
                });

            modelBuilder.Entity("CoinApi.Domain.Models.DomainModels.CurrencyFavoriteDomainModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CoinFavoriteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CoinFavoriteId", "Symbol")
                        .IsUnique();

                    b.ToTable("CurrencyFavorite", (string)null);
                });

            modelBuilder.Entity("CoinApi.Domain.Models.DomainModels.CryptoCurrencyQuoteDomainModel", b =>
                {
                    b.HasOne("CoinApi.Domain.Models.DomainModels.CryptoCurrencyDomainModel", "CryptoCurrency")
                        .WithMany("Quotes")
                        .HasForeignKey("CryptoCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CryptoCurrency");
                });

            modelBuilder.Entity("CoinApi.Domain.Models.DomainModels.CurrencyFavoriteDomainModel", b =>
                {
                    b.HasOne("CoinApi.Domain.Models.DomainModels.CoinFavoriteDomainModel", "CoinFavorite")
                        .WithMany("FavoriteCurrencies")
                        .HasForeignKey("CoinFavoriteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CoinFavorite");
                });

            modelBuilder.Entity("CoinApi.Domain.Models.DomainModels.CoinFavoriteDomainModel", b =>
                {
                    b.Navigation("FavoriteCurrencies");
                });

            modelBuilder.Entity("CoinApi.Domain.Models.DomainModels.CryptoCurrencyDomainModel", b =>
                {
                    b.Navigation("Quotes");
                });
#pragma warning restore 612, 618
        }
    }
}
