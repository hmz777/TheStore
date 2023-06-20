using Ardalis.GuardClauses;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.SharedKernel.Interfaces;

namespace TheStore.Catalog.Core.Aggregates.Products
{
	public class AssembledProduct : Product, IAggregateRoot
	{
		private List<ProductId> parts = new();

		[NotMapped]
		public ReadOnlyCollection<ProductId> Parts => parts.AsReadOnly();

		// Ef Core
		private AssembledProduct() { }

		public AssembledProduct(
			CategoryId categoryId,
			string name,
			string description,
			string shortDescription,
			string sku) : base(
				categoryId,
				name,
				description,
				shortDescription,
				sku,
				Money.ZeroUsd,
				InventoryRecord.Empty)
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