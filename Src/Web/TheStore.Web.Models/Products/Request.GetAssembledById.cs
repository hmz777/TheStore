using System.ComponentModel;
using TheStore.Web.Models;

namespace TheStore.Web.Models.Products
{
	[DisplayName("Product.Assembled." + nameof(GetAssembledByIdRequest))]
	public class GetAssembledByIdRequest : RequestBase
	{
		public const string RouteName = "AssembledProducts.Id";
		public const string RouteTemplate = "products/assembledproducts/{ProductId:int}";
		public override string Route => RouteTemplate.Replace("{ProductId:int}", ProductId.ToString());

		public int ProductId { get; set; }

		public GetAssembledByIdRequest() { }

		public GetAssembledByIdRequest(int productId)
		{
			ProductId = productId;
		}
	}
}