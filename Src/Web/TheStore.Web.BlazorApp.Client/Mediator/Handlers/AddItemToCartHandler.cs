using MediatR;
using TheStore.SharedModels.Models;
using TheStore.Web.BlazorApp.Client.Mediator.Commands;
using TheStore.Web.BlazorApp.Client.Services;

namespace TheStore.Web.BlazorApp.Client.Mediator.Handlers
{
    public class AddItemToCartHandler(CartService cartService) : IRequestHandler<AddItemToCartCommand, Result>
    {
        public Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
        {
            return cartService.AddItemToCart(request.Sku, cancellationToken);
        }
    }
}