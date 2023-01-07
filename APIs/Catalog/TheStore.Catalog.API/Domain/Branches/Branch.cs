using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStore.Catalog.API.Domain;
using TheStore.Catalog.API.Domain.Images;

namespace TheStore.Catalog.API.Domain.Branches
{
	public class Branch : BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Address { get; set; }

		public Branch(string name, string description, string address)
		{
			Guard.Against.NullOrEmpty(name, nameof(name));
			Guard.Against.NullOrEmpty(description, nameof(description));
			Guard.Against.NullOrEmpty(address, nameof(address));

			Name = name;
			Description = description;
			Address = address;
		}

		public Image? Image { get; set; }
	}
}
