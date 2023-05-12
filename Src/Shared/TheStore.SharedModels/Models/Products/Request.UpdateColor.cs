using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product.Single." + nameof(UpdateColorRequest))]
	public class UpdateColorRequest : RequestBase
	{
		public const string RouteTemplate = "products/singleproducts/{ProductId:int}/colors/{ProductColorId:int}";
		public override string Route =>
			RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString())
			.Replace("{ProductColorId:int}", ProductColorId.ToString());

		[FromRoute(Name = nameof(ProductId))]
		public int ProductId { get; set; }

		[FromRoute(Name = nameof(ProductColorId))]
		public int ProductColorId { get; set; }

		[FromBody]
		public UpdateProductColorDto Color { get; set; }

		public UpdateColorRequest()
		{

		}

		public UpdateColorRequest(int productId, UpdateProductColorDto color)
		{
			ProductId = productId;
			Color = color;
		}
	}
}