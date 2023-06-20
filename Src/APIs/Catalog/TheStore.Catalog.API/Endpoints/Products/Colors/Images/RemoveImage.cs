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
		.WithRequest<RemoveImageFromColorRequest>
		.WithActionResult
	{

		private readonly IValidator<RemoveImageFromColorRequest> validator;
		private readonly IApiRepository<CatalogDbContext, Product> apiRepository;
		private readonly Serilog.ILogger log = Log.ForContext<RemoveColor>();

		public RemoveImage(
			IValidator<RemoveImageFromColorRequest> validator,
			IApiRepository<CatalogDbContext, Product> apiRepository)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
		}

		[HttpDelete(RemoveImageFromColorRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[SwaggerOperation(
		   Summary = "Removes an image from a single product color",
		   Description = "Removes an image from a single product color",
		   OperationId = "Product.Single.Color.Image.Remove",
		   Tags = new[] { "Products" })]
		public async override Task<ActionResult> HandleAsync(
			[FromRoute] RemoveImageFromColorRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var singleProduct = await apiRepository
				.GetByIdAsync(new ProductId(request.ProductId), cancellationToken);

			if (singleProduct == null)
				return NotFound("Product not found");

			var color = singleProduct.ProductColors.FirstOrDefault(x => x.ColorCode == request.ColorCode);
			if (color == null)
				return NotFound("Color not found");

			var decodedImagePath = HttpUtility.UrlDecode(request.ImagePath);

			var image = color.Images.FirstOrDefault(x => x.StringFileUri == decodedImagePath);
			if (image == null)
				return NotFound("Image not found");

			color.RemoveImage(image);
			await apiRepository.SaveChangesAsync(cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Remove an image with path: {ImagePath} from color with code: {ColorCode} from single product with id: {Id}", image.StringFileUri, color.ColorCode, request.ProductId);

			return NoContent();
		}
	}
}