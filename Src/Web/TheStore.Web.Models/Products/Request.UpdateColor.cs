using System.ComponentModel;
using TheStore.Web.Models;
using TheStore.Web.Models.ValueObjectsDtos;

namespace TheStore.Web.Models.Products
{
    [DisplayName("Product.Single." + nameof(UpdateColorRequest))]
	public class UpdateColorRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}/colors/{ColorCode}";
		public override string Route =>
			RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString())
			.Replace("{ColorCode}", ColorCode);

		public int ProductId { get; set; }

		public string ColorCode { get; set; }

		public UpdateProductColorDto Color { get; set; }

		public InventoryRecordDto InventoryRecord { get; set; }

		public UpdateColorRequest() { }

		public UpdateColorRequest(int productId, string colorCode, UpdateProductColorDto color, InventoryRecordDto inventoryRecord)
		{
			ProductId = productId;
			ColorCode = colorCode;
			Color = color;
			InventoryRecord = inventoryRecord;
		}
	}
}