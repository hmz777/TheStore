using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Products
{
	public class CreateRequest : RequestBase
	{
		public const string RouteTemplate = "products/singleproducts";
		public override string Route => RouteTemplate;

		public int CategoryId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ShortDescription { get; set; }
		public string Sku { get; set; }
		public MoneyDto Price { get; set; }
		public InventoryRecordDto Inventory { get; set; }
		//	public List<ProductColorDto> ProductColors { get; set; }
	}
}
