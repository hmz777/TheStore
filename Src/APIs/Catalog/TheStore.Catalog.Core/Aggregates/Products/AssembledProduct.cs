using Ardalis.GuardClauses;
using System.Collections.ObjectModel;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.SharedKernel.Entities;
using TheStore.SharedKernel.Interfaces;

namespace TheStore.Catalog.Core.Aggregates.Products
{
	public class AssembledProduct : BaseEntity<AssembledProductId>, IAggregateRoot
	{
		private List<ProductId> parts = new();
		public ReadOnlyCollection<ProductId> Parts => parts.AsReadOnly();

		public CategoryId CategoryId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ShortDescription { get; set; }
		public string Sku { get; set; }

		// Ef Core
		private AssembledProduct()
		{

		}

		public AssembledProduct(
			CategoryId categoryId,
			string name,
			string description,
			string shortDescription,
			string sku)
		{
			Guard.Against.Null(categoryId, nameof(categoryId));
			Guard.Against.NullOrWhiteSpace(name, nameof(name));
			Guard.Against.NullOrWhiteSpace(description, nameof(description));
			Guard.Against.NullOrWhiteSpace(shortDescription, nameof(shortDescription));
			Guard.Against.NullOrWhiteSpace(sku, nameof(sku));

			CategoryId = categoryId;
			Name = name;
			Description = description;
			ShortDescription = shortDescription;
			Sku = sku;
		}

		public AssembledProduct(
			CategoryId categoryId,
			string name,
			string description,
			string shortDescription,
			string sku,
			List<ProductId> parts)
			: this(
				  categoryId,
				  name,
				  description,
				  shortDescription,
				  sku)
		{
			this.parts = parts ?? new();
		}

		public void AddPart(ProductId productId)
		{
			Guard.Against.Null(productId, nameof(productId));

			if (parts.Contains(productId))
			{
				return;
			}

			parts.Add(productId);
		}

		public bool RemovePart(ProductId productId)
		{
			Guard.Against.Null(productId, nameof(productId));

			return parts.Remove(productId);
		}
	}
}