using Ardalis.GuardClauses;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.SharedKernel.Entities;
using TheStore.SharedKernel.Interfaces;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Catalog.Core.Aggregates.Branches
{
	public class Branch : BaseEntity<int>, IAggregateRoot
	{
		public MultilanguageString Name { get; set; }
		public MultilanguageString Description { get; set; }
		public Address Address { get; set; }
		public Image Image { get; set; }
		public bool Published { get; set; }

		// Ef Core
		private Branch() { }

		public Branch(MultilanguageString name, MultilanguageString description, Address address, bool published)
		{
			Guard.Against.Null(name, nameof(name));
			Guard.Against.Null(description, nameof(description));
			Guard.Against.Null(address, nameof(address));

			Name = name;
			Description = description;
			Address = address;
			Published = published;
		}
	}
}