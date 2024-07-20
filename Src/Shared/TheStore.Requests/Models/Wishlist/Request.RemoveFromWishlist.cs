using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TheStore.Requests.Models.Wishlist
{
	[DisplayName("Wishlist." + nameof(RemoveFromWishlistRequest))]
	public class RemoveFromWishlistRequest : RequestBase
	{
		public const string RouteTemplate = "wishlist/{WishlistId}/{Sku}";
		public override string Route => RouteTemplate
			.Replace("{WishlistId}", WishlistId.ToString())
			.Replace("{Sku}", Sku);

		[FromRoute(Name = nameof(WishlistId))]
		public Guid WishlistId { get; set; }

		[FromRoute(Name = nameof(Sku))]
		public string Sku { get; set; }

		public RemoveFromWishlistRequest() { }

		public RemoveFromWishlistRequest(Guid wishlistId, string sku)
		{
			WishlistId = wishlistId;
			Sku = sku;
		}
	}
}