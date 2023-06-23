using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product.Single." + nameof(UpdateRequest))]
	public class UpdateRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}";
		internal override string Route => RouteTemplate.Replace("{ProductId:int}", ProductId.ToString());

		public int ProductId { get; set; }
		public int CategoryId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ShortDescription { get; set; }
		public string Sku { get; set; }
		public MoneyDto Price { get; set; }
		public InventoryRecordDto Inventory { get; set; }

		public UpdateRequest()
		{

		}

		public UpdateRequest(
			int productId,
			int categoryId,
			string name,
			string description,
			string shortDescription,
			string sku,
			MoneyDto price,
			InventoryRecordDto inventory)
		{
			ProductId = productId;
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