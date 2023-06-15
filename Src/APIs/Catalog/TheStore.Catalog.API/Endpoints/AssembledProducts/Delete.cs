using Ardalis.ApiEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.AssembledProducts
{
	public class Delete : EndpointBaseAsync
		.WithRequest<DeleteAssembledRequest>
		.WithActionResult
	{
		private readonly IValidator<DeleteAssembledRequest> validator;
		private readonly IApiRepository<CatalogDbContext, AssembledProduct> apiRepository;
		private readonly Serilog.ILogger log = Log.ForContext<Delete>();

		public Delete(
			IValidator<DeleteAssembledRequest> validator,
			IApiRepository<CatalogDbContext, AssembledProduct> apiRepository)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
		}

		[HttpDelete(DeleteAssembledRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[SwaggerOperation(
		   Summary = "Deletes an assembled product",
		   Description = "Deletes an assembled product",
		   OperationId = "Product.Assembled.Delete",
		   Tags = new[] { "AssembledProducts" })]

		public async override Task<ActionResult> HandleAsync(
		[FromRoute] DeleteAssembledRequest request,
			CancellationToken cancellationToken = default)
		{
			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Delete assembled product with id: {Id}", request.ProductId);

			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var assembledProduct = await apiRepository.GetByIdAsync(new AssembledProductId(request.ProductId), cancellationToken);

			if (assembledProduct == null)
			{
				return NotFound();
			}

			await apiRepository.ExecuteDeleteAsync<AssembledProduct, AssembledProductId>(assembledProduct.Id, cancellationToken);

			return NoContent();
		}
	}
}
