using TheStore.Cart.Core.ValueObjects.Keys;
using TheStore.SharedKernel.Entities;

namespace TheStore.Cart.Core.Entities
{
	public class WishlistItem : BaseEntity<WishlistItemId>
	{
		public int ProductId { get; set; }

		public WishlistItem(int productId)
		{
			ProductId = productId;
		}
	}
}