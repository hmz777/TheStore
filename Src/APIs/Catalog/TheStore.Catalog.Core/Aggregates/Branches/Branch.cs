using Ardalis.GuardClauses;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.SharedKernel.Entities;
using TheStore.SharedKernel.Interfaces;

namespace TheStore.Catalog.Core.Aggregates.Branches
{
	public class Branch : BaseEntity<int>, IAggregateRoot
	{
		public string Name { get; private set; }
		public string Description { get; private set; }
		public Address Address { get; private set; }
		public Image Image { get; private set; }

		// Ef Core
		private Branch()
		{

		}

		public Branch(string name, string description, Address address, Image image)
		{
			Guard.Against.NullOrEmpty(name, nameof(name));
			Guard.Against.NullOrEmpty(description, nameof(description));
			Guard.Against.Null(address, nameof(address));
			Guard.Against.Null(image, nameof(image));

			Name = name;
			Description = description;
			Address = address;
			Image = image;
		}
	}
}