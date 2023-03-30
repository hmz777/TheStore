using Ardalis.GuardClauses;
using System.Collections.ObjectModel;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Products;
using TheStore.SharedKernel.Entities;

namespace TheStore.Catalog.Core.Aggregates.Products
{
	public class Product : BaseEntity<int>
	{
		public string Name { get; private set; }
		public string Description { get; private set; }
		public string ShortDescription { get; private set; }
		public string Sku { get; private set; }
		public InventoryRecord Inventory { get; private set; }

		private List<ProductColor> _productColors = new();
		public ReadOnlyCollection<ProductColor> ProductColors => _productColors.AsReadOnly();

		public Product(string name, string description, string shortDescription, string sku, InventoryRecord inventory)
		{
			Guard.Against.NullOrWhiteSpace(name, nameof(name));
			Guard.Against.NullOrWhiteSpace(description, nameof(description));
			Guard.Against.NullOrWhiteSpace(shortDescription, nameof(shortDescription));
			Guard.Against.NullOrWhiteSpace(sku, nameof(sku));
			Guard.Against.Null(inventory, nameof(inventory));

			Name = name;
			Description = description;
			ShortDescription = shortDescription;
			Sku = sku;
			Inventory = inventory;
		}

		#region API

		public bool HasColors() => ProductColors.Any();

		public void AddOrUpdateColor(ProductColor productColor)
		{
			Guard.Against.Null(productColor, nameof(productColor));

			var color = _productColors.FirstOrDefault(c => c == productColor);

			if (color != null)
			{
				_productColors.Remove(color);
			}

			_productColors.Add(productColor);
		}

		public void RemoveColor(ProductColor productColor)
		{
			Guard.Against.Null(productColor, nameof(productColor));

			_productColors.Remove(productColor);
		}

		#endregion
	}
}