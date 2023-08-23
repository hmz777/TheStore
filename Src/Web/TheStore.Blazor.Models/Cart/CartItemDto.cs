using TheStore.Blazor.Models;

namespace TheStore.Blazor.Models.Cart
{
    public class CartItemDto : DtoBase
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}