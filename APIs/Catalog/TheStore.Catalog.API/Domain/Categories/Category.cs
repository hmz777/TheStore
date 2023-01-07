using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStore.Catalog.API.Domain;
using TheStore.Catalog.API.Domain.Subcategories;

namespace TheStore.Catalog.API.Domain.Categories
{
	public class Category : BaseEntity
	{
		public int Order { get; set; }
		public string Name { get; set; }

		public Category(int order, string name)
		{
			Guard.Against.NullOrEmpty(name, nameof(name));

			Order = order;
			Name = name;

			Subcategories = new HashSet<Subcategory>();
		}

		public ICollection<Subcategory> Subcategories { get; set; }
	}
}