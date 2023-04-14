using System.ComponentModel;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product.Single." + nameof(GetByIdRequest))]
	public class GetByIdRequest : RequestBase
	{
		public const string RouteName = "SingleProducts.Id";
		public const string RouteTemplate = "products/singleproducts/{ProductId:int}";
		public override string Route => RouteTemplate.Replace("{ProductId:int}", ProductId.ToString());

		public int ProductId { get; set; }
	}
}