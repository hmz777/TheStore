using Ardalis.GuardClauses;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Products;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Catalog.Core.Aggregates.Products
{
	public class ProductVariant
	{
		private List<ProductReview> reviews = new();

		[NotMapped]
		public ReadOnlyCollection<ProductReview> Reviews => reviews.AsReadOnly();

		public string Name { get; set; }
		public string Sku { get; set; }
		public MultilanguageString Description { get; set; }
		public MultilanguageString ShortDescription { get; set; }
		public Money Price { get; set; }
		public InventoryRecord Inventory { get; set; }
		public ProductColor Color { get; set; }
		public ProductVariantOptions Options { get; set; }
		public Dimensions Dimentions { get; set; }
		public ProductSpecifications Sepcifications { get; set; }
		public bool Published { get; set; }

		// EF Core
		public ProductVariant() { }

		public ProductVariant(
			string name,
			string sku,
			MultilanguageString description,
			MultilanguageString shortDescription,
			Money price,
			InventoryRecord inventory,
			ProductColor color,
			ProductVariantOptions options,
			Dimensions dimentions,
			ProductSpecifications sepcifications,
			bool published,
			List<ProductReview> reviews = null!)
		{
			Guard.Against.NullOrEmpty(sku, nameof(sku));
			Guard.Against.Null(description, nameof(description));
			Guard.Against.Null(shortDescription, nameof(shortDescription));
			Guard.Against.Null(price, nameof(price));
			Guard.Against.Null(inventory, nameof(inventory));
			Guard.Against.Null(color, nameof(color));
			Guard.Against.Null(options, nameof(options));
			Guard.Against.Null(sepcifications, nameof(sepcifications));

			Name = name;
			Sku = sku;
			Description = description;
			ShortDescription = shortDescription;
			Price = price;
			Inventory = inventory;
			Color = color;
			Options = options;
			Dimentions = dimentions;
			Sepcifications = sepcifications;
			Published = published;
			this.reviews = reviews ?? new();
		}
	}
}