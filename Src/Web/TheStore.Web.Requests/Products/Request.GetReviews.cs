namespace TheStore.Web.Requests.Products
{
	public class GetReviewsRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}/reviews";
		public override string Route => RouteTemplate.Replace("{ProductId:int}", ProductId.ToString());

		public int ProductId { get; set; }
	}
}