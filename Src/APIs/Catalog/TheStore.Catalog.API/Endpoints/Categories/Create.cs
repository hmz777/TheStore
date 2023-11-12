using Ardalis.ApiEndpoints;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Catalog.Core.Aggregates.Categories;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Categories;

namespace TheStore.Catalog.API.Endpoints.Categories
{
	public class Create : EndpointBaseAsync
		.WithRequest<CreateRequest>
		.WithActionResult<CategoryDtoRead>
	{
		private readonly IValidator<CreateRequest> validator;
		private readonly IApiRepository<CatalogDbContext, Category> apiRepository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<Create>();

		public Create(
			IValidator<CreateRequest> validator,
			IApiRepository<CatalogDbContext, Category> apiRepository,
			IMapper mapper)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
			this.mapper = mapper;
		}

		[HttpPost(CreateRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[SwaggerOperation(
		   Summary = "Creates a category",
		   Description = "Creates a category",
		   OperationId = "Category.Create",
		   Tags = new[] { "Categories" })]
		public async override Task<ActionResult<CategoryDtoRead>> HandleAsync(
		    CreateRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var category = await apiRepository.AddAsync(mapper.Map<Category>(request.Category), cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Create category with name: {Name}", request.Category.Name, request.CorrelationId);

			return CreatedAtRoute(
				GetByIdRequest.RouteName,
				routeValues: new { CategoryId = category.Id.Id },
				mapper.Map<CategoryDtoRead>(category));
		}
	}
}