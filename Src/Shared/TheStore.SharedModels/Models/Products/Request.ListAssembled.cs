using System.ComponentModel;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product.Assembled." + nameof(ListAssembledRequest))]
	public class ListAssembledRequest : RequestBase
	{
		public const string RouteTemplate = "products/singleproducts";

		public override string Route
			=> (RouteTemplate + "?page={Page:int}&take={Take:int}").Replace("{Page:int}", Page.ToString()).Replace("{Take:int}", Take.ToString());

		public int Page { get; set; } = 1;
		public int Take { get; set; } = 12;

		public ListAssembledRequest()
		{

		}

		public ListAssembledRequest(int page, int take)
		{
			Page = page;
			Take = take;
		}
	}
}
