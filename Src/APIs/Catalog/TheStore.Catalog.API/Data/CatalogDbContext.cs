﻿using Microsoft.EntityFrameworkCore;
using TheStore.Catalog.API.Domain.Branches;
using TheStore.Catalog.API.Domain.Categories;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueConverters;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Core.ValueObjects.Products;

namespace TheStore.Catalog.API.Data
{
	public class CatalogDbContext : DbContext
	{
		public CatalogDbContext(DbContextOptions<CatalogDbContext> dbContextOptions) : base(dbContextOptions)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			#region Branch

			modelBuilder.Entity<Branch>()
				.OwnsOne(b => b.Address);

			modelBuilder.Entity<Branch>()
				.OwnsOne(b => b.Image);//.ToJson();

			#endregion

			#region Single Product

			modelBuilder.Entity<SingleProduct>()
				.Property(s => s.Id)
				.HasConversion<ProductIdValueConverter>();

			modelBuilder.Entity<SingleProduct>()
				.HasKey(s => s.Id);

			modelBuilder.Entity<SingleProduct>()
				.Property(c => c.CategoryId)
				.HasConversion<CategoryIdValueConverter>();

			modelBuilder.Entity<SingleProduct>()
				.OwnsOne(s => s.Price, p =>
				{
					p.Property(m => m.Amount)
					 .HasPrecision(precision: 16, scale: 3);

					p.OwnsOne(pp => pp.Currency);
					//p.ToJson();
				});

			modelBuilder.Entity<SingleProduct>()
				.OwnsOne(s => s.Inventory);

			modelBuilder.Entity<SingleProduct>()
				.OwnsMany<ProductColor>("productColors", pc =>
				{
					pc.OwnsMany(pc => pc.Images);
					//pc.ToJson();
				});

			#endregion

			#region Assembled Product

			//modelBuilder.Entity<AssembledProduct>()
			//	.Property<List<ProductId>>("parts")
			//	.HasConversion<ProductIdValueConverter>();

			#endregion

			#region Category

			modelBuilder.Entity<Category>()
				.Property(c => c.Id)
				.HasConversion<CategoryIdValueConverter>();

			modelBuilder.Entity<Category>()
				.HasKey(c => c.Id);

			#endregion
		}

		public DbSet<Category> Categories => Set<Category>();
		public DbSet<SingleProduct> SingleProducts => Set<SingleProduct>();
		public DbSet<AssembledProduct> AssembledProducts => Set<AssembledProduct>();
		public DbSet<Branch> Branches => Set<Branch>();
	}
}