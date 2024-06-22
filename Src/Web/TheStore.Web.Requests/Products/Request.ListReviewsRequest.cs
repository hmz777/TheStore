namespace TheStore.Web.Requests.Products
{
	public class ListReviewsRequest : RequestBase
	{
		public const string RouteTemplate = "products/{Identifier}/reviews";
		public override string Route =>
			(RouteTemplate + "?page={Page:int}&take={Take:int}")
						 .Replace("{Identifier}", Identifier)
						 .Replace("{Page:int}", Page.ToString())
						 .Replace("{Take:int}", Take.ToString());

		public string Identifier { get; set; }
		public int Take { get; set; }
		public int Page { get; set; }
	}
}