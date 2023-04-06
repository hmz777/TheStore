using Ardalis.ApiEndpoints;
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
	public class Delete : EndpointBaseAsync
		.WithRequest<DeleteRequest>
		.WithActionResult
	{
		private readonly IValidator<DeleteRequest> validator;
		private readonly IApiRepository<CatalogDbContext, Category> apiRepository;
		private readonly Serilog.ILogger log = Log.ForContext<Delete>();

		public Delete(
			IValidator<DeleteRequest> validator,
			IApiRepository<CatalogDbContext, Category> apiRepository)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
			this.apiRepository = apiRepository;
		}

		[HttpDelete(DeleteRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[SwaggerOperation(
		   Summary = "Deletes a category",
		   Description = "Deletes a category",
		   OperationId = "Category.Delete",
		   Tags = new[] { "Categories" })]
		public async override Task<ActionResult> HandleAsync(
			[FromRoute] DeleteRequest request,
			CancellationToken cancellationToken = default)
		{
			log.Information("Delete category with id: {Id}", request.CategoryId);

			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var category = await apiRepository.GetByIdAsync(new CategoryId(request.CategoryId), cancellationToken);

			if (category == null)
			{
				return NotFound();
			}

			await apiRepository.ExecuteDeleteAsync<Category, CategoryId>(category.Id, cancellationToken);

			return NoContent();
		}
	}
}