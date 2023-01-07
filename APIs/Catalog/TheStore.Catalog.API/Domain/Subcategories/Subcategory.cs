using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStore.Catalog.API.Domain.Categories;
using TheStore.Catalog.API.Domain.Images;
using TheStore.Catalog.API.Domain.Products;

namespace TheStore.Catalog.API.Domain.Subcategories
{
	public class Subcategory : BaseEntity
	{
		public int Order { get; set; }
		public string Name { get; set; }
		public string ShortDescription { get; set; }
		public string LongDescription { get; set; }

		public Subcategory(int order, string name, string shortDescription, string longDescription)
		{
			Guard.Against.NullOrEmpty(name, nameof(name));
			Guard.Against.NullOrEmpty(shortDescription, nameof(shortDescription));
			Guard.Against.NullOrEmpty(longDescription, nameof(longDescription));

			Order = order;
			Name = name;
			ShortDescription = shortDescription;
			LongDescription = longDescription;

			Products = new HashSet<Product>();
		}

		public Image? Image { get; set; }
		public Category? Category { get; set; }
		public ICollection<Product> Products { get; set; }
	}
}
