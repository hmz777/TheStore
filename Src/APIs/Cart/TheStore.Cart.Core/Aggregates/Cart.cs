using Ardalis.GuardClauses;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using TheStore.Cart.Core.Entities;
using TheStore.Cart.Core.ValueObjects.Keys;
using TheStore.SharedKernel.Entities;
using TheStore.SharedKernel.Interfaces;

namespace TheStore.Cart.Core.Aggregates
{
    public class Cart : BaseEntity<Guid>, IAggregateRoot
    {
        private List<CartItem> items;

        [NotMapped]
        public ReadOnlyCollection<CartItem> Items => items.AsReadOnly();

        public BuyerId BuyerId { get; private set; }

        // Ef Core
        private Cart() { }

        public Cart(BuyerId buyerId, List<CartItem> items = null!)
        {
            BuyerId = buyerId;
            this.items = items ?? new();
        }

        public void AddItem(CartItem item)
        {
            Guard.Against.Null(item, nameof(item));

            if (items.Contains(item))
            {
                item.IncreaseQuantity();
                return;
            }

            items.Add(item);
        }

        public bool UpdateItem(CartItem item)
        {
            Guard.Against.Null(item, nameof(item));

            var cartItem = items.FirstOrDefault(i => i.Sku == item.Sku);

            if (cartItem == null) { return false; }

            // TODO: Later more properties will be added related to more specific attributes like sizes and colors
            cartItem.Quantity = item.Quantity;

            return true;
        }

        public bool RemoveItem(CartItem item)
        {
            Guard.Against.Null(item, nameof(item));

            return items.Remove(item);
        }
    }
}