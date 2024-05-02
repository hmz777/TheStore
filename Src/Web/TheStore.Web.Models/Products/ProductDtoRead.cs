using System.ComponentModel;
using TheStore.Web.Models.ValueObjectsDtos;

namespace TheStore.Web.Models.Products
{
	[DisplayName(nameof(ProductDtoRead))]
	public class ProductDtoRead : DtoBase
	{
		public int ProductId { get; set; }
		public int CategoryId { get; set; }
		public string Name { get; set; }
		public MultilanguageStringDto ShortDescription { get; set; }
		public MultilanguageStringDto Description { get; set; }
		public List<ProductVariantDtoRead> VariantSkus { get; set; } = [];
		public bool Published { get; set; }
	}
}

// TODO: Maps SKUs and only the first (or main) variant for the catalog view