using Ardalis.GuardClauses;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using TheStore.Catalog.Core.Exceptions;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Core.ValueObjects.Products;
using TheStore.SharedKernel.Entities;
using TheStore.SharedKernel.Interfaces;

namespace TheStore.Catalog.Core.Aggregates.Products
{
	public class Product : BaseEntity<ProductId>, IAggregateRoot
	{
		public CategoryId CategoryId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ShortDescription { get; set; }
		public string Sku { get; set; }
		public virtual Money Price { get; set; }
		public virtual InventoryRecord Inventory { get; set; }

		private List<ProductColor> productColors = new();

		[NotMapped]
		public virtual ReadOnlyCollection<ProductColor> ProductColors => productColors.AsReadOnly();

		// Ef Core
		protected Product() { }

		public Product(
		CategoryId categoryId,
		string name,
		string description,
		string shortDescription,
		string sku,
		Money price,
		InventoryRecord inventory)
		{
			Guard.Against.Null(categoryId, nameof(categoryId));
			Guard.Against.NullOrWhiteSpace(name, nameof(name));
			Guard.Against.NullOrWhiteSpace(description, nameof(description));
			Guard.Against.NullOrWhiteSpace(shortDescription, nameof(shortDescription));
			Guard.Against.NullOrWhiteSpace(sku, nameof(sku));
			Guard.Against.Null(price, nameof(price));
			Guard.Against.Null(inventory, nameof(inventory));

			CategoryId = categoryId;
			Name = name;
			Description = description;
			ShortDescription = shortDescription;
			Sku = sku;
			Price = price;
			Inventory = inventory;
		}

		public Product(
			CategoryId categoryId,
			string name,
			string description,
			string shortDescription,
			string sku,
			Money price,
			InventoryRecord inventory,
			List<ProductColor> productColors)
			: this(
				  categoryId,
				  name,
				  description,
				  shortDescription,
				  sku,
				  price,
				  inventory)
		{
			this.productColors = productColors ?? new();
		}

		#region API

		public bool HasColors() => ProductColors.Any();

		public void AddColor(ProductColor productColor)
		{
			Guard.Against.Null(productColor, nameof(productColor));

			if (productColors.Any(c => c == productColor))
			{
				throw new ColorAlreadyExistsException();
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