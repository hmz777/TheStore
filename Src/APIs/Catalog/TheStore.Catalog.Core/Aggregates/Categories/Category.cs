using Ardalis.GuardClauses;
using TheStore.SharedKernel.Entities;
using TheStore.SharedKernel.Interfaces;

namespace TheStore.Catalog.API.Domain.Categories
{
	public class Category : BaseEntity<int>, IAggregateRoot, ISyncableAggregate
	{
		public int Order { get; set; }
		public string Name { get; set; }
		public bool Active { get; set; }
		public bool NeedsSynchronization { get; set; }

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