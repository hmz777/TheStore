using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product.Single." + nameof(CreateRequest))]
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

		public CreateRequest()
		{

		}

		public CreateRequest(
			int categoryId,
			string name,
			string description,
			string shortDescription,
			string sku,
			MoneyDto price,
			InventoryRecordDto inventory)
		{
			CategoryId = categoryId;
			Name = name;
			Description = description;
			ShortDescription = shortDescription;
			Sku = sku;
			Price = price;
			Inventory = inventory;
		}
	}
}