using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product.Single." + nameof(RemoveColorRequest))]
	public class RemoveColorRequest : RequestBase
	{
		public const string RouteTemplate = "products/singleproducts/{ProductId:int}/colors/{ColorCode}";
		public override string Route =>
			RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString())
			.Replace("{ColorCode}", ColorCode);

		[FromRoute(Name = nameof(ProductId))]
		public int ProductId { get; set; }

		[FromRoute(Name = nameof(ColorCode))]
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