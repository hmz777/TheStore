using Ardalis.GuardClauses;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.SharedKernel.Entities;
using TheStore.SharedKernel.Interfaces;

namespace TheStore.Catalog.API.Domain.Branches
{
	public class Branch : BaseEntity<int>, IAggregateRoot
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Address { get; set; }

		public Branch(string name, string description, string address, Image image)
		{
			Guard.Against.NullOrEmpty(name, nameof(name));
			Guard.Against.NullOrEmpty(description, nameof(description));
			Guard.Against.NullOrEmpty(address, nameof(address));
			Guard.Against.Null(image, nameof(image));

			Name = name;
			Description = description;
			Address = address;
			Image = image;
		}

		public Image Image { get; set; }
	}
}