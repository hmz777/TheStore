using Microsoft.EntityFrameworkCore;
using TheStore.ApiCommon.Services;
using TheStore.Catalog.Core.Aggregates.Branches;
using TheStore.Catalog.Core.Aggregates.Categories;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Products;
using TheStore.Catalog.Infrastructure.Data.ValueConverters;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Catalog.Infrastructure.Data
{
	public class CatalogDbContext : DbContext
	{
		private readonly EventDispatcher eventDispatcher;

		public CatalogDbContext(
			DbContextOptions<CatalogDbContext> dbContextOptions,
			EventDispatcher eventDispatcher) : base(dbContextOptions)
		{
			this.eventDispatcher = eventDispatcher;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			#region Value Objects

			modelBuilder.Entity<MultilanguageString>()
				.OwnsMany<LocalizedString>("localizedStrings", opt =>
				{
					opt.OwnsOne(ls => ls.CultureCode);
				});

			modelBuilder.Entity<Address>()
				.OwnsOne(ad => ad.Coordinate);

			modelBuilder.Entity<ProductVariant>(opt =>
			{
				opt.OwnsOne(v => v.Description);
				opt.OwnsOne(v => v.ShortDescription);
				opt.OwnsOne(v => v.Price);
				opt.OwnsOne(v => v.Inventory);
				opt.OwnsOne(v => v.Color);
				opt.OwnsOne(v => v.Dimentions);
				opt.OwnsOne(v => v.Sepcifications);
				opt.OwnsMany<ProductReview>("reviews");
			});

			modelBuilder.Entity<ProductColor>(opt =>
			{
				opt.OwnsMany<Image>("images")
				   .OwnsOne(i => i.Alt);
			});

			#endregion

			#region Branch

			modelBuilder.Entity<Branch>()
				.OwnsOne(b => b.Address);

			modelBuilder.Entity<Branch>()
				.OwnsOne(b => b.Image);

			modelBuilder.Entity<Branch>()
				.OwnsOne(b => b.Name);

			modelBuilder.Entity<Branch>()
				.OwnsOne(b => b.Description);

			#endregion

			#region Category

			modelBuilder.Entity<Category>()
				.Property(c => c.Id)
				.HasConversion<CategoryIdValueConverter>();

			modelBuilder.Entity<Category>()
				.HasKey(c => c.Id);

			modelBuilder.Entity<Category>()
				.OwnsOne(c => c.Name);

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
				.OwnsOne(p => p.Name);

			modelBuilder.Entity<Product>()
				.OwnsMany(p => p.Variants);

			modelBuilder.Entity<Product>()
				.OwnsMany<ProductColor>("productColors");

			#endregion

			#region Assembled Product

			modelBuilder.Entity<AssembledProduct>()
				.Property(c => c.CategoryId)
				.HasConversion<CategoryIdValueConverter>();

			#endregion
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			await eventDispatcher.PublishDomainEventsAsync(cancellationToken);

			// We trigger domain events before saving in order to
			// commit changes as a single transaction

			var result = await base.SaveChangesAsync(cancellationToken);

			await eventDispatcher.PublishIntegrationEventsAsync(cancellationToken);

			return result;
		}

		public DbSet<Category> Categories => Set<Category>();
		public DbSet<Product> Products => Set<Product>();
		public DbSet<AssembledProduct> AssembledProducts => Set<AssembledProduct>();
		public DbSet<Branch> Branches => Set<Branch>();
	}
}