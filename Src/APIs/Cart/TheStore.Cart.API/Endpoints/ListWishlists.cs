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
	public class ListWishlists : EndpointBaseAsync
		.WithRequest<ListRequest>
		.WithActionResult<List<WishlistDto>>
	{
		private readonly IValidator<ListRequest> validator;
		private readonly IReadApiRepository<CartDbContext, Wishlist> apiRepository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<ListWishlists>();

		public ListWishlists(
			IValidator<ListRequest> validator,
			IReadApiRepository<CartDbContext, Wishlist> apiRepository,
			IMapper mapper)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
			this.mapper = mapper;
		}

		[HttpGet(ListRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[SwaggerOperation(
			Summary = "Lists wishlists",
			Description = "Lists wishlists",
			OperationId = "Wishlists.List",
			Tags = new[] { "Wishlists" })]
		public async override Task<ActionResult<List<WishlistDto>>> HandleAsync(
			ListRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var wishlists = (await apiRepository
				.ListAsync(new ListWishlistsPaginationReadSpec(request.Take, request.Page), cancellationToken))
				.Map<Wishlist, WishlistDto>(mapper);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("List wishlists with page: {Page} and take: {Take}", request.Page, request.Take);

			return wishlists;
		}
	}
}