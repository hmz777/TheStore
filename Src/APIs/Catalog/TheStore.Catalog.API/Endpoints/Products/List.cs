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
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Catalog.Infrastructure.Data.Specifications.Products;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products
{
	public class List : EndpointBaseAsync
		.WithRequest<ListRequest>
		.WithActionResult<ProductsPaginatedResult>
	{
		private readonly IValidator<ListRequest> validator;
		private readonly IReadApiRepository<CatalogDbContext, Product> repository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<List>();

		public List(
			IValidator<ListRequest> validator,
			IReadApiRepository<CatalogDbContext, Product> repository,
			IMapper mapper)
		{
			this.validator = validator;
			this.repository = repository;
			this.mapper = mapper;
		}

		[HttpGet(ListRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[SwaggerOperation(
			Summary = "Lists single products",
			Description = "Lists single products with pagination using skip and take",
			OperationId = "Product.Single.List",
			Tags = new[] { "Products" })]
		public async override Task<ActionResult<ProductsPaginatedResult>> HandleAsync(
			[FromQuery] ListRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var spec = new ListProductsPaginationDefaultOrderReadSpec(request.Take, request.Page);

			var products = (await repository
				.ListAsync(spec, cancellationToken))
				.Map<Product, ProductDtoRead>(mapper);

			var productscount = await repository.CountAsync(spec, cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("List products with Page: {Page} and Take: {Take}", request.Page, request.Take, request.CorrelationId);

			return new ProductsPaginatedResult
			{
				Products = products,
				Count = productscount,
				PageNumber = request.Page
			};
		}
	}
}