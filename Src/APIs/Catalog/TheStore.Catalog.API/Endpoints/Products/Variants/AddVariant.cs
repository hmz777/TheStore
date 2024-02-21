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
using TheStore.Catalog.Infrastructure.Services;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products.Variants
{
	public class AddVariant : EndpointBaseAsync
		.WithRequest<AddVariantRequest>
		.WithActionResult
	{
		private readonly IValidator<AddVariantRequest> validator;
		private readonly IApiRepository<CatalogDbContext, Product> apiRepository;
		private readonly SkuService skuService;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<AddVariant>();

		public AddVariant(
			IValidator<AddVariantRequest> validator,
			IApiRepository<CatalogDbContext, Product> apiRepository,
			SkuService skuService,
			IMapper mapper)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
			this.skuService = skuService;
			this.mapper = mapper;
		}

		[HttpPost(AddVariantRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[SwaggerOperation(
		   Summary = "Adds a variant to a product",
		   Description = "Adds a variant to a product",
		   OperationId = "Product.Variant.Add",
		   Tags = new[] { "Products" })]
		public async override Task<ActionResult> HandleAsync(
			AddVariantRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var product = await apiRepository
				.GetByIdAsync(new ProductId(request.ProductId), cancellationToken);

			if (product == null)
				return NotFound("Product not found");

			// Assign SKU to the variant
			var variant = mapper.Map<ProductVariant>(request.ProductVariant);
			variant.Sku = await skuService.CreateSkuAsync();

			product.AddVariant(variant);

			await apiRepository.SaveChangesAsync(cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Create a variant of product with id: {Id}", request.ProductId);

			return CreatedAtRoute(GetByIdRequest.RouteName,
				routeValues: new { ProductId = product.Id.Id }, mapper.Map<ProductDtoRead>(product));
		}
	}
}