using System.ComponentModel;

namespace TheStore.Blazor.Models.Products
{
	[DisplayName("Product.Single." + nameof(RemoveColorRequest))]
	public class RemoveColorRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}/colors/{ColorCode}";
		public override string Route =>
			RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString())
			.Replace("{ColorCode}", ColorCode);

		public int ProductId { get; set; }

		public string ColorCode { get; set; }

		public RemoveColorRequest()
		{

		}

		public RemoveColorRequest(int productId, string colorCode)
		{
			ProductId = productId;
			ColorCode = colorCode;
		}
	}
}