using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName(nameof(ProductVariantDetailsDtoRead))]
	public class ProductVariantDetailsDtoRead : DtoBase
	{
		public string Name { get; set; }
		public string Sku { get; set; }
		public MultilanguageStringDto Description { get; set; }
		public MultilanguageStringDto ShortDescription { get; set; }
		public MoneyDto Price { get; set; }
		public InventoryRecordDto Inventory { get; set; }
		public ProductColorDtoRead Color { get; set; }
		public ProductVariantOptionsDto Options { get; set; }
		public DimentionsDto Dimentions { get; set; }
		public List<ProductSpecificationDto> Sepcifications { get; set; }
	}
}