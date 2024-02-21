using Ardalis.ApiEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;
using System.Web;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products.Colors.Images
{
	public class RemoveImage : EndpointBaseAsync
		.WithRequest<RemoveImageFromVariantRequest>
		.WithActionResult
	{
		private readonly IValidator<RemoveImageFromVariantRequest> validator;
		private readonly IApiRepository<CatalogDbContext, Product> apiRepository;
		private readonly Serilog.ILogger log = Log.ForContext<RemoveImage>();

		public RemoveImage(
			IValidator<RemoveImageFromVariantRequest> validator,
			IApiRepository<CatalogDbContext, Product> apiRepository)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
		}

		[HttpDelete(RemoveImageFromVariantRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[SwaggerOperation(
		   Summary = "Removes an image from a single product variant",
		   Description = "Removes an image from a single product variant",
		   OperationId = "Product.Single.Variant.Color.Image.Remove",
		   Tags = new[] { "Products" })]
		public async override Task<ActionResult> HandleAsync(
			[FromRoute] RemoveImageFromVariantRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var singleProduct = await apiRepository
				.GetByIdAsync(new ProductId(request.ProductId), cancellationToken);

			if (singleProduct == null)
				return NotFound("Product not found");

			var variant = singleProduct.Variants.FirstOrDefault(v => v.Sku == request.Sku);
			if (variant == null)
				return NotFound("Variant not found");

			var color = variant.Color;

			var image = color.Images.FirstOrDefault(x => x.StringFileUri == request.DecodedImagePath);
			if (image == null)
				return NotFound("Image not found");

			color.RemoveImage(image);
			await apiRepository.SaveChangesAsync(cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Remove an image with path: {ImagePath} from variant with SKU: {Sku} from product with id: {Id}",
					request.ImagePath, request.Sku, request.ProductId);

			return NoContent();
		}
	}
}