using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product.Single." + nameof(RemoveColorRequest))]
	public class RemoveColorRequest : RequestBase
	{
		public const string RouteTemplate = "products/singleproducts/{ProductId:int}/colors/{ProductColorId:int}";
		public override string Route =>
			RouteTemplate.Replace("{ProductId:int}", ProductId.ToString())
			.Replace("{ProductColorId:int}", ProductColorId.ToString());

		[FromRoute(Name = nameof(ProductId))]
		public int ProductId { get; set; }

		[FromRoute(Name = nameof(ProductColorId))]
		public int ProductColorId { get; set; }

		public RemoveColorRequest()
		{

		}
		public RemoveColorRequest(int productId, int productColorId)
		{
			ProductId = productId;
			ProductColorId = productColorId;
		}
	}
}