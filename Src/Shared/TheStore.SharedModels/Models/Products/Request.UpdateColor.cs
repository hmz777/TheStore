using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product.Single." + nameof(UpdateColorRequest))]
	public class UpdateColorRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}/colors/{ColorCode}";
		public override string Route =>
			RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString())
			.Replace("{ColorCode}", ColorCode);

		[FromRoute(Name = nameof(ProductId))]
		public int ProductId { get; set; }

		[FromRoute(Name = nameof(ColorCode))]
		public string ColorCode { get; set; }

		[FromBody]
		public UpdateProductColorDto Color { get; set; }

		public UpdateColorRequest()
		{

		}

		public UpdateColorRequest(int productId, string colorCode, UpdateProductColorDto color)
		{
			ProductId = productId;
			ColorCode = colorCode;
			Color = color;
		}
	}
}