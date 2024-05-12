using System.ComponentModel;
using TheStore.Requests;

namespace TheStore.Requests.Models.Products
{
	[DisplayName("Product." + nameof(ListRequest))]
	public class ListRequest : RequestBase
	{
		public const string RouteTemplate = "products";

		public override string Route
			=> (RouteTemplate + "?page={Page:int}&take={Take:int}").Replace("{Page:int}", Page.ToString()).Replace("{Take:int}", Take.ToString());

		public int Page { get; set; }
		public int Take { get; set; }
	}
}
