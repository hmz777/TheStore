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
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.AssembledProducts
{
	public class Update : EndpointBaseAsync
		.WithRequest<UpdateAssembledRequest>
		.WithActionResult
	{
		private readonly IValidator<UpdateAssembledRequest> validator;
		private readonly IApiRepository<CatalogDbContext, AssembledProduct> apiRepository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<Update>();

		public Update(
			IValidator<UpdateAssembledRequest> validator,
			IApiRepository<CatalogDbContext, AssembledProduct> apiRepository,
			IMapper mapper)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
			this.mapper = mapper;
		}

		[HttpPut(UpdateAssembledRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[SwaggerOperation(
		   Summary = "Updates an assembled product",
		   Description = "Updates a assembled product",
		   OperationId = "Product.Assembled.Update",
		   Tags = new[] { "Products" })]
		public async override Task<ActionResult> HandleAsync(
			UpdateAssembledRequest request,
			CancellationToken cancellationToken = default)
		{
			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Update assembled product with id: {Id}", request.ProductId);

			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var assembledProduct = await apiRepository.GetByIdAsync(new AssembledProductId(request.ProductId), cancellationToken);

			if (assembledProduct == null)
			{
				return NotFound();
			}

			await RepositoryHelpers.PropertyUpdateAsync(request, assembledProduct, mapper, apiRepository);

			return NoContent();
		}
	}
}
