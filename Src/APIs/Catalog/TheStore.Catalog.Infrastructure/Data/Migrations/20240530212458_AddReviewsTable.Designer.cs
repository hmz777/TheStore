﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheStore.Catalog.Infrastructure.Data;

#nullable disable

namespace TheStore.Catalog.Infrastructure.Data.Migrations
{
    [DbContext(typeof(CatalogDbContext))]
    [Migration("20240530212458_AddReviewsTable")]
    partial class AddReviewsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TheStore.Catalog.Core.Aggregates.Branches.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("ImageID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Published")
                        .HasColumnType("bit");

                    b.ComplexProperty<Dictionary<string, object>>("Address", "TheStore.Catalog.Core.Aggregates.Branches.Branch.Address#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.ComplexProperty<Dictionary<string, object>>("Coordinate", "TheStore.Catalog.Core.Aggregates.Branches.Branch.Address#Address.Coordinate#Coordinate", b2 =>
                                {
                                    b2.IsRequired();

                                    b2.Property<float>("Latitude")
                                        .HasColumnType("real");

                                    b2.Property<float>("Longitude")
                                        .HasColumnType("real");
                                });
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Description", "TheStore.Catalog.Core.Aggregates.Branches.Branch.Description#MultilanguageString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Json")
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("Id");

                    b.HasIndex("ImageID");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("TheStore.Catalog.Core.Aggregates.Categories.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("NeedsSynchronization")
                        .HasColumnType("bit");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<bool>("Published")
                        .HasColumnType("bit");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "TheStore.Catalog.Core.Aggregates.Categories.Category.Name#MultilanguageString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Json")
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TheStore.Catalog.Core.Aggregates.Products.AssembledProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Published")
                        .HasColumnType("bit");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "TheStore.Catalog.Core.Aggregates.Products.AssembledProduct.Description#MultilanguageString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Json")
                                .HasColumnType("nvarchar(max)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("ShortDescription", "TheStore.Catalog.Core.Aggregates.Products.AssembledProduct.ShortDescription#MultilanguageString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Json")
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("Id");

                    b.ToTable("AssembledProducts");
                });

            modelBuilder.Entity("TheStore.Catalog.Core.Aggregates.Products.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Published")
                        .HasColumnType("bit");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "TheStore.Catalog.Core.Aggregates.Products.Product.Description#MultilanguageString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Json")
                                .HasColumnType("nvarchar(max)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("ShortDescription", "TheStore.Catalog.Core.Aggregates.Products.Product.ShortDescription#MultilanguageString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Json")
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("TheStore.Catalog.Core.Aggregates.Products.ProductReview", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("Key", 0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<bool>("Published")
                        .HasColumnType("bit");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductReviews");
                });

            modelBuilder.Entity("TheStore.Catalog.Core.Aggregates.Products.ProductVariant", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("Key", 0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Sku")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "TheStore.Catalog.Core.Aggregates.Products.ProductVariant.Description#MultilanguageString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Json")
                                .HasColumnType("nvarchar(max)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("ShortDescription", "TheStore.Catalog.Core.Aggregates.Products.ProductVariant.ShortDescription#MultilanguageString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Json")
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("ID");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductVariant");
                });

            modelBuilder.Entity("TheStore.Catalog.Core.ValueObjects.Image", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("Key", 0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<bool>("IsMainImage")
                        .HasColumnType("bit");

                    b.Property<int>("ProductColorID")
                        .HasColumnType("int");

                    b.Property<string>("StringFileUri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ComplexProperty<Dictionary<string, object>>("Alt", "TheStore.Catalog.Core.ValueObjects.Image.Alt#MultilanguageString", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Json")
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("ID");

                    b.HasIndex("ProductColorID");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("TheStore.Catalog.Core.ValueObjects.ProductSpecification", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("Key", 0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("ProductVariantID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ProductVariantID");

                    b.ToTable("ProductSpecification");
                });

            modelBuilder.Entity("TheStore.Catalog.Core.ValueObjects.Products.ProductColor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("Key", 0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("ColorCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ColorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsMainColor")
                        .HasColumnType("bit");

                    b.Property<int>("variantId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("variantId")
                        .IsUnique();

                    b.ToTable("ProductColor");
                });

            modelBuilder.Entity("TheStore.Catalog.Core.Aggregates.Branches.Branch", b =>
                {
                    b.HasOne("TheStore.Catalog.Core.ValueObjects.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageID");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("TheStore.Catalog.Core.Aggregates.Products.Product", b =>
                {
                    b.HasOne("TheStore.Catalog.Core.Aggregates.Categories.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("TheStore.Catalog.Core.Aggregates.Products.ProductReview", b =>
                {
                    b.HasOne("TheStore.Catalog.Core.Aggregates.Products.Product", null)
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TheStore.Catalog.Core.Aggregates.Products.ProductVariant", b =>
                {
                    b.HasOne("TheStore.Catalog.Core.Aggregates.Products.Product", null)
                        .WithMany("Variants")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("TheStore.Catalog.Core.Aggregates.Products.ProductVariantOptions", "Options", b1 =>
                        {
                            b1.Property<int>("ProductVariantID")
                                .HasColumnType("int");

                            b1.Property<bool>("CanBeFavorited")
                                .HasColumnType("bit");

                            b1.Property<bool>("CanBePurchased")
                                .HasColumnType("bit");

                            b1.Property<bool>("IsMainVariant")
                                .HasColumnType("bit");

                            b1.Property<bool>("Published")
                                .HasColumnType("bit");

                            b1.HasKey("ProductVariantID");

                            b1.ToTable("ProductVariant");

                            b1.WithOwner()
                                .HasForeignKey("ProductVariantID");
                        });

                    b.OwnsOne("TheStore.Catalog.Core.ValueObjects.Dimensions", "Dimentions", b1 =>
                        {
                            b1.Property<int>("ProductVariantID")
                                .HasColumnType("int");

                            b1.Property<decimal>("Height")
                                .HasPrecision(5, 2)
                                .HasColumnType("decimal");

                            b1.Property<decimal>("Length")
                                .HasPrecision(5, 2)
                                .HasColumnType("decimal");

                            b1.Property<decimal>("Width")
                                .HasPrecision(5, 2)
                                .HasColumnType("decimal");

                            b1.HasKey("ProductVariantID");

                            b1.ToTable("ProductVariant");

                            b1.WithOwner()
                                .HasForeignKey("ProductVariantID");

                            b1.OwnsOne("TheStore.Catalog.Core.ValueObjects.UnitOfMeasure", "Unit", b2 =>
                                {
                                    b2.Property<int>("DimensionsProductVariantID")
                                        .HasColumnType("int");

                                    b2.Property<string>("Unit")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("DimensionsProductVariantID");

                                    b2.ToTable("ProductVariant");

                                    b2.WithOwner()
                                        .HasForeignKey("DimensionsProductVariantID");
                                });

                            b1.Navigation("Unit")
                                .IsRequired();
                        });

                    b.OwnsOne("TheStore.Catalog.Core.ValueObjects.InventoryRecord", "Inventory", b1 =>
                        {
                            b1.Property<int>("ProductVariantID")
                                .HasColumnType("int");

                            b1.Property<int>("AvailableStock")
                                .HasColumnType("int");

                            b1.Property<int>("MaxStockThreshold")
                                .HasColumnType("int");

                            b1.Property<bool>("OnReorder")
                                .HasColumnType("bit");

                            b1.Property<int>("OverStock")
                                .HasColumnType("int");

                            b1.Property<int>("RestockThreshold")
                                .HasColumnType("int");

                            b1.HasKey("ProductVariantID");

                            b1.ToTable("ProductVariant");

                            b1.WithOwner()
                                .HasForeignKey("ProductVariantID");
                        });

                    b.OwnsOne("TheStore.Catalog.Core.ValueObjects.Money", "Price", b1 =>
                        {
                            b1.Property<int>("ProductVariantID")
                                .HasColumnType("int");

                            b1.Property<decimal>("Amount")
                                .HasPrecision(8, 2)
                                .HasColumnType("decimal");

                            b1.HasKey("ProductVariantID");

                            b1.ToTable("ProductVariant");

                            b1.WithOwner()
                                .HasForeignKey("ProductVariantID");

                            b1.OwnsOne("TheStore.Catalog.Core.ValueObjects.Currency", "Currency", b2 =>
                                {
                                    b2.Property<int>("MoneyProductVariantID")
                                        .HasColumnType("int");

                                    b2.Property<string>("CurrencyCode")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("MoneyProductVariantID");

                                    b2.ToTable("ProductVariant");

                                    b2.WithOwner()
                                        .HasForeignKey("MoneyProductVariantID");
                                });

                            b1.Navigation("Currency")
                                .IsRequired();
                        });

                    b.Navigation("Dimentions")
                        .IsRequired();

                    b.Navigation("Inventory")
                        .IsRequired();

                    b.Navigation("Options")
                        .IsRequired();

                    b.Navigation("Price")
                        .IsRequired();
                });

            modelBuilder.Entity("TheStore.Catalog.Core.ValueObjects.Image", b =>
                {
                    b.HasOne("TheStore.Catalog.Core.ValueObjects.Products.ProductColor", null)
                        .WithMany("Images")
                        .HasForeignKey("ProductColorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TheStore.Catalog.Core.ValueObjects.ProductSpecification", b =>
                {
                    b.HasOne("TheStore.Catalog.Core.Aggregates.Products.ProductVariant", null)
                        .WithMany("Sepcifications")
                        .HasForeignKey("ProductVariantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TheStore.Catalog.Core.ValueObjects.Products.ProductColor", b =>
                {
                    b.HasOne("TheStore.Catalog.Core.Aggregates.Products.ProductVariant", null)
                        .WithOne("Color")
                        .HasForeignKey("TheStore.Catalog.Core.ValueObjects.Products.ProductColor", "variantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TheStore.Catalog.Core.Aggregates.Categories.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("TheStore.Catalog.Core.Aggregates.Products.Product", b =>
                {
                    b.Navigation("Reviews");

                    b.Navigation("Variants");
                });

            modelBuilder.Entity("TheStore.Catalog.Core.Aggregates.Products.ProductVariant", b =>
                {
                    b.Navigation("Color")
                        .IsRequired();

                    b.Navigation("Sepcifications");
                });

            modelBuilder.Entity("TheStore.Catalog.Core.ValueObjects.Products.ProductColor", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
