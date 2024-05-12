using System.ComponentModel;
using TheStore.Web.Requests;

namespace TheStore.Web.Requests.Categories
{
	[DisplayName("Category." + nameof(ListRequest))]
	public class ListRequest : RequestBase
	{
		public const string RouteTemplate = "categories";
		public override string Route
			=> (RouteTemplate + "?page={Page:int}&take={Take:int}").Replace("{Page:int}", Page.ToString()).Replace("{Take:int}", Take.ToString());

		public int Page { get; set; }

		public int Take { get; set; }
	}
}