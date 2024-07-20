using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TheStore.Requests.Models.Wishlist
{
	[DisplayName("Wishlist." + nameof(AddToWishlistRequest))]
	public class AddToWishlistRequest : RequestBase
	{
		public const string RouteTemplate = "wishlist/{WishlistId}";
		public override string Route => RouteTemplate.Replace("{WishlistId}", WishlistId.ToString());

		[FromRoute(Name = nameof(WishlistId))]
		public Guid WishlistId { get; set; }

		[FromBody]
		public string Sku { get; set; }

		public AddToWishlistRequest() { }

		public AddToWishlistRequest(Guid wishlistId, string sku)
		{
			WishlistId = wishlistId;
			Sku = sku;
		}
	}
}