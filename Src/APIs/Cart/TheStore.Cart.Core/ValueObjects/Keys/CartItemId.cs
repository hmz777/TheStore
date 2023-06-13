using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Cart.Core.ValueObjects.Keys
{
	public class CartItemId : EntityId<int>
	{
		public CartItemId(int id) : base(id) { }
	}
}