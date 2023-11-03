using Ardalis.ApiEndpoints;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;
using System.Web;
using TheStore.ApiCommon.Constants;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Catalog.Infrastructure.Mediator.Handlers.ImageUpload;
using TheStore.SharedKernel.ValueObjects;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products.Colors.Images
{
	public class UpdateImage : EndpointBaseAsync
		.WithRequest<UpdateImageOfColorRequest>
		.WithActionResult
	{

		private readonly IValidator<UpdateImageOfColorRequest> validator;
		private readonly IApiRepository<CatalogDbContext, Product> apiRepository;
		private readonly IMediator mediator;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<UpdateImage>();

		public UpdateImage(
			IValidator<UpdateImageOfColorRequest> validator,
			IApiRepository<CatalogDbContext, Product> apiRepository,
			IMediator mediator,
			IMapper mapper)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
			this.mediator = mediator;
			this.mapper = mapper;
		}

		[HttpPut(UpdateImageOfColorRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[SwaggerOperation(
		   Summary = "Updates an image of a variant of a single product",
		   Description = "Updates an image of a variant of a single product",
		   OperationId = "Product.Single.Variant.Color.Image.Update",
		   Tags = new[] { "Products" })]
		public async override Task<ActionResult> HandleAsync(
			UpdateImageOfColorRequest request,
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

			var decodedImagePath = HttpUtility.UrlDecode(request.ImagePath);

			var image = color.Images.FirstOrDefault(x => x.FileNameWithExtension == decodedImagePath);
			if (image == null)
				return NotFound("Image not found");

			var imagePath = await mediator
				.Send(new UploadImageRequest(request.Image.File, ResourceFilePaths.ProductsImages, request.ImagePath), cancellationToken);

			image = new Image(imagePath, mapper.Map<MultilanguageString>(request.Image.Alt), request.Image.IsMainImage);
			await apiRepository.SaveChangesAsync(cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Update an image with path: {ImagePath} in variant with SKU: {Sku} in product with id: {Id}",
					request.ImagePath, request.Sku, request.ProductId);

			return NoContent();
		}
	}
}