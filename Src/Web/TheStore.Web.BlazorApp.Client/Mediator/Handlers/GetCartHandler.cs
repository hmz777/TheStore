using MediatR;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Cart;
using TheStore.Web.BlazorApp.Client.Mediator.Queries;
using TheStore.Web.BlazorApp.Client.Services;

namespace TheStore.Web.BlazorApp.Client.Mediator.Handlers
{
    public class GetCartHandler : IRequestHandler<GetCartQuery, Result<CartDto>>
    {
        private readonly CartService cartService;

        public GetCartHandler(CartService cartService)
        {
            this.cartService = cartService;
        }

        public Task<Result<CartDto>> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            return cartService.GetCart(cancellationToken);
        }
    }
}