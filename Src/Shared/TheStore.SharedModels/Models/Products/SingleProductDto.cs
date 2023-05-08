using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName(nameof(SingleProductDto))]
	public class SingleProductDto : DtoBase
	{
		public int ProductId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ShortDescription { get; set; }
		public string Sku { get; set; }
		public MoneyDto Price { get; set; }
		public InventoryRecordDto Inventory { get; set; }
		public List<ProductColorDto> ProductColors { get; set; }

		public ImageDto GetMainColor() => ProductColors.First().Images.First();
	}
}