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
using TheStore.Catalog.Core.Aggregates.Categories;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Catalog.Infrastructure.Data.Specifications.Categories;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Categories;

namespace TheStore.Catalog.API.Endpoints.Categories
{
	public class List : EndpointBaseAsync
		.WithRequest<ListRequest>
		.WithActionResult<List<CategoryDto>>
	{
		private readonly IValidator<ListRequest> validator;
		private readonly IReadApiRepository<CatalogDbContext, Category> repository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<List>();

		public List(
			IValidator<ListRequest> validator,
			IReadApiRepository<CatalogDbContext, Category> repository,
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
			Summary = "Lists catalog categories",
			Description = "Lists catalog categories with pagination using skip and take",
			OperationId = "Category.List",
			Tags = new[] { "Categories" })]
		public async override Task<ActionResult<List<CategoryDto>>> HandleAsync(
			[FromQuery] ListRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var categories = (await repository
				.ListAsync(new ListCategoriesPaginationDefaultOrderReadSpec(request.Take, request.Page), cancellationToken))
				.Map<Category, CategoryDto>(mapper);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("List categories with Page: {Page} and Take: {Take}", request.Page, request.Take, request.CorrelationId);

			return categories;
		}
	}
}