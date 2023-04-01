using Microsoft.EntityFrameworkCore;
using TheStore.Catalog.API.Domain.Branches;
using TheStore.Catalog.API.Domain.Categories;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueConverters;

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
				.OwnsOne(b => b.Image);

			#endregion

			#region Single Product

			modelBuilder.Entity<SingleProduct>()
				.Property(s => s.Id)
				.HasConversion<ProductIdValueConverter>();

			modelBuilder.Entity<SingleProduct>()
				.HasKey(s => s.Id);

			modelBuilder.Entity<SingleProduct>()
				.OwnsOne(s => s.Price, p => p.OwnsOne(pp => pp.Currency));

			modelBuilder.Entity<SingleProduct>()
				.OwnsOne(s => s.Inventory);

			modelBuilder.Entity<SingleProduct>()
				.OwnsMany(s => s.ProductColors, pc =>
				{
					pc.OwnsMany(pc => pc.Images);
				});

			#endregion

			#region Assembled Product

			modelBuilder.Entity<AssembledProduct>()
				.HasMany(ap => ap.Parts);

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