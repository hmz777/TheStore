using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Products;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Catalog.Core.Aggregates.Products
{
	public class ProductVariant : ValueObject
	{
		private List<ProductReview> reviews = new();

		[NotMapped]
		public ReadOnlyCollection<ProductReview> Reviews => reviews.AsReadOnly();

		public string Name { get; set; }
		public string Sku { get; }
		public MultilanguageString Description { get; }
		public MultilanguageString ShortDescription { get; }
		public Money Price { get; }
		public InventoryRecord Inventory { get; }
		public ProductColor Color { get; }
		public VariantOptions Options { get; set; }
		public Dimentions Dimentions { get; }
		public ProductSpecifications Sepcifications { get; }

		public ProductVariant(
			string name,
			string sku,
			MultilanguageString description,
			MultilanguageString shortDescription,
			Money price,
			InventoryRecord inventory,
			ProductColor color,
			VariantOptions options,
			Dimentions dimentions,
			ProductSpecifications sepcifications,
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
			this.reviews = reviews ?? new();
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return Sku;
		}
	}
}