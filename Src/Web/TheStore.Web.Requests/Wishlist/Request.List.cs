﻿using System.ComponentModel;
using TheStore.Web.Requests;

namespace TheStore.Web.Requests.Wishlist
{
	[DisplayName("Wishlist." + nameof(ListRequest))]
	public class ListRequest : RequestBase
	{
		public const string RouteTemplate = "wishlists";
		public override string Route
		=> (RouteTemplate + "?page={Page:int}&take={Take:int}").Replace("{Page:int}", Page.ToString()).Replace("{Take:int}", Take.ToString());

		public int Page { get; set; } = 1;
		public int Take { get; set; } = 12;

		public ListRequest()
		{

		}

		public ListRequest(int page, int take)
		{
			Page = page;
			Take = take;
		}
	}
}