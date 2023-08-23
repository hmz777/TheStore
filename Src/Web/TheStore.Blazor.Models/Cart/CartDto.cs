using TheStore.Blazor.Models;

namespace TheStore.Blazor.Models.Cart
{
    public class CartDto : DtoBase
    {
        public List<CartItemDto> Items { get; set; }
    }
}