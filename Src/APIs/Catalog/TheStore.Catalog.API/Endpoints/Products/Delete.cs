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

namespace TheStore.Catalog.API.Endpoints.Products
{
	public class Delete : EndpointBaseAsync
		.WithRequest<DeleteRequest>
		.WithActionResult
	{
		private readonly IValidator<DeleteRequest> validator;
		private readonly IApiRepository<CatalogDbContext, Product> apiRepository;
		private readonly Serilog.ILogger log = Log.ForContext<Delete>();

		public Delete(
			IValidator<DeleteRequest> validator,
			IApiRepository<CatalogDbContext, Product> apiRepository)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
		}

		[HttpDelete(DeleteRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[SwaggerOperation(
		   Summary = "Deletes a single product",
		   Description = "Deletes a single product",
		   OperationId = "Product.Single.Delete",
		   Tags = new[] { "Products" })]

		public async override Task<ActionResult> HandleAsync(
		[FromRoute] DeleteRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var product = await apiRepository.GetByIdAsync(new ProductId(request.ProductId), cancellationToken);

			if (product == null)
				return NotFound();

			await apiRepository.DeleteAsync(product, cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Delete a single product with id: {Id}", request.ProductId);

			return NoContent();
		}
	}
}