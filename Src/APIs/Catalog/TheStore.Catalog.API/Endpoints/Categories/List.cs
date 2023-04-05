using Ardalis.ApiEndpoints;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.AutoMapper;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Catalog.API.Data;
using TheStore.Catalog.API.Data.Specifications.Categories;
using TheStore.Catalog.API.Domain.Categories;
using TheStore.SharedModels.Models.Category;

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