using Ardalis.ApiEndpoints;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Cart.Core.Aggregates;
using TheStore.Cart.Core.Entities;
using TheStore.Cart.Infrastructure.Data;
using TheStore.Cart.Infrastructure.Services.Rpc;
using TheStore.Requests;
using TheStore.Requests.Models.Wishlist;
using TheStore.SharedModels.Models.Wishlist;

namespace TheStore.Cart.API.Endpoints
{
    public class AddToWishlist : EndpointBaseAsync
		.WithRequest<AddToWishlistRequest>
		.WithActionResult
	{

		private readonly IValidator<AddToWishlistRequest> validator;
		private readonly ICatalogEntityCheckService catalogEntityCheckService;
		private readonly IApiRepository<CartDbContext, Wishlist> apiRepository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<AddToWishlist>();

		public AddToWishlist(
			IValidator<AddToWishlistRequest> validator,
			ICatalogEntityCheckService catalogEntityCheckService,
			IApiRepository<CartDbContext, Wishlist> apiRepository,
			IMapper mapper)
		{
			this.validator = validator;
			this.catalogEntityCheckService = catalogEntityCheckService;
			this.apiRepository = apiRepository;
			this.mapper = mapper;
		}

		[HttpPost(AddToWishlistRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[SwaggerOperation(
		   Summary = "Adds item to wishlist",
		   Description = "Adds item to wishlist",
		   OperationId = "Wishlist.Items.Add",
		   Tags = new[] { "Wishlists" })]
		public async override Task<ActionResult> HandleAsync(
		[FromBody] AddToWishlistRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var productExists = await catalogEntityCheckService
				.CheckProductExistsAsync(request.Sku, cancellationToken);

			if (productExists == false)
				return NotFound("Product not found");

			var wishlist = await apiRepository
				.GetByIdAsync(request.WishlistId, cancellationToken);

			if (wishlist == null)
				return NotFound("Wishlist not found");

			var wishlistItem = wishlist.Items.FirstOrDefault(wi => wi.Sku == request.Sku);

			// If item already exists so we do nothing

			if (wishlistItem == null)
			{
				// Item don't exist we create a new one
				wishlistItem = new WishlistItem(request.Sku);
				wishlist.AddItem(wishlistItem);

				await apiRepository.SaveChangesAsync(cancellationToken);
			}

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
			{
				log.Information("Add product with Sku: {Sku} to wishlist with id: {WishlistId}",
					request.Sku, request.WishlistId);
			}

			return CreatedAtRoute(
				GetWishlistByIdRequest.RouteName,
				routeValues: new { WishlistId = request.WishlistId },
				mapper.Map<WishlistDto>(wishlist));
		}
	}
}