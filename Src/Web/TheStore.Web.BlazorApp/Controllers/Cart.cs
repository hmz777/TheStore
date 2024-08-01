using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TheStore.Requests.Models.Cart;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Cart;
using TheStore.Web.BlazorApp.Client.Mediator.Commands;
using TheStore.Web.BlazorApp.Client.Mediator.Queries;

namespace TheStore.Web.BlazorApp.Controllers
{
    [ApiController]
    public class Cart(IMediator mediator) : ControllerBase
    {
        [Route(GetUserCartRequest.RouteTemplate)]
        [HttpGet]
        public async Task<Result<CartDto>> GetUserCart()
        {
            return await mediator.Send(new GetCartQuery());
        }

        [Route(AddToCartRequest.RouteTemplate)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Result> AddItemToCart([Required] string sku)
        {
            return await mediator.Send(new AddItemToCartCommand(sku));
        }

        [Route(RemoveFromCartRequest.RouteTemplate)]
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<Result> RemoveItemFromCart([Required] string sku)
        {
            return await mediator.Send(new RemoveItemFromCartCommand(sku));
        }

        // TODO: Temporary route, add a proper request/response
        [Route("/checkout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Result> Checkout()
        {
            return await mediator.Send(new CheckoutCommand());
        }
    }
}