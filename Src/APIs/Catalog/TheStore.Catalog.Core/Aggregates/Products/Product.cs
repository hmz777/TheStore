using Ardalis.GuardClauses;
using TheStore.Catalog.Core.Aggregates.Categories;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.SharedKernel.Entities;
using TheStore.SharedKernel.Interfaces;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Catalog.Core.Aggregates.Products
{
	public class Product : BaseEntity<ProductId>, IAggregateRoot
	{
		public string Name { get; set; }
		public string Identifier { get; set; }
		public CategoryId CategoryId { get; set; }
		public Category Category { get; set; }
		public MultilanguageString ShortDescription { get; set; }
		public MultilanguageString Description { get; set; }
		public List<ProductVariant> Variants { get; set; }
		public List<ProductReview> Reviews { get; set; }
		public bool Published { get; set; }

		// Ef Core
		private Product() { }

		public Product(
			CategoryId categoryId,
			string name,
			string identifier,
			MultilanguageString shortDescription,
			MultilanguageString description,
			bool published,
			List<ProductVariant> variants = null!,
			List<ProductReview> reviews = null!)
		{
			Guard.Against.Null(categoryId, nameof(categoryId));
			Guard.Against.NegativeOrZero(categoryId.Id, nameof(categoryId.Id));
			Guard.Against.NullOrEmpty(name, nameof(name));
			Guard.Against.NullOrEmpty(identifier, nameof(identifier));
			Guard.Against.Null(shortDescription, nameof(shortDescription));
			Guard.Against.Null(description, nameof(description));

			CategoryId = categoryId;
			Name = name;
			Identifier = identifier;
			ShortDescription = shortDescription;
			Description = description;
			Published = published;
			Variants = variants ?? [];
			Reviews = reviews ?? [];
		}
	}
}