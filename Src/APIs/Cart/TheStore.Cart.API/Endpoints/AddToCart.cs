using Ardalis.ApiEndpoints;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Cart.Core.Entities;
using TheStore.Cart.Infrastructure.Data;
using TheStore.Cart.Infrastructure.Services;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Cart;

namespace TheStore.Cart.API.Endpoints
{
	public class AddToCart : EndpointBaseAsync
		.WithRequest<AddToCartRequest>
		.WithActionResult
	{
		private readonly IValidator<AddToCartRequest> validator;
		private readonly CatalogEntityCheckService catalogEntityCheckService;
		private readonly IApiRepository<CartDbContext, Core.Aggregates.Cart> apiRepository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<AddToCart>();

		public AddToCart(
			IValidator<AddToCartRequest> validator,
			CatalogEntityCheckService catalogEntityCheckService,
			IApiRepository<CartDbContext, Core.Aggregates.Cart> apiRepository,
			IMapper mapper)
		{
			this.validator = validator;
			this.catalogEntityCheckService = catalogEntityCheckService;
			this.apiRepository = apiRepository;
			this.mapper = mapper;
		}

		[HttpPost(AddToCartRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[SwaggerOperation(
		   Summary = "Adds item to cart",
		   Description = "Adds item to cart",
		   OperationId = "Cart.Items.Add",
		   Tags = new[] { "Carts" })]
		public async override Task<ActionResult> HandleAsync(
		[FromBody] AddToCartRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var productExists = await catalogEntityCheckService
				.CheckProductExistsAsync(request.ProductId, cancellationToken);

			if (productExists == false)
				return NotFound("Product not found");

			var cart = await apiRepository
				.GetByIdAsync(request.CartId, cancellationToken);

			if (cart == null)
				return NotFound("Cart not found");

			var cartItem = cart.Items.FirstOrDefault(ci => ci.ProductId == request.ProductId);

			if (cartItem != null)
			{
				// Item already exists so we increase quantity
				cartItem.IncreaseQuantity();
			}
			else
			{
				// Item don't exist we create a new one with quantity of 1
				cartItem = new CartItem(request.ProductId, 1);
				cart.AddItem(cartItem);
			}

			await apiRepository.SaveChangesAsync(cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Add product with id: {ProductId} to cart with id: {CartId}",
					request.ProductId, request.CartId);

			return CreatedAtRoute(
				GetCartByIdRequest.RouteName,
				routeValues: new { CartId = request.CartId },
				mapper.Map<CartDto>(cart));
		}
	}
}