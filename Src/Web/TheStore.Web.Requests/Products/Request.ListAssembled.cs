using System.ComponentModel;

namespace TheStore.Web.Requests.Products
{
	[DisplayName("Product.Assembled." + nameof(ListAssembledRequest))]
	public class ListAssembledRequest : RequestBase
	{
		public const string RouteTemplate = "products/assembledproducts";

		public override string Route
			=> (RouteTemplate + "?page={Page:int}&take={Take:int}").Replace("{Page:int}", Page.ToString()).Replace("{Take:int}", Take.ToString());

		public int Page { get; set; }
		public int Take { get; set; }

		public ListAssembledRequest(int page, int take)
		{
			Page = page;
			Take = take;
		}
	}
}
