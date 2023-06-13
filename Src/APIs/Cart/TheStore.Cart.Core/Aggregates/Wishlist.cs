using Ardalis.GuardClauses;
using TheStore.Cart.Core.ValueObjects.Keys;
using TheStore.SharedKernel.Entities;
using TheStore.SharedKernel.Interfaces;

namespace TheStore.Cart.Core.Aggregates
{
	public class Wishlist : BaseEntity<Guid>, IAggregateRoot
	{
		private List<WishlistItemId> items;

		public BuyerId BuyerId { get; private set; }

		// Ef Core
		private Wishlist() { }

		public Wishlist(BuyerId buyerId, List<WishlistItemId> items = null)
		{
			BuyerId = buyerId;
			this.items = items ?? new();
		}

		public void AddItem(WishlistItemId itemId)
		{
			Guard.Against.Null(itemId, nameof(itemId));

			items.Add(itemId);
		}

		public bool RemoveItem(WishlistItemId itemId)
		{
			Guard.Against.Null(itemId, nameof(itemId));

			return items.Remove(itemId);
		}
	}
}