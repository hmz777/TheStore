using System.ComponentModel;
using TheStore.Blazor.Models;

namespace TheStore.Blazor.Models.Products
{
	[DisplayName("Product.Single." + nameof(GetByIdRequest))]
	public class GetByIdRequest : RequestBase
	{
		public const string RouteName = "Products.Id";
		public const string RouteTemplate = "products/{ProductId:int}";
		public override string Route => RouteTemplate.Replace("{ProductId:int}", ProductId.ToString());

		public int ProductId { get; set; }

		public GetByIdRequest()
		{

		}

		public GetByIdRequest(int productId)
		{
			ProductId = productId;
		}
	}
}