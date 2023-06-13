using TheStore.ApiCommon.Interfaces;
using TheStore.Cart.Core.Aggregates;
using TheStore.Cart.Core.ValueObjects.Keys;

namespace TheStore.Cart.Infrastructure.Data
{
	public class DataSeeder : IDataSeeder<CartDbContext>
	{
		public async Task SeedDataAsync(CartDbContext context)
		{
			(var buyers, var carts, var wishLists) = GenerateData();

			await context.Buyers.AddRangeAsync(buyers);
			await context.Carts.AddRangeAsync(carts);
			await context.Wishlists.AddRangeAsync(wishLists);

			await context.SaveChangesAsync();
		}

		private (List<Buyer>, List<Core.Aggregates.Cart>, List<Wishlist>) GenerateData()
		{
			var buyers = new List<Buyer>();
			var carts = new List<Core.Aggregates.Cart>();
			var wishlists = new List<Wishlist>();

			for (int i = 0; i < 5; i++)
			{
				var buyerId = new BuyerId(Guid.NewGuid());
				var buyer = new Buyer(
					buyerId,
					$"Name {i}",
					$"Last Name {i}");

				var cart = new Core.Aggregates.Cart(buyerId);
				var wishlist = new Wishlist(buyerId);

				buyers.Add(buyer);
				carts.Add(cart);
				wishlists.Add(wishlist);
			}

			return (buyers, carts, wishlists);
		}
	}
}