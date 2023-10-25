using MediatR;
using TheStore.Web.Blazor.Mediator.Commands;
using TheStore.Web.Blazor.Services;

namespace TheStore.Web.Blazor.Mediator.Handlers
{
	public class AddItemToCartHandler : IRequestHandler<AddItemToCartCommand>
	{
		private readonly CartService cartService;

		public AddItemToCartHandler(CartService cartService)
		{
			this.cartService = cartService;
		}

		public Task Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
		{
			cartService.AddItemToCart(request.ItemId);

			return Task.CompletedTask;
		}
	}
}
