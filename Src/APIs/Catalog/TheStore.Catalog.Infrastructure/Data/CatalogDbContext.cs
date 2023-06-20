using Microsoft.EntityFrameworkCore;
using TheStore.Catalog.Core.Aggregates.Branches;
using TheStore.Catalog.Core.Aggregates.Categories;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Products;
using TheStore.Catalog.Infrastructure.Data.ValueConverters;

namespace TheStore.Catalog.Infrastructure.Data
{
	public class CatalogDbContext : DbContext
	{
		public CatalogDbContext(DbContextOptions<CatalogDbContext> dbContextOptions) : base(dbContextOptions) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			#region Branch

			modelBuilder.Entity<Branch>()
				.OwnsOne(b => b.Address, ad => ad.OwnsOne(add => add.Coordinate));

			modelBuilder.Entity<Branch>()
				.OwnsOne(b => b.Image);

			#endregion

			#region Product

			modelBuilder.Entity<Product>()
				.Property(s => s.Id)
				.HasConversion<ProductIdValueConverter>();

			modelBuilder.Entity<Product>()
				.HasKey(s => s.Id);

			modelBuilder.Entity<Product>()
				.Property(c => c.CategoryId)
				.HasConversion<CategoryIdValueConverter>();

			modelBuilder.Entity<Product>()
				.OwnsOne(s => s.Price, p =>
				{
					p.Property(m => m.Amount)
					 .HasPrecision(precision: 16, scale: 3);

					p.OwnsOne(pp => pp.Currency);
				});

			modelBuilder.Entity<Product>()
				.OwnsOne(s => s.Inventory);

			modelBuilder.Entity<Product>()
				.OwnsMany<ProductColor>("productColors", pc =>
				{
					pc.OwnsMany<Image>("images");
				});

			#endregion

			#region Assembled Product

			modelBuilder.Entity<AssembledProduct>()
				.Property(c => c.CategoryId)
				.HasConversion<CategoryIdValueConverter>();

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
		public DbSet<Product> Products => Set<Product>();
		public DbSet<AssembledProduct> AssembledProducts => Set<AssembledProduct>();
		public DbSet<Branch> Branches => Set<Branch>();
	}
}