using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheStore.Cart.Core.ValueObjects.Keys;

namespace TheStore.Cart.Infrastructure.Data.ValueConverters
{
	public class CartItemIdValueConverter : ValueConverter<CartItemId, int>
	{
		public CartItemIdValueConverter() : base(cartItemId => cartItemId.Id,
			id => new CartItemId(id))
		{ }
	}
}