using Ardalis.GuardClauses;
using System.Collections.ObjectModel;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Core.ValueObjects.Products;

namespace TheStore.Catalog.Core.Aggregates.Products
{
	public class AssembledProduct : SingleProduct
	{
		private List<ProductId> parts = new();
		public ReadOnlyCollection<ProductId> Parts => parts.AsReadOnly();

		// Ef Core
		private AssembledProduct()
		{

		}

		public AssembledProduct(List<ProductId> parts, CategoryId categoryId, string name, string description, string shortDescription, string sku, Money price, InventoryRecord inventory, List<ProductColor>? productColors = null)
			: base(categoryId, name, description, shortDescription, sku, price, inventory, productColors)
		{
			this.parts = parts;
		}

		public void AddPart(ProductId productId)
		{
			Guard.Against.Null(productId, nameof(productId));

			parts.Add(productId);
		}
		public void RemovePart(ProductId productId)
		{
			Guard.Against.Null(productId, nameof(productId));

			parts.Remove(productId);
		}
	}
}
