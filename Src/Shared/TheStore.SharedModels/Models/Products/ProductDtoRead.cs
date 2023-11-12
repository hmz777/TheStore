using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Products
{
    [DisplayName(nameof(ProductDtoRead))]
	public class ProductDtoRead : DtoBase
	{
		public int ProductId { get; set; }
		public int CategoryId { get; set; }
		public string Name { get; set; }
		public MultilanguageStringDto ShortDescription { get; set; }
		public MultilanguageStringDto Description { get; set; }
		public List<ProductVariantDtoRead> Variants { get; set; }
        public bool Published { get; set; }
    }
}