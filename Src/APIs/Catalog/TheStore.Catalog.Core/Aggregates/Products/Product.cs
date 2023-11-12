using Ardalis.GuardClauses;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using TheStore.Catalog.Core.Exceptions;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.SharedKernel.Entities;
using TheStore.SharedKernel.Interfaces;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Catalog.Core.Aggregates.Products
{
	public class Product : BaseEntity<ProductId>, IAggregateRoot
	{
		private readonly List<ProductVariant> variants = new();

		public CategoryId CategoryId { get; set; }
		public string Name { get; set; }
		public MultilanguageString ShortDescription { get; set; }
		public MultilanguageString Description { get; set; }
		public bool Published { get; set; }

		[NotMapped]
		public ReadOnlyCollection<ProductVariant> Variants => variants.AsReadOnly();

		public Product(
			CategoryId categoryId,
			string name,
			MultilanguageString shortDescription,
			MultilanguageString description,
			bool published,
			List<ProductVariant> variants = null!)
		{
			Guard.Against.Null(categoryId, nameof(categoryId));
			Guard.Against.NegativeOrZero(categoryId.Id, nameof(categoryId.Id));
			Guard.Against.NullOrEmpty(name, nameof(name));
			Guard.Against.Null(shortDescription, nameof(shortDescription));
			Guard.Against.Null(description, nameof(description));

			CategoryId = categoryId;
			Name = name;
			ShortDescription = shortDescription;
			Description = description;
			Published = published;
			this.variants = variants ?? new();
		}

		#region API

		public bool HasVariants => Variants.Any();

		public void AddVariant(ProductVariant productVariant)
		{
			Guard.Against.Null(productVariant, nameof(productVariant));

			if (variants.Any(c => c == productVariant))
			{
				throw new ColorAlreadyExistsException();
			}

			variants.Add(productVariant);
		}

		public void RemoveVariant(ProductVariant productVariant)
		{
			Guard.Against.Null(productVariant, nameof(productVariant));

			variants.Remove(productVariant);
		}

		#endregion
	}
}