using Ardalis.GuardClauses;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.SharedKernel.Entities;
using TheStore.SharedKernel.Interfaces;

namespace TheStore.Catalog.Core.Aggregates.Categories
{
	public class Category : BaseEntity<CategoryId>, IAggregateRoot, ISyncableAggregate
	{
		public int Order { get; set; }
		public string Name { get; set; }
		public bool Active { get; set; }
		public bool NeedsSynchronization { get; set; }

		private Category()
		{

		}

		public Category(int order, string name, bool active)
		{
			Guard.Against.NegativeOrZero(order, nameof(order));
			Guard.Against.NullOrEmpty(name, nameof(name));

			Order = order;
			Name = name;
			Active = active;
		}
	}
}