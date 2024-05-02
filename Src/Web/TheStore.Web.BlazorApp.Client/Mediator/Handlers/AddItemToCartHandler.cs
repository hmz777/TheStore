using MediatR;
using TheStore.Web.BlazorApp.Client.Mediator.Commands;
using TheStore.Web.BlazorApp.Client.Services;

namespace TheStore.Web.BlazorApp.Client.Mediator.Handlers
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
