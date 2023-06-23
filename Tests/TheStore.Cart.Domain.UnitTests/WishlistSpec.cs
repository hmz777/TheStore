using FluentAssertions;
using TheStore.Cart.Core.Aggregates;
using TheStore.Cart.Core.Entities;
using TheStore.Cart.Core.ValueObjects.Keys;

namespace TheStore.Cart.Domain.UnitTests
{
	public class WishlistSpec
	{
		[Fact]
		public void Can_Create_Wishlist()
		{
			var action1 = () => new Wishlist(new BuyerId(Guid.NewGuid()));
			var action2 = () => new Wishlist(new BuyerId(Guid.NewGuid()), new List<WishlistItem>());

			action1.Should().NotThrow<Exception>();
			action2.Should().NotThrow<Exception>();
		}

		[Fact]
		public void Can_Add_Wishlist_Item()
		{
			var wishlist = new Wishlist(new BuyerId(Guid.NewGuid()));
			var wishlistItem = new WishlistItem(1);

			wishlist.AddItem(wishlistItem);

			wishlist.Items.Should().Contain(wishlistItem);
		}

		[Fact]
		public void Can_Remove_Wishlist_Item()
		{
			var wishlistItem = new WishlistItem(1);
			var wishlist = new Wishlist(
				new BuyerId(Guid.NewGuid()), new List<WishlistItem>() { wishlistItem });

			wishlist.RemoveItem(wishlistItem);

			wishlist.Items.Should().NotContain(wishlistItem);
		}
	}
}
