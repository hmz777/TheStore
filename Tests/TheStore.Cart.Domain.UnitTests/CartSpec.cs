using FluentAssertions;
using TheStore.Cart.Core.Entities;
using TheStore.Cart.Core.ValueObjects.Keys;

namespace TheStore.Cart.Domain.UnitTests
{
	public class CartSpec
	{
		[Fact]
		public void Can_Create_Cart()
		{
			var action1 = () => new Core.Aggregates.Cart(new BuyerId(Guid.NewGuid()));
			var action2 = () => new Core.Aggregates.Cart(new BuyerId(Guid.NewGuid()), new List<CartItem>());

			action1.Should().NotThrow<Exception>();
			action2.Should().NotThrow<Exception>();
		}

		[Fact]
		public void Can_Add_Cart_Item()
		{
			var cart = new Core.Aggregates.Cart(new BuyerId(Guid.NewGuid()));
			var cartItem = new CartItem("Sku 0", 5);

			cart.AddItem(cartItem);

			cart.Items.Should().Contain(cartItem);
		}

		[Fact]
		public void Can_Remove_Cart_Item()
		{
			var cartItem = new CartItem("Sku 0", 5);
			var cart = new Core.Aggregates.Cart(
				new BuyerId(Guid.NewGuid()), new List<CartItem>() { cartItem });

			cart.RemoveItem(cartItem);

			cart.Items.Should().NotContain(cartItem);
		}
	}
}