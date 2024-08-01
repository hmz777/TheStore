using Ardalis.ApiEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Cart.Core.ValueObjects.Keys;
using TheStore.Cart.Infrastructure.Data;
using TheStore.Cart.Infrastructure.Data.Specifications;
using TheStore.Requests;
using TheStore.Requests.Models.Cart;

namespace TheStore.Cart.API.Endpoints
{
    public class RemoveFromCart(
        IValidator<RemoveFromCartRequest> validator,
        IApiRepository<CartDbContext, Core.Aggregates.Cart> apiRepository) : EndpointBaseAsync
        .WithRequest<RemoveFromCartRequest>
        .WithActionResult
    {
        // TODO: Use ILogger from DI instead of the static one
        private readonly Serilog.ILogger log = Log.ForContext<RemoveFromCart>();

        [Authorize]
        [HttpDelete(RemoveFromCartRequest.RouteTemplate)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(
           Summary = "Removes an item from cart",
           Description = "Removes an item from cart",
           OperationId = "Cart.Items.Remove",
           Tags = ["Carts"])]
        public async override Task<ActionResult> HandleAsync(
            RemoveFromCartRequest request,
            CancellationToken cancellationToken = default)
        {
            var validation = await validator.ValidateAsync(request, cancellationToken);
            if (validation.IsValid == false)
                return BadRequest(validation.AsErrors());

            var buyerId = new BuyerId(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)!));

            var cart = await apiRepository
                .FirstOrDefaultAsync(new GetCartByBuyerIdSpec(buyerId), cancellationToken);

            if (cart == null)
                return NotFound("Cart not found");

            var cartItem = cart.Items
                .FirstOrDefault(ci => ci.Sku == request.Sku);

            if (cartItem == null)
                return NotFound("Cart item not found");

            cart.RemoveItem(cartItem);
            await apiRepository.SaveChangesAsync(cancellationToken);

            using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
            {
                log.Information("Remove item with product Sku: {Sku} from cart with id: {CartId}",
                    cartItem.Sku, cart.Id);
            }

            return NoContent();
        }
    }
}