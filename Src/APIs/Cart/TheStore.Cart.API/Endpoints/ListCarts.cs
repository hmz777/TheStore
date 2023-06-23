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
using TheStore.Cart.Infrastructure.Data;
using TheStore.Cart.Infrastructure.Data.Specifications;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Cart;

namespace TheStore.Cart.API.Endpoints
{
	public class ListCarts : EndpointBaseAsync
		.WithRequest<ListRequest>
		.WithActionResult<List<CartDto>>
	{
		private readonly IValidator<ListRequest> validator;
		private readonly IReadApiRepository<CartDbContext, Core.Aggregates.Cart> apiRepository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<ListCarts>();

		public ListCarts(
			IValidator<ListRequest> validator,
			IReadApiRepository<CartDbContext, Core.Aggregates.Cart> apiRepository,
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
			Summary = "Lists carts",
			Description = "Lists carts",
			OperationId = "Carts.List",
			Tags = new[] { "Carts" })]
		public async override Task<ActionResult<List<CartDto>>> HandleAsync(
		[FromQuery] ListRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var carts = (await apiRepository
				.ListAsync(new ListCartsPaginationReadSpec(request.Take, request.Page), cancellationToken))
				.Map<Core.Aggregates.Cart, CartDto>(mapper);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("List carts with page: {Page} and take: {Take}", request.Page, request.Take);

			return carts;
		}
	}
}