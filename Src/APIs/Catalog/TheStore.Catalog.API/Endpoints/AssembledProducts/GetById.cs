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
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Catalog.Infrastructure.Data.Specifications.Products;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.AssembledProducts
{
	public class GetById : EndpointBaseAsync
		.WithRequest<GetAssembledByIdRequest>
		.WithActionResult<AssembledProductDto>
	{
		private readonly IValidator<GetAssembledByIdRequest> validator;
		private readonly IReadApiRepository<CatalogDbContext, AssembledProduct> repository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<GetById>();

		public GetById(
			IValidator<GetAssembledByIdRequest> validator,
			IReadApiRepository<CatalogDbContext, AssembledProduct> repository,
			IMapper mapper)
		{
			this.validator = validator;
			this.repository = repository;
			this.mapper = mapper;
		}

		[HttpGet(GetAssembledByIdRequest.RouteTemplate, Name = GetAssembledByIdRequest.RouteName)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[SwaggerOperation(
			Summary = "Gets an assembled product by id",
			Description = "Gets an assembled product by id",
			OperationId = "Product.Assembled.GetById",
			Tags = new[] { "AssembledProducts" })]
		public async override Task<ActionResult<AssembledProductDto>> HandleAsync(
		[FromRoute] GetAssembledByIdRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var assembledProduct = (await repository
				.FirstOrDefaultAsync(
				new GetAssembledProductByIdReadSpec(new ProductId(request.ProductId)), cancellationToken))
				.Map<AssembledProduct, AssembledProductDto>(mapper);

			if (assembledProduct == null)
				return NotFound();

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Get assembled product with Id: {Id}", request.ProductId);

			return assembledProduct;
		}
	}
}