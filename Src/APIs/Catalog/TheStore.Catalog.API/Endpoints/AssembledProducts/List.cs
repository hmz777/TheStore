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
using TheStore.Requests;
using TheStore.Requests.Models.Products;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.AssembledProducts
{
	public class List : EndpointBaseAsync
		.WithRequest<ListAssembledRequest>
		.WithActionResult<List<AssembledProductDtoRead>>
	{
		private readonly IValidator<ListAssembledRequest> validator;
		private readonly IReadApiRepository<CatalogDbContext, AssembledProduct> repository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<List>();

		public List(
			IValidator<ListAssembledRequest> validator,
			IReadApiRepository<CatalogDbContext, AssembledProduct> repository,
			IMapper mapper)
		{
			this.validator = validator;
			this.repository = repository;
			this.mapper = mapper;
		}

		[HttpGet(ListAssembledRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[SwaggerOperation(
			Summary = "Lists assembled products",
			Description = "Lists assembled products with pagination using skip and take",
			OperationId = "Product.Assembled.List",
			Tags = new[] { "AssembledProducts" })]
		public async override Task<ActionResult<List<AssembledProductDtoRead>>> HandleAsync(
			[FromQuery] ListAssembledRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var assembledProducts = (await repository
				.ListAsync(new ListAssembledProductsPaginationReadSpec(request.Take, request.Page), cancellationToken))
				.Map<AssembledProduct, AssembledProductDtoRead>(mapper);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("List assembled products with Page: {Page} and Take: {Take}", request.Page, request.Take, request.CorrelationId);

			return assembledProducts;
		}
	}
}
