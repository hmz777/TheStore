using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TheStore.SharedModels.Models.Categories
{
	[DisplayName("Category." + nameof(ListRequest))]
	public class ListRequest : RequestBase
	{
		public const string RouteTemplate = "categories";
		public override string Route
			=> (RouteTemplate + "?page={Page:int}&take={Take:int}").Replace("{Page:int}", Page.ToString()).Replace("{Take:int}", Take.ToString());

		[FromQuery]
		public int Page { get; set; }

		[FromQuery]
		public int Take { get; set; }

		public ListRequest(int page, int take)
		{
			Page = page;
			Take = take;
		}
	}
}