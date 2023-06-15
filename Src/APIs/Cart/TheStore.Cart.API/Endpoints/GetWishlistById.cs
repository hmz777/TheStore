using Ardalis.ApiEndpoints;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.AutoMapper;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Cart.Core.Aggregates;
using TheStore.Cart.Infrastructure.Data;
using TheStore.Cart.Infrastructure.Data.Specifications;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Wishlist;

namespace TheStore.Cart.API.Endpoints
{
	public class GetWishlistById : EndpointBaseAsync
		.WithRequest<GetWishlistByIdRequest>
		.WithActionResult<WishlistDto>
	{
		private readonly IValidator<GetWishlistByIdRequest> validator;
		private readonly IReadApiRepository<CartDbContext, Wishlist> apiRepository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<GetWishlistById>();

		public GetWishlistById(
			IValidator<GetWishlistByIdRequest> validator,
			IReadApiRepository<CartDbContext, Wishlist> apiRepository,
			IMapper mapper)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
			this.mapper = mapper;
		}

		[HttpGet(GetWishlistByIdRequest.RouteTemplate, Name = GetWishlistByIdRequest.RouteName)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[SwaggerOperation(
			Summary = "Gets a wishlist by id",
			Description = "Gets a wishlist by id",
			OperationId = "Wishlist.GetById",
			Tags = new[] { "Wishlists" })]
		public async override Task<ActionResult<WishlistDto>> HandleAsync(
			GetWishlistByIdRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var wishlist = (await apiRepository
				.FirstOrDefaultAsync(new GetWishlistByIdReadSpec(request.WishlistId), cancellationToken))
				.Map<Wishlist, WishlistDto>(mapper);

			if (wishlist == null)
				return NotFound();

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Get wishlist with id: {WishlistId}", request.WishlistId);

			return wishlist;
		}
	}
}