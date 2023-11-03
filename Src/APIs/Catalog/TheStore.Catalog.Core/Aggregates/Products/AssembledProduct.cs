using Ardalis.GuardClauses;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
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

		public AssembledProduct(CategoryId categoryId, string name, List<ProductId> parts = null!) : base(categoryId, name)
		{
			Guard.Against.Null(categoryId, nameof(categoryId));
			Guard.Against.NullOrWhiteSpace(name, nameof(name));

			CategoryId = categoryId;
			Name = name;
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