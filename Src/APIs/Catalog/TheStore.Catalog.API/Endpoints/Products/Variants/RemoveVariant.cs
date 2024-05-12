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
using TheStore.Requests;
using TheStore.Requests.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products.Variants
{
	public class RemoveVariant : EndpointBaseAsync
		.WithRequest<RemoveVariantRequest>
		.WithActionResult
	{
		private readonly IValidator<RemoveVariantRequest> validator;
		private readonly IApiRepository<CatalogDbContext, Product> apiRepository;
		private readonly Serilog.ILogger log = Log.ForContext<RemoveVariant>();

		public RemoveVariant(
			IValidator<RemoveVariantRequest> validator,
			IApiRepository<CatalogDbContext, Product> apiRepository)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
		}

		[HttpDelete(RemoveVariantRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[SwaggerOperation(
		   Summary = "Removes a variant from a product",
		   Description = "Removes a variant from a product",
		   OperationId = "Product.Single.Variant.Remove",
		   Tags = new[] { "Products" })]
		public async override Task<ActionResult> HandleAsync(
		[FromRoute] RemoveVariantRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var product = await apiRepository
				.GetByIdAsync(new ProductId(request.ProductId), cancellationToken);

			if (product == null)
				return NotFound("Product not found");

			var variant = product.Variants.Where(v => v.Sku == request.Sku).FirstOrDefault();

			if (variant == null)
				return NotFound("Variant not found");

			product.Variants.Remove(variant);

			await apiRepository.SaveChangesAsync(cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Remove a variant with SKU: {Sku} from a product with id: {Id}", request.Sku, request.ProductId);

			return NoContent();
		}
	}
}