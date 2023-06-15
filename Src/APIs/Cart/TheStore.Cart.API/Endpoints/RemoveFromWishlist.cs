using Ardalis.ApiEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Cart.Core.Aggregates;
using TheStore.Cart.Core.ValueObjects.Keys;
using TheStore.Cart.Infrastructure.Data;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Wishlist;

namespace TheStore.Cart.API.Endpoints
{
	public class RemoveFromWishlist : EndpointBaseAsync
		.WithRequest<RemoveFromWishlistRequest>
		.WithActionResult
	{
		private readonly IValidator<RemoveFromWishlistRequest> validator;
		private readonly IApiRepository<CartDbContext, Wishlist> apiRepository;
		private readonly Serilog.ILogger log = Log.ForContext<RemoveFromWishlist>();

		public RemoveFromWishlist(
			IValidator<RemoveFromWishlistRequest> validator,
			IApiRepository<CartDbContext, Wishlist> apiRepository)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
		}

		[HttpDelete(RemoveFromWishlistRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[SwaggerOperation(
		   Summary = "Removes an item from wishlist",
		   Description = "Removes an item from wishlist",
		   OperationId = "Wishlist.Items.Remove",
		   Tags = new[] { "Wishlists" })]
		public async override Task<ActionResult> HandleAsync(
			RemoveFromWishlistRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var wishlist = await apiRepository
				.GetByIdAsync(request.WishlistId, cancellationToken);

			if (wishlist == null)
				return NotFound("Wishlist not found");

			var wishlistItem = wishlist.Items
				.FirstOrDefault(ci => ci.Id == new WishlistItemId(request.ItemId));

			if (wishlistItem == null)
				return NotFound("Wishlist item not found");

			wishlist.RemoveItem(wishlistItem);

			await apiRepository.SaveChangesAsync(cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Remove item with id: {ItemId} and product id: {ProductId} from wishlist with id: {WishlistId}",
					request.ItemId, wishlistItem.ProductId, request.WishlistId);

			return NoContent();
		}
	}
}
