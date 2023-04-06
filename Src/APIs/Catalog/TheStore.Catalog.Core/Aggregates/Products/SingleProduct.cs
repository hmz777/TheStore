using Ardalis.GuardClauses;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.SharedKernel.Interfaces;

namespace TheStore.Catalog.Core.Aggregates.Products
{
	public class SingleProduct : Product, IAggregateRoot
	{
		// Ef Core
		protected SingleProduct()
		{

		}

		public SingleProduct(CategoryId categoryId, string name, string description, string shortDescription, string sku, Money price, InventoryRecord inventory)
			: base(name, description, shortDescription, sku, price, inventory)
		{
			Guard.Against.Null(categoryId, nameof(categoryId));

			CategoryId = categoryId;
		}

		public CategoryId CategoryId { get; set; }
	}
}