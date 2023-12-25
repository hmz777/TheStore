using Ardalis.ApiEndpoints;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;
using TheStore.ApiCommon.Data.Helpers;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.ApiCommon.Services;
using TheStore.Catalog.Core.Aggregates.Categories;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Events.Categories.IntegrationEvents;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Categories;

namespace TheStore.Catalog.API.Endpoints.Categories
{
	public class Update : EndpointBaseAsync
		.WithRequest<UpdateRequest>
		.WithActionResult
	{
		private readonly IValidator<UpdateRequest> validator;
		private readonly IApiRepository<CatalogDbContext, Category> apiRepository;
		private readonly IMapper mapper;
		private readonly EventDispatcher eventDispatcher;
		private readonly Serilog.ILogger log = Log.ForContext<Update>();

		public Update(
			IValidator<UpdateRequest> validator,
			IApiRepository<CatalogDbContext, Category> apiRepository,
			IMapper mapper,
			EventDispatcher eventDispatcher)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
			this.mapper = mapper;
			this.eventDispatcher = eventDispatcher;
		}

		[HttpPut(UpdateRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[SwaggerOperation(
		   Summary = "Updates a category",
		   Description = "Updates a category",
		   OperationId = "Category.Update",
		   Tags = new[] { "Categories" })]
		public async override Task<ActionResult> HandleAsync(
		    UpdateRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var category = await apiRepository.GetByIdAsync(new CategoryId(request.CategoryId), cancellationToken);

			if (category == null)
				return NotFound();

			eventDispatcher.AddEvent(new CategoryUpdatedIntegrationEvent(category.Name));

			await RepositoryHelpers.PropertyUpdateAsync(request.Category, category, mapper, apiRepository);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Update category with id: {Id}", request.CategoryId);

			return NoContent();
		}
	}
}