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
	public class RemovePart : EndpointBaseAsync
		.WithRequest<RemovePartRequest>
		.WithActionResult
	{
		private readonly IValidator<RemovePartRequest> validator;
		private readonly IApiRepository<CatalogDbContext, AssembledProduct> assembledProductRepository;
		private readonly Serilog.ILogger log = Log.ForContext<RemovePart>();

		public RemovePart(
			IValidator<RemovePartRequest> validator,
			IApiRepository<CatalogDbContext, AssembledProduct> assembledProductRepository)
		{
			this.validator = validator;
			this.assembledProductRepository = assembledProductRepository;
		}

		[HttpDelete(RemovePartRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[SwaggerOperation(
		   Summary = "Removes a part from an assembled product",
		   Description = "Removes a part from an assembled product",
		   OperationId = "Product.Assembled.Part.Remove",
		   Tags = new[] { "Products" })]
		public async override Task<ActionResult> HandleAsync(
			RemovePartRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var partId = new ProductId(request.PartId);

			var assembledProduct = await assembledProductRepository
				.GetByIdAsync(new AssembledProductId(request.ProductId), cancellationToken);

			if (assembledProduct == null)
				return NotFound("Assembled product not found");

			if (assembledProduct.Parts.Contains(partId) == false)
				return NotFound("Part not found");

			assembledProduct.RemovePart(partId);
			await assembledProductRepository.SaveChangesAsync(cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Remove part with id: {PartId} from assembled product with id: {Id}", request.PartId, request.ProductId);

			return NoContent();
		}
	}
}