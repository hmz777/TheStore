using Ardalis.GuardClauses;
using System.Collections.ObjectModel;
using TheStore.Cart.Core.Entities;
using TheStore.Cart.Core.ValueObjects.Keys;
using TheStore.SharedKernel.Entities;
using TheStore.SharedKernel.Interfaces;

namespace TheStore.Cart.Core.Aggregates
{
	public class Cart : BaseEntity<Guid>, IAggregateRoot
	{
		private List<CartItem> items;

		public ReadOnlyCollection<CartItem> Items => items.AsReadOnly();

		public BuyerId BuyerId { get; private set; }

		// Ef Core
		private Cart() { }

		public Cart(BuyerId buyerId, List<CartItem> items = null)
		{
			BuyerId = buyerId;
			this.items = items ?? new();
		}

		public void AddItem(CartItem item)
		{
			Guard.Against.Null(item, nameof(item));

			items.Add(item);
		}

		public bool RemoveItem(CartItem item)
		{
			Guard.Against.Null(item, nameof(item));

			return items.Remove(item);
		}
	}
}