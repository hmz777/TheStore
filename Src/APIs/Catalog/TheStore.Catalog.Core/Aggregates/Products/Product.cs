using Ardalis.GuardClauses;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Core.ValueObjects.Products;
using TheStore.SharedKernel.Entities;

namespace TheStore.Catalog.Core.Aggregates.Products
{
	public class Product : BaseEntity<ProductId>
	{
		public string Name { get; private set; }
		public string Description { get; private set; }
		public string ShortDescription { get; private set; }
		public string Sku { get; private set; }
		public Money Price { get; private set; }
		public InventoryRecord Inventory { get; private set; }

		private List<ProductColor> productColors;

		[NotMapped]
		public ReadOnlyCollection<ProductColor> ProductColors => productColors.AsReadOnly();

		// Ef Core
		protected Product()
		{

		}

		public Product(string name, string description, string shortDescription, string sku, Money price, InventoryRecord inventory, List<ProductColor>? productColors = null)
		{
			Guard.Against.NullOrWhiteSpace(name, nameof(name));
			Guard.Against.NullOrWhiteSpace(description, nameof(description));
			Guard.Against.NullOrWhiteSpace(shortDescription, nameof(shortDescription));
			Guard.Against.NullOrWhiteSpace(sku, nameof(sku));
			Guard.Against.Null(price, nameof(price));
			Guard.Against.Null(inventory, nameof(inventory));

			Name = name;
			Description = description;
			ShortDescription = shortDescription;
			Sku = sku;
			Price = price;
			Inventory = inventory;

			this.productColors = productColors ?? new();
		}

		#region API

		public bool HasColors() => ProductColors.Any();

		public void AddOrUpdateColor(ProductColor productColor)
		{
			Guard.Against.Null(productColor, nameof(productColor));

			var color = productColors.FirstOrDefault(c => c == productColor);

			if (color != null)
			{
				productColors.Remove(color);
			}

			productColors.Add(productColor);
		}

		public void RemoveColor(ProductColor productColor)
		{
			Guard.Against.Null(productColor, nameof(productColor));

			productColors.Remove(productColor);
		}

		#endregion
	}
}