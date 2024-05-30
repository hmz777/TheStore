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

			#region Branch

			modelBuilder.Entity<Branch>(entity =>
			{
				entity
				.HasOne(b => b.Image);

				entity
				.Navigation(b => b.Image)
				.AutoInclude();
			});

			#endregion

			#region Category

			modelBuilder.Entity<Category>(entity =>
			{
				entity
				.Property(c => c.Id)
				.HasConversion<CategoryIdValueConverter>();

				entity
				.HasKey(c => c.Id);
			});

			#endregion

			#region Product

			modelBuilder.Entity<Product>(entity =>
			{
				entity
				.Property(s => s.Id)
				.HasConversion<ProductIdValueConverter>();

				entity
				.HasKey(s => s.Id);

				entity
				.Property(c => c.CategoryId)
				.HasConversion<CategoryIdValueConverter>()
				.HasColumnType("int");

				entity
				.HasOne(p => p.Category)
				.WithMany(c => c.Products)
				.HasForeignKey(p => p.CategoryId)
				.IsRequired();

				entity
				.Navigation(p => p.Category)
				.AutoInclude();

				entity
				.HasMany(p => p.Variants)
				.WithOne()
				.IsRequired();

				entity
				.HasMany(p => p.Reviews)
				.WithOne()
				.HasForeignKey(r => r.ProductId);
			});

			modelBuilder.Entity<ProductVariant>(opt =>
			{
				opt.Property<int>("ID")
					.HasColumnType("int")
					.ValueGeneratedOnAdd()
					.HasAnnotation("Key", 0);

				opt.OwnsOne(v => v.Price, priceOpt =>
				{
					priceOpt.OwnsOne(pOpt => pOpt.Currency);
					priceOpt.Property(c => c.Amount).HasColumnType("decimal").HasPrecision(8, 2);
				});

				opt.OwnsOne(v => v.Inventory);

				opt.HasOne(v => v.Color)
				   .WithOne()
				   .HasForeignKey<ProductColor>("variantId")
				   .IsRequired();

				opt.Navigation(v => v.Color).AutoInclude();

				opt.OwnsOne(v => v.Options);

				opt.OwnsOne(v => v.Dimentions, opt =>
				{
					opt.OwnsOne(d => d.Unit);
					opt.Property(d => d.Width).HasColumnType("decimal").HasPrecision(5, 2);
					opt.Property(d => d.Height).HasColumnType("decimal").HasPrecision(5, 2);
					opt.Property(d => d.Length).HasColumnType("decimal").HasPrecision(5, 2);
				});

				opt.HasMany(v => v.Sepcifications)
				   .WithOne()
				   .IsRequired();
			});

			modelBuilder.Entity<ProductColor>(opt =>
			{
				opt.Property<int>("ID")
				.HasColumnType("int")
				.ValueGeneratedOnAdd()
				.HasAnnotation("Key", 0);

				opt.HasMany(pc => pc.Images)
				   .WithOne()
				   .IsRequired();

				opt.Navigation(pc => pc.Images).AutoInclude();
			});

			modelBuilder.Entity<Image>(opt =>
			{
				opt.Property<int>("ID")
				.HasColumnType("int")
				.ValueGeneratedOnAdd()
				.HasAnnotation("Key", 0);
			});

			modelBuilder.Entity<ProductReview>(opt =>
			{
				opt.Property<int>("ID")
				.HasColumnType("int")
				.ValueGeneratedOnAdd()
				.HasAnnotation("Key", 0);
			});

			modelBuilder.Entity<ProductSpecification>(opt =>
			{
				opt.Property<int>("ID")
				.HasColumnType("int")
				.ValueGeneratedOnAdd()
				.HasAnnotation("Key", 0);
			});

			#endregion

			#region Assembled Product

			modelBuilder.Entity<AssembledProduct>()
				.Property(c => c.CategoryId)
				.HasConversion<CategoryIdValueConverter>();

			#endregion
		}

		protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
		{
			configurationBuilder.ComplexProperties<Address>();
			configurationBuilder.ComplexProperties<Coordinate>();
			configurationBuilder.ComplexProperties<MultilanguageString>();
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
		public DbSet<ProductReview> ProductReviews => Set<ProductReview>();
		public DbSet<AssembledProduct> AssembledProducts => Set<AssembledProduct>();
		public DbSet<Branch> Branches => Set<Branch>();
	}
}