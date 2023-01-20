using Microsoft.EntityFrameworkCore;
using TheStore.Catalog.API.Domain.Categories;
using TheStore.Catalog.Core.Aggregates.Products;

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
		}

		public DbSet<Category> Categories => Set<Category>();
		public DbSet<Product> Products => Set<Product>();
	}
}