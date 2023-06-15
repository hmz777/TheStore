using TheStore.Cart.Core.ValueObjects.Keys;
using TheStore.SharedKernel.Entities;

namespace TheStore.Cart.Core.Entities
{
	public class CartItem : BaseEntity<CartItemId>
	{
		public int ProductId { get; set; }
		public int Quantity { get; set; }

		public CartItem(int productId, int quantity)
		{
			ProductId = productId;
			Quantity = quantity;
		}

		public void IncreaseQuantity() => Quantity++;
		public void DecreaseQuantity() => Quantity--;
	}
}