using Ardalis.GuardClauses;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.SharedKernel.Entities;
using TheStore.SharedKernel.Interfaces;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Catalog.Core.Aggregates.Categories
{
	public class Category : BaseEntity<CategoryId>, IAggregateRoot, ISyncableAggregate
	{
		public int Order { get; set; }
		public MultilanguageString Name { get; set; }
		public bool Published { get; set; }
		public bool NeedsSynchronization { get; set; }

		public List<Product> Products { get; set; }

		// EF Core
		private Category() { }

		public Category(int order, MultilanguageString name, bool published)
		{
			Guard.Against.NegativeOrZero(order, nameof(order));
			Guard.Against.Null(name, nameof(name));

			Order = order;
			Name = name;
			Published = published;
		}
	}
}