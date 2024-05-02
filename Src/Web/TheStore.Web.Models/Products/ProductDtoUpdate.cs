using System.ComponentModel;
using TheStore.Web.Models.ValueObjectsDtos;

namespace TheStore.Web.Models.Products
{
	[DisplayName(nameof(ProductDtoUpdate))]
	public class ProductDtoUpdate : DtoBase
	{
		public int CategoryId { get; set; }
		public string Name { get; set; }
		public MultilanguageStringDto ShortDescription { get; set; }
		public MultilanguageStringDto Description { get; set; }
		public bool Published { get; set; }
	}
}