﻿// <auto-generated />

using Shared.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;


#nullable disable

namespace Shared.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DatabaseContexts.Entities.CategoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DatabaseContexts.Entities.ImageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ImageUrl")
                        .IsUnique();

                    b.ToTable("Images");
                });

            modelBuilder.Entity("DatabaseContexts.Entities.ManufacturesEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ManufacturerName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerName")
                        .IsUnique();

                    b.ToTable("Manufactures");
                });

            modelBuilder.Entity("DatabaseContexts.Entities.ProductEntity", b =>
                {
                    b.Property<string>("ArticleNumber")
                        .HasMaxLength(225)
                        .HasColumnType("nvarchar(225)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ingress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ManufacturerId")
                        .HasColumnType("int");

                    b.Property<string>("Specification")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("ArticleNumber");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("DatabaseContexts.Entities.ProductImageEntity", b =>
                {
                    b.Property<string>("ArticleNumber")
                        .HasColumnType("nvarchar(225)")
                        .HasColumnOrder(1);

                    b.Property<int>("ImageId")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.HasKey("ArticleNumber", "ImageId");

                    b.HasIndex("ImageId");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("DatabaseContexts.Entities.ProductPriceEntity", b =>
                {
                    b.Property<string>("ArticleNumber")
                        .HasColumnType("nvarchar(225)");

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("CurrencyEntityCode")
                        .HasColumnType("nvarchar(3)");

                    b.Property<decimal?>("DiscountPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("ArticleNumber");

                    b.HasIndex("CurrencyEntityCode");

                    b.ToTable("ProductPrices");
                });

            modelBuilder.Entity("DatabaseContexts.Models.CurrencyEntity", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("CurrencyName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Code");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("DatabaseContexts.Entities.ProductEntity", b =>
                {
                    b.HasOne("DatabaseContexts.Entities.CategoryEntity", "CategoryEntity")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatabaseContexts.Entities.ManufacturesEntity", "ManufacturesEntity")
                        .WithMany()
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryEntity");

                    b.Navigation("ManufacturesEntity");
                });

            modelBuilder.Entity("DatabaseContexts.Entities.ProductImageEntity", b =>
                {
                    b.HasOne("DatabaseContexts.Entities.ProductEntity", "Product")
                        .WithMany()
                        .HasForeignKey("ArticleNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatabaseContexts.Entities.ImageEntity", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DatabaseContexts.Entities.ProductPriceEntity", b =>
                {
                    b.HasOne("DatabaseContexts.Entities.ProductEntity", "ProductEntity")
                        .WithMany()
                        .HasForeignKey("ArticleNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatabaseContexts.Models.CurrencyEntity", "CurrencyEntity")
                        .WithMany()
                        .HasForeignKey("CurrencyEntityCode");

                    b.Navigation("CurrencyEntity");

                    b.Navigation("ProductEntity");
                });
#pragma warning restore 612, 618
        }
    }
}
