using Ardalis.ApiEndpoints;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Cart.Core.Entities;
using TheStore.Cart.Core.ValueObjects.Keys;
using TheStore.Cart.Infrastructure.Data;
using TheStore.Cart.Infrastructure.Data.Specifications;
using TheStore.Cart.Infrastructure.Services;
using TheStore.Requests;
using TheStore.Requests.Models.Cart;
using TheStore.SharedModels.Models.Cart;

namespace TheStore.Cart.API.Endpoints
{
	public class AddToCart : EndpointBaseAsync
		.WithRequest<AddToCartRequest>
		.WithActionResult
	{
		private readonly IValidator<AddToCartRequest> validator;
		private readonly ICatalogEntityCheckService catalogEntityCheckService;
		private readonly IApiRepository<CartDbContext, Core.Aggregates.Cart> apiRepository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<AddToCart>();

		public AddToCart(
			IValidator<AddToCartRequest> validator,
			ICatalogEntityCheckService catalogEntityCheckService,
			IApiRepository<CartDbContext, Core.Aggregates.Cart> apiRepository,
			IMapper mapper)
		{
			this.validator = validator;
			this.catalogEntityCheckService = catalogEntityCheckService;
			this.apiRepository = apiRepository;
			this.mapper = mapper;
		}

		[Authorize]
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
			AddToCartRequest request,
			CancellationToken cancellationToken = default)
		// TODO: There is a mapping issue here
		// that prevents mapping from multiple sources in the same model.
		// Will be fixed in .NET8
		// Until then we map cart id from body
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var productExists = await catalogEntityCheckService
				.CheckProductExistsAsync(request.Sku, cancellationToken);

			if (productExists == false)
				return NotFound("Product not found");

			var buyerId = new BuyerId(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)!));

			var cart = await apiRepository
				.FirstOrDefaultAsync(new GetCartByBuyerIdSpec(buyerId), cancellationToken);

			if (cart == null)
				return NotFound("Cart not found");

			var cartItem = cart.Items.FirstOrDefault(ci => ci.Sku == request.Sku);

			if (cartItem != null)
			{
				// Item already exists so we increase quantity
				cartItem.IncreaseQuantity();
			}
			else
			{
				// Item don't exist we create a new one with quantity of 1
				cartItem = new CartItem(request.Sku, 1);
				cart.AddItem(cartItem);
			}

			await apiRepository.SaveChangesAsync(cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
			{
				log.Information("Add product with Sku: {Sku} to cart with id: {CartId}",
					request.Sku, cart.Id);
			}

			return Ok(mapper.Map<CartDto>(cart));
		}
	}
}