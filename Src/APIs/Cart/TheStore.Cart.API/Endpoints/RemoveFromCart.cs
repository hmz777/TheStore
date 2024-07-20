using Ardalis.ApiEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Cart.Infrastructure.Data;
using TheStore.Requests;
using TheStore.Requests.Models.Cart;

namespace TheStore.Cart.API.Endpoints
{
	public class RemoveFromCart : EndpointBaseAsync
		.WithRequest<RemoveFromCartRequest>
		.WithActionResult
	{
		private readonly IValidator<RemoveFromCartRequest> validator;
		private readonly IApiRepository<CartDbContext, Core.Aggregates.Cart> apiRepository;
		private readonly Serilog.ILogger log = Log.ForContext<RemoveFromCart>();

		public RemoveFromCart(
			IValidator<RemoveFromCartRequest> validator,
			IApiRepository<CartDbContext, Core.Aggregates.Cart> apiRepository)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
		}

		[HttpDelete(RemoveFromCartRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[SwaggerOperation(
		   Summary = "Removes an item from cart",
		   Description = "Removes an item from cart",
		   OperationId = "Cart.Items.Remove",
		   Tags = new[] { "Carts" })]
		public async override Task<ActionResult> HandleAsync(
			RemoveFromCartRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var cart = await apiRepository
				.GetByIdAsync(request.CartId, cancellationToken);

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
					cartItem.Sku, request.CartId);
			}

			return NoContent();
		}
	}
}