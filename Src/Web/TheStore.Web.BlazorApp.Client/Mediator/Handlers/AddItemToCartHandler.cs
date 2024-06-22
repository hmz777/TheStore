using MediatR;
using TheStore.Web.BlazorApp.Client.Mediator.Commands;
using TheStore.Web.BlazorApp.Client.Services;

namespace TheStore.Web.BlazorApp.Client.Mediator.Handlers
{
	public class AddItemToCartHandler(CartService cartService) : IRequestHandler<AddItemToCartCommand>
	{
		public Task Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
		{
			cartService.AddItemToCart(request.Sku);

			return Task.CompletedTask;
		}
	}
}