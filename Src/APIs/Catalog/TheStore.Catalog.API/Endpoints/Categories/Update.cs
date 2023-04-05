using Ardalis.ApiEndpoints;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Catalog.API.Data;
using TheStore.Catalog.API.Domain.Categories;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.SharedModels.Models.Category;

namespace TheStore.Catalog.API.Endpoints.Categories
{
	public class Update : EndpointBaseAsync
		.WithRequest<UpdateRequest>
		.WithActionResult<UpdateResponse>
	{
		private readonly IValidator<UpdateRequest> validator;
		private readonly IApiRepository<CatalogDbContext, Category> apiRepository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<Delete>();

		public Update(
			IValidator<UpdateRequest> validator,
			IApiRepository<CatalogDbContext, Category> apiRepository,
			IMapper mapper)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
			this.mapper = mapper;
		}

		[HttpPut(UpdateRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[SwaggerOperation(
		   Summary = "Updates a category",
		   Description = "Updates a category",
		   OperationId = "Category.Update",
		   Tags = new[] { "Categories" })]
		public async override Task<ActionResult<UpdateResponse>> HandleAsync(UpdateRequest request, CancellationToken cancellationToken = default)
		{
			log.Information("Update category with id: {Id}", request.CategoryId);

			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var category = await apiRepository.GetByIdAsync(new CategoryId(request.CategoryId), cancellationToken);

			if (category == null)
			{
				return NotFound();
			}

			mapper.Map(request, category);
			await apiRepository.SaveChangesAsync(cancellationToken);

			return NoContent();
		}
	}
}