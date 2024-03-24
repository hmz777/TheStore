namespace TheStore.Web.Models.Cart
{
	public class CartDto : DtoBase
	{
		public List<CartItemDto> Items { get; set; }
	}
}