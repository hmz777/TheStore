using Ardalis.GuardClauses;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.SharedKernel.Entities;
using TheStore.SharedKernel.Interfaces;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Catalog.Core.Aggregates.Products
{
	public class AssembledProduct : BaseEntity<int>, IAggregateRoot
	{
		private Dictionary<ProductId, string> parts = new();

		[NotMapped]
		public ReadOnlyDictionary<ProductId, string> Parts => parts.AsReadOnly();

		public CategoryId CategoryId { get; set; }
		public string Name { get; set; }
		public MultilanguageString ShortDescription { get; set; }
		public MultilanguageString Description { get; set; }
		public bool Published { get; set; }

		// Ef Core
		private AssembledProduct() { }

		public AssembledProduct(
			CategoryId categoryId,
			string name,
			MultilanguageString shortDescription,
			MultilanguageString description,
			bool published)
		{
			CategoryId = categoryId;
			Name = name;
			ShortDescription = shortDescription;
			Description = description;
			Published = published;
			this.parts = parts ?? new();
		}

		public void AddPart(ProductId productId, string sku)
		{
			Guard.Against.Null(productId, nameof(productId));
			Guard.Against.NullOrEmpty(sku, nameof(sku));

			var part = KeyValuePair.Create(productId, sku);

			if (parts.Contains(part))
			{
				return;
			}

			parts.Add(part.Key, part.Value);
		}

		public bool RemovePart(ProductId productId)
		{
			Guard.Against.Null(productId, nameof(productId));

			return parts.Remove(productId);
		}

		public bool RemovePart(string sku)
		{
			Guard.Against.NullOrEmpty(sku, nameof(sku));

			var part = parts.Where(p => p.Value == sku).FirstOrDefault();

			if (part.Equals(default(KeyValuePair<ProductId, string>)) == false)
			{
				return parts.Remove(part.Key);
			}

			return false;
		}
	}
}