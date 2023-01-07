using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStore.Catalog.API.Domain.Categories;
using TheStore.Catalog.API.Domain.Images;
using TheStore.Catalog.API.Domain.Products;
using TheStore.Catalog.API.Domain.Subcategories;

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

			modelBuilder.Entity<Category>()
				.HasMany(c => c.Subcategories)
				.WithOne(s => s.Category)
				.OnDelete(DeleteBehavior.SetNull);

			modelBuilder.Entity<Subcategory>()
				.HasMany(s => s.Products)
				.WithOne(p => p.Subcategory)
				.OnDelete(DeleteBehavior.SetNull);

			modelBuilder.Entity<Subcategory>()
				.HasOne(s => s.Image)
				.WithOne()
				.HasForeignKey<Image>("ImageSubcategoryFk")
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Product>()
				.HasMany(p => p.Images)
				.WithOne()
				.HasForeignKey("ImageProductFk")
				.OnDelete(DeleteBehavior.Cascade);
		}

		public DbSet<Category> Categories => Set<Category>();
		public DbSet<Subcategory> Subcategories => Set<Subcategory>();
		public DbSet<Product> Products => Set<Product>();
		public DbSet<Image> Images => Set<Image>();
	}
}