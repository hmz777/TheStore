using Microsoft.EntityFrameworkCore;
using TheStore.ApiCommon.Data.ValueConverters;
using TheStore.Cart.Core.Aggregates;
using TheStore.Cart.Core.Entities;
using TheStore.Cart.Core.ValueObjects.Keys;
using TheStore.Cart.Infrastructure.Data.ValueConverters;

namespace TheStore.Cart.Infrastructure.Data
{
	public class CartDbContext : DbContext
	{
		public CartDbContext(DbContextOptions<CartDbContext> dbContextOptions) : base(dbContextOptions) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			#region Cart

			modelBuilder.Entity<Core.Aggregates.Cart>()
				.Property(x => x.Id)
				.HasConversion(new GuidValueConverter());

			modelBuilder.Entity<Core.Aggregates.Cart>()
				.HasKey(x => x.Id);

			modelBuilder.Entity<Core.Aggregates.Cart>()
				.HasMany<CartItemId>("items")
				.WithOne();

			#endregion

			#region Wishlist

			modelBuilder.Entity<Wishlist>()
				.Property(x => x.Id)
				.HasConversion(new GuidValueConverter());

			modelBuilder.Entity<Wishlist>()
				.HasKey(x => x.Id);

			modelBuilder.Entity<Wishlist>()
				.HasMany<WishlistItemId>("items")
				.WithOne();

			#endregion

			#region Cart Item

			modelBuilder.Entity<CartItem>()
				.Property(x => x.Id)
				.HasConversion(new CartItemIdValueConverter());

			modelBuilder.Entity<CartItem>()
				.HasKey(x => x.Id);

			#endregion

			#region Wishlist Item

			modelBuilder.Entity<WishlistItem>()
				.Property(x => x.Id)
				.HasConversion(new WishlistItemIdValueConverter());

			modelBuilder.Entity<WishlistItem>()
				.HasKey(x => x.Id);

			#endregion

			#region Buyer

			modelBuilder.Entity<Buyer>()
				.Property(x => x.Id)
				.HasConversion(new BuyerIdValueConverter());

			modelBuilder.Entity<Buyer>()
				.HasKey(x => x.Id);

			#endregion
		}

		public DbSet<Core.Aggregates.Cart> Carts => Set<Core.Aggregates.Cart>();
		public DbSet<Wishlist> Wishlists => Set<Wishlist>();
		public DbSet<Buyer> Buyers => Set<Buyer>();
    }
}