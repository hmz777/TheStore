﻿using System.ComponentModel;
using TheStore.Web.Requests;

namespace TheStore.Web.Requests.Wishlist
{
	[DisplayName("Wishlist." + nameof(RemoveFromWishlistRequest))]
	public class RemoveFromWishlistRequest : RequestBase
	{
		public const string RouteTemplate = "wishlist/{WishlistId}/{ProductId:int}";
		public override string Route => RouteTemplate
			.Replace("{WishlistId}", WishlistId.ToString())
			.Replace("{ProductId:int}", ProductId.ToString());


		public Guid WishlistId { get; set; }


		public int ProductId { get; set; }

		public RemoveFromWishlistRequest()
		{

		}

		public RemoveFromWishlistRequest(Guid wishlistId, int productId)
		{
			WishlistId = wishlistId;
			ProductId = productId;
		}
	}
}