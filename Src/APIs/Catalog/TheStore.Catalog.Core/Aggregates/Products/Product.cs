using Ardalis.GuardClauses;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using TheStore.Catalog.Core.Exceptions;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.SharedKernel.Entities;
using TheStore.SharedKernel.Interfaces;

namespace TheStore.Catalog.Core.Aggregates.Products
{
	public class Product : BaseEntity<ProductId>, IAggregateRoot
	{
		private readonly List<ProductVariant> variants = new();

		public CategoryId CategoryId { get; set; }
		public string Name { get; set; }
		public bool Published { get; set; }

		[NotMapped]
		public ReadOnlyCollection<ProductVariant> Variants => variants.AsReadOnly();

		public Product(CategoryId categoryId, string name, bool published, List<ProductVariant> variants = null!)
		{
			Guard.Against.Null(categoryId, nameof(categoryId));
			Guard.Against.NullOrEmpty(name, nameof(name));

			CategoryId = categoryId;
			Name = name;
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