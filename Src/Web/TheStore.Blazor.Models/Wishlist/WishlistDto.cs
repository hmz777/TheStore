using TheStore.Blazor.Models;

namespace TheStore.Blazor.Models.Wishlist
{
	public class WishlistDto : DtoBase
	{
		public List<WishlistItemDto> Items { get; set; }
	}
}