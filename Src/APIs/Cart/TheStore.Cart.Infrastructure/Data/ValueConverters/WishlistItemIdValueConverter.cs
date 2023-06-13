using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheStore.Cart.Core.ValueObjects.Keys;

namespace TheStore.Cart.Infrastructure.Data.ValueConverters
{
	public class WishlistItemIdValueConverter : ValueConverter<WishlistItemId, int>
	{
		public WishlistItemIdValueConverter() : base(wishlistItemId => wishlistItemId.Id,
			id => new WishlistItemId(id))
		{ }
	}
}