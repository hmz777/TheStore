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

		public int ProductId { get; set; }
		public int ProductColorId { get; set; }
	}
}