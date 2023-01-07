using ApiCommon.Extensions.AutoMapper;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Catalog.Data.Specifications.Categories;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.Validation;
using TheStore.Catalog.API.Data;
using TheStore.Catalog.API.Domain.Categories;
using TheStore.Catalog.API.Dtos.Category;

namespace TheStore.Catalog.API.Endpoints.Categories
{
	public class Get : EndpointBaseAsync
		.WithRequest<GetRequest>
		.WithActionResult<List<CategoryDto>>
	{
		private readonly IValidator<GetRequest> validator;
		private readonly IReadRepository<Category, CatalogDbContext> repository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<Get>();

		public Get(
			IValidator<GetRequest> validator,
			IReadRepository<Category, CatalogDbContext> repository,
			IMapper mapper)
		{
			this.validator = validator;
			this.repository = repository;
			this.mapper = mapper;
		}

		[HttpGet(GetRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[SwaggerOperation(
			Summary = "Lists catalog categories",
			Description = "Lists catalog categories with pagination using skip and take",
			OperationId = "Category.Get",
			Tags = new[] { "Categories" })]
		public async override Task<ActionResult<List<CategoryDto>>> HandleAsync(
			[FromQuery] GetRequest request,
			CancellationToken cancellationToken = default)
		{
			log.Information("Get categories with Page: {Page} and Take: {Take}", request.Page, request.Take);

			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var categories = (await repository
				.ListAsync(new ListCategoriesPaginationDefaultOrderReadSpec(request.Take, request.Page), cancellationToken))
				.Map<Category, CategoryDto>(mapper);

			return categories;
		}
	}
}