using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStore.Catalog.API.Domain.Images;
using TheStore.Catalog.API.Domain.Subcategories;

namespace TheStore.Catalog.API.Domain.Products
{
	public class Product : BaseEntity
	{
		public int Order { get; set; }
		public string Name { get; set; }
		public string ShortDescription { get; set; }
		public string LongDescription { get; set; }

		public Product(int order, string name, string shortDescription, string longDescription)
		{
			Guard.Against.NullOrEmpty(name, nameof(name));
			Guard.Against.NullOrEmpty(shortDescription, nameof(shortDescription));
			Guard.Against.NullOrEmpty(longDescription, nameof(longDescription));

			Order = order;
			Name = name;
			ShortDescription = shortDescription;
			LongDescription = longDescription;

			Images = new HashSet<Image>();
		}

		public Subcategory? Subcategory { get; set; }
		public ICollection<Image> Images { get; set; }
	}
}
