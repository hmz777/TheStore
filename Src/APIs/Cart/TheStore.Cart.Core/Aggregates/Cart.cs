using Ardalis.GuardClauses;
using TheStore.Cart.Core.ValueObjects.Keys;
using TheStore.SharedKernel.Entities;
using TheStore.SharedKernel.Interfaces;

namespace TheStore.Cart.Core.Aggregates
{
	public class Cart : BaseEntity<Guid>, IAggregateRoot
	{
		private List<CartItemId> items = new();

		public BuyerId BuyerId { get; private set; }

		// Ef Core
		private Cart() { }

		public Cart(BuyerId buyerId, List<CartItemId> items = null)
		{
			BuyerId = buyerId;
			this.items = items ?? new();
		}

		public void AddItem(CartItemId itemId)
		{
			Guard.Against.Null(itemId, nameof(itemId));

			items.Add(itemId);
		}

		public bool RemoveItem(CartItemId itemId)
		{
			Guard.Against.Null(itemId, nameof(itemId));

			return items.Remove(itemId);
		}
	}
}