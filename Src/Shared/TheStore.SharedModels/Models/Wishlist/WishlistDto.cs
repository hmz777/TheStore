namespace TheStore.SharedModels.Models.Wishlist
{
	public class WishlistDto : DtoBase
	{
		public List<WishlistItemDto> Items { get; set; }
	}
}