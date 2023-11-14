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
using TheStore.SharedKernel.ValueObjects;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products.Colors.Images
{
	public class AddImage : EndpointBaseAsync
		.WithRequest<AddImageToVariantRequest>
		.WithActionResult
	{
		private readonly IValidator<AddImageToVariantRequest> validator;
		private readonly IApiRepository<CatalogDbContext, Product> apiRepository;
		private readonly IMapper mapper;
		private readonly IMediator mediator;
		private readonly Serilog.ILogger log = Log.ForContext<AddImage>();

		public AddImage(
			IValidator<AddImageToVariantRequest> validator,
			IApiRepository<CatalogDbContext, Product> apiRepository,
			IMapper mapper,
			IMediator mediator)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
			this.mapper = mapper;
			this.mediator = mediator;
		}

		[HttpPost(AddImageToVariantRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[SwaggerOperation(
		   Summary = "Adds an image to a product variant",
		   Description = "Adds an image to a product variant",
		   OperationId = "Product.Variant.Color.Image.Add",
		   Tags = new[] { "Products" })]
		public async override Task<ActionResult> HandleAsync(
			AddImageToVariantRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var product = await apiRepository
				.GetByIdAsync(new ProductId(request.ProductId), cancellationToken);

			if (product == null)
				return NotFound("Product not found");

			var variant = product.Variants.FirstOrDefault(v => v.Sku == request.Sku);
			if (variant == null)
				return NotFound("Variant not found");

			var color = variant.Color;

			var imagePath = await mediator
				.Send(new UploadImageRequest(request.Image.File, ResourceFilePaths.ProductsImages, null!), cancellationToken);

			color.AddImage(new Image(imagePath, mapper.Map<MultilanguageString>(request.Image.Alt), request.Image.IsMainImage));
			await apiRepository.SaveChangesAsync(cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Add an image to variant with SKU: {Sku} in product with id: {Id}",
					request.Sku, request.ProductId);

			return CreatedAtRoute(
				GetByIdRequest.RouteName,
				routeValues: new { ProductId = product.Id.Id },
				mapper.Map<ProductDtoRead>(product));
		}
	}
}
