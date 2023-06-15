using Ardalis.GuardClauses;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using TheStore.Cart.Core.Entities;
using TheStore.Cart.Core.ValueObjects.Keys;
using TheStore.SharedKernel.Entities;
using TheStore.SharedKernel.Interfaces;

namespace TheStore.Cart.Core.Aggregates
{
	public class Wishlist : BaseEntity<Guid>, IAggregateRoot
	{
		private List<WishlistItem> items;

		[NotMapped]
		public ReadOnlyCollection<WishlistItem> Items => items.AsReadOnly();

		public BuyerId BuyerId { get; private set; }

		// Ef Core
		private Wishlist() { }

		public Wishlist(BuyerId buyerId, List<WishlistItem> items = null)
		{
			BuyerId = buyerId;
			this.items = items ?? new();
		}

		public void AddItem(WishlistItem item)
		{
			Guard.Against.Null(item, nameof(item));

			items.Add(item);
		}

		public bool RemoveItem(WishlistItem item)
		{
			Guard.Against.Null(item, nameof(item));

			return items.Remove(item);
		}
	}
}