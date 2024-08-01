using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Cart;

namespace TheStore.Web.BlazorApp.Client.Services
{
    public class EventBroker
    {
        public Action<Result<CartItemDto>>? OnItemAddedToCart;
    }
}