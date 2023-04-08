namespace TheStore.SharedModels.Models.Products
{
	public class ListRequest : RequestBase
	{
		public const string RouteTemplate = "products/singleproducts";

		public override string Route => RouteTemplate;

		public int Page { get; set; } = 1;
		public int Take { get; set; } = 12;
	}
}
