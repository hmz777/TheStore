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

namespace TheStore.Catalog.API.Endpoints.SingleProducts
{
	public class GetById : EndpointBaseAsync
		.WithRequest<GetByIdRequest>
		.WithActionResult<SingleProductDto>
	{
		private readonly IValidator<GetByIdRequest> validator;
		private readonly IReadApiRepository<CatalogDbContext, SingleProduct> repository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<GetById>();

		public GetById(
			IValidator<GetByIdRequest> validator,
			IReadApiRepository<CatalogDbContext, SingleProduct> repository,
			IMapper mapper)
		{
			this.validator = validator;
			this.repository = repository;
			this.mapper = mapper;
		}

		[HttpGet(GetByIdRequest.RouteTemplate, Name = GetByIdRequest.RouteName)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[SwaggerOperation(
			Summary = "Gets a single product by id",
			Description = "Gets a single product by id",
			OperationId = "Product.Single.GetById",
			Tags = new[] { "Products" })]
		public async override Task<ActionResult<SingleProductDto>> HandleAsync(
		[FromRoute] GetByIdRequest request,
			CancellationToken cancellationToken = default)
		{
			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Get a single product with Id: {Id}", request.ProductId);

			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var singleProduct = (await repository
				.FirstOrDefaultAsync(new GetSingleProductByIdReadSpec(new ProductId(request.ProductId)), cancellationToken))
				.Map<SingleProduct, SingleProductDto>(mapper);

			if (singleProduct == null)
				return NotFound();

			return singleProduct;
		}
	}
}