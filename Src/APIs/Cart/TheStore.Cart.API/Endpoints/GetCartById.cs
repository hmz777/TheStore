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
	public class GetCartById : EndpointBaseAsync
		.WithRequest<GetCartByIdRequest>
		.WithActionResult<CartDto>
	{
		private readonly IValidator<GetCartByIdRequest> validator;
		private readonly IReadApiRepository<CartDbContext, Core.Aggregates.Cart> apiRepository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<GetCartById>();

		public GetCartById(
			IValidator<GetCartByIdRequest> validator,
			IReadApiRepository<CartDbContext, Core.Aggregates.Cart> apiRepository,
			IMapper mapper)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
			this.mapper = mapper;
		}

		[HttpGet(GetCartByIdRequest.RouteTemplate, Name = GetCartByIdRequest.RouteName)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[SwaggerOperation(
			Summary = "Gets a cart by id",
			Description = "Gets a cart by id",
			OperationId = "Cart.GetById",
			Tags = new[] { "Carts" })]
		public async override Task<ActionResult<CartDto>> HandleAsync(
			GetCartByIdRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var cart = (await apiRepository
				.FirstOrDefaultAsync(new GetCartByIdReadSpec(request.CartId), cancellationToken))
				.Map<Core.Aggregates.Cart, CartDto>(mapper);

			if (cart == null)
				return NotFound();

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Get cart with id: {CartId}", request.CartId);

			return cart;
		}
	}
}