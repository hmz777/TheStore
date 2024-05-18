namespace TheStore.Requests.Models.Products
{
	public class ListReviewsRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}/reviews";
		public override string Route =>
			(RouteTemplate + "?page={Page:int}&take={Take:int}")
						 .Replace("{ProductId:int}", ProductId.ToString())
						 .Replace("{Page:int}", Page.ToString())
						 .Replace("{Take:int}", Take.ToString());

		public int ProductId { get; set; }
		public int Take { get; set; }
		public int Page { get; set; }
	}
}