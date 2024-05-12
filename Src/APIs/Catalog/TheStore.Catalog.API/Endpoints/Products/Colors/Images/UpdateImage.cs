using Ardalis.ApiEndpoints;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;
using TheStore.ApiCommon.Constants;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Catalog.Infrastructure.Mediator.Handlers.ImageUpload;
using TheStore.Requests;
using TheStore.Requests.Models.Products;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Catalog.API.Endpoints.Products.Colors.Images
{
	public class UpdateImage : EndpointBaseAsync
		.WithRequest<UpdateImageOfVariantRequest>
		.WithActionResult
	{
		private readonly IValidator<UpdateImageOfVariantRequest> validator;
		private readonly IApiRepository<CatalogDbContext, Product> apiRepository;
		private readonly IMediator mediator;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<UpdateImage>();

		public UpdateImage(
			IValidator<UpdateImageOfVariantRequest> validator,
			IApiRepository<CatalogDbContext, Product> apiRepository,
			IMediator mediator,
			IMapper mapper)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
			this.mediator = mediator;
			this.mapper = mapper;
		}

		[HttpPut(UpdateImageOfVariantRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[SwaggerOperation(
		   Summary = "Updates an image of a variant of a single product",
		   Description = "Updates an image of a variant of a single product",
		   OperationId = "Product.Single.Variant.Color.Image.Update",
		   Tags = new[] { "Products" })]
		public async override Task<ActionResult> HandleAsync(
			UpdateImageOfVariantRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var singleProduct = await apiRepository
				.GetByIdAsync(new ProductId(request.ProductId), cancellationToken);

			if (singleProduct == null)
				return NotFound("Product not found");

			var variant = singleProduct.Variants.Find(v => v.Sku == request.Sku);
			if (variant == null)
				return NotFound("Variant not found");

			var color = variant.Color;

			var image = color.Images.Find(x => x.FileNameWithExtension == request.DecodedImagePath);
			if (image == null)
				return NotFound("Image not found");

			var imagePath = await mediator
				.Send(new UploadImageRequest(new FormFile(request.Image.File.OpenReadStream(cancellationToken: cancellationToken), 0, request.Image.File.Size, null!, request.Image.File.Name),
											ResourceFilePaths.ProductsImages, request.ImagePath), cancellationToken);

			image = new Image(imagePath, mapper.Map<MultilanguageString>(request.Image.Alt), request.Image.IsMainImage);
			await apiRepository.SaveChangesAsync(cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
			{
				log.Information("Update an image with path: {ImagePath} in variant with SKU: {Sku} in product with id: {Id}",
					request.ImagePath, request.Sku, request.ProductId);
			}

			return NoContent();
		}
	}
}