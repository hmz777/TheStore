using System.ComponentModel;

namespace TheStore.Blazor.Models.Wishlist
{
	[DisplayName("Wishlist." + nameof(GetWishlistByIdRequest))]
	public class GetWishlistByIdRequest : RequestBase
	{
		public const string RouteName = "Wishlists.Id";
		public const string RouteTemplate = "wishlist/{WishlistId}";
		public override string Route => RouteTemplate.Replace("{WishlistId}", WishlistId.ToString());

		public Guid WishlistId { get; set; }
	}
}