using Ardalis.ApiEndpoints;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Cart.Core.Entities;
using TheStore.Cart.Infrastructure.Data;
using TheStore.Requests.Models.Cart;

namespace TheStore.Cart.API.Endpoints
{
	public class SaveCart : EndpointBaseAsync
		.WithRequest<SaveCartRequest>
		.WithActionResult<Core.Aggregates.Cart>
	{
		private readonly IApiRepository<CartDbContext, Core.Aggregates.Cart> repository;
		private readonly IValidator<SaveCartRequest> validator;
		private readonly IMapper mapper;

		public SaveCart(
			IApiRepository<CartDbContext, Core.Aggregates.Cart> repository,
			IValidator<SaveCartRequest> validator,
			IMapper mapper)
		{
			this.repository = repository;
			this.validator = validator;
			this.mapper = mapper;
		}

		[HttpPost(SaveCartRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async override Task<ActionResult<Core.Aggregates.Cart>> HandleAsync(
			SaveCartRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var cart = await repository
				.GetByIdAsync(request.CartId, cancellationToken);

			if (cart == null)
				return NotFound("Cart not found");

			var cartItems = mapper.Map<List<CartItem>>(request.CartItems);

			// TODO: Now we completely replace the cart items, later we could use a different strategy

			foreach (var cartItem in cartItems)
			{
				if (cart.UpdateItem(cartItem) == false)
				{
					// Item doesn't exist we add it
					cart.AddItem(cartItem);
				}
			}

			return Ok(cart);
		}
	}
}
