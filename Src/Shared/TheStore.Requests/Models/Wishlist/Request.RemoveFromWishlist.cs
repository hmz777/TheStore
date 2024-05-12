using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TheStore.Requests.Models.Wishlist
{
	[DisplayName("Wishlist." + nameof(RemoveFromWishlistRequest))]
	public class RemoveFromWishlistRequest : RequestBase
	{
		public const string RouteTemplate = "wishlist/{WishlistId}/{ProductId:int}";
		public override string Route => RouteTemplate
			.Replace("{WishlistId}", WishlistId.ToString())
			.Replace("{ProductId:int}", ProductId.ToString());

		[FromRoute(Name = nameof(WishlistId))]
		public Guid WishlistId { get; set; }

		[FromRoute(Name = nameof(ProductId))]
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