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
		private readonly IMapper mapper;
		private readonly IMediator mediator;
		private readonly Serilog.ILogger log = Log.ForContext<UpdateImage>();

		public UpdateImage(
			IValidator<UpdateImageOfColorRequest> validator,
			IApiRepository<CatalogDbContext, Product> apiRepository,
			IMapper mapper,
			IMediator mediator)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
			this.mapper = mapper;
			this.mediator = mediator;
		}

		[HttpPut(UpdateImageOfColorRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[SwaggerOperation(
		   Summary = "Updates an image of a color of a single product",
		   Description = "Updates an image of a color of a single product",
		   OperationId = "Product.Single.Color.Image.Update",
		   Tags = new[] { "Products" })]
		public async override Task<ActionResult> HandleAsync(
		[FromForm] UpdateImageOfColorRequest request,
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

			var newImageDto = request.Image;

			await mediator.Send(new UpdateImageRequest(image.StringFileUri, newImageDto, ResourceFilePaths.ProductsImages), cancellationToken);

			image = new Image(newImageDto.StringFileUri, request.Image.Alt);
			await apiRepository.SaveChangesAsync(cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Update an image with path: {ImagePath} in color with code: {ColorCode} in single product with id: {Id}", request.ImagePath, color.ColorCode, request.ProductId);

			return NoContent();
		}
	}
}