using Ardalis.ApiEndpoints;
using AutoMapper;
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

namespace TheStore.Catalog.API.Endpoints.Products.Variants
{
	public class UpdateVariant : EndpointBaseAsync
		.WithRequest<UpdateVariantRequest>
		.WithActionResult
	{
		private readonly IValidator<UpdateVariantRequest> validator;
		private readonly IApiRepository<CatalogDbContext, Product> apiRepository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<UpdateVariant>();

		public UpdateVariant(
			IValidator<UpdateVariantRequest> validator,
			IApiRepository<CatalogDbContext, Product> apiRepository,
			IMapper mapper)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
			this.mapper = mapper;
		}

		[HttpPut(UpdateVariantRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[SwaggerOperation(
		   Summary = "Updates a product variant",
		   Description = "Updates a product variant",
		   OperationId = "Product.Variant.Update",
		   Tags = new[] { "Products" })]
		public async override Task<ActionResult> HandleAsync(
			UpdateVariantRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var product = await apiRepository.GetByIdAsync(new ProductId(request.ProductId), cancellationToken);

			if (product == null)
				return NotFound("Product not found");

			var variant = product.Variants.FirstOrDefault(p => p.Sku == request.Sku);

			if (variant == null)
				return NotFound("Variant not found");

			mapper.Map(request.Variant, variant);
			await apiRepository.SaveChangesAsync(cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Update variant with SKU: {Sku} in product with id: {Id}", request.Sku, request.ProductId);

			return NoContent();
		}
	}
}
