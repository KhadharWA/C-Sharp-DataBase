

using Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Shared.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{

    public virtual DbSet<ProductEntity> Products { get; set; }

    public virtual DbSet<CategoryEntity> Categories { get; set; }

    public virtual DbSet<CurrencyEntity> Currencies { get; set; }

    public virtual DbSet<ImageEntity> Images { get; set; }

    public virtual DbSet<ManufacturesEntity> Manufactures { get; set; }

    public virtual DbSet<ProductCurrencyEntity> ProductImages { get; set; }

    public virtual DbSet<ProductPriceEntity> ProductPrices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ManufacturesEntity>()
            .HasIndex(x => x.ManufacturerName)
            .IsUnique();

        modelBuilder.Entity<ImageEntity>()
            .HasIndex(x => x.ImageUrl)
            .IsUnique();

        modelBuilder.Entity<ProductCurrencyEntity>()
            .HasKey(pie => new { pie.ArticleNumber, pie.ImageId });
    }

    
}
