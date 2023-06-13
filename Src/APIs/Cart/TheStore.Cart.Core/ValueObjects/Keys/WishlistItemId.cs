using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Cart.Core.ValueObjects.Keys
{
	public class WishlistItemId : EntityId<int>
	{
		public WishlistItemId(int id) : base(id) { }
	}
}