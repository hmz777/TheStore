using System.ComponentModel;
using TheStore.Web.Requests;

namespace TheStore.Web.Requests.Wishlist
{
	[DisplayName("Wishlist." + nameof(AddToWishlistRequest))]
	public class AddToWishlistRequest : RequestBase
	{
		public const string RouteTemplate = "wishlist/{WishlistId}";
		public override string Route => RouteTemplate.Replace("{WishlistId}", WishlistId.ToString());

		public Guid WishlistId { get; set; }

		public int ProductId { get; set; }

		public AddToWishlistRequest()
		{

		}

		public AddToWishlistRequest(Guid wishlistId, int productId)
		{
			WishlistId = wishlistId;
			ProductId = productId;
		}
	}
}