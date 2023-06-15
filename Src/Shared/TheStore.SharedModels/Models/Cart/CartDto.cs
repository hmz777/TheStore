namespace TheStore.SharedModels.Models.Cart
{
	public class CartDto : DtoBase
	{
		public List<CartItemDto> Items { get; set; }
	}
}