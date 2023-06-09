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
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.SingleProducts.Colors.Images
{
	public class AddImage : EndpointBaseAsync
		.WithRequest<AddImageToColorRequest>
		.WithActionResult
	{

		private readonly IValidator<AddImageToColorRequest> validator;
		private readonly IApiRepository<CatalogDbContext, SingleProduct> apiRepository;
		private readonly IMapper mapper;
		private readonly IMediator mediator;
		private readonly Serilog.ILogger log = Log.ForContext<AddImage>();

		public AddImage(
			IValidator<AddImageToColorRequest> validator,
			IApiRepository<CatalogDbContext, SingleProduct> apiRepository,
			IMapper mapper,
			IMediator mediator)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
			this.mapper = mapper;
			this.mediator = mediator;
		}

		[HttpPost(AddImageToColorRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[SwaggerOperation(
		   Summary = "Adds an image to a single product color",
		   Description = "Adds an image to a single product color",
		   OperationId = "Product.Single.Color.Image.Add",
		   Tags = new[] { "Products" })]
		public async override Task<ActionResult> HandleAsync(
		   [FromForm] AddImageToColorRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var singleProduct = await apiRepository
				.GetByIdAsync(new ProductId(request.ProductId), cancellationToken);

			if (singleProduct == null)
				return NotFound();

			var color = singleProduct.ProductColors.FirstOrDefault(x => x.ColorCode == request.ColorCode);
			if (color == null)
				return NotFound("Color not found");

			await mediator.Send(new AddImageRequest(request.Image, ResourceFilePaths.ProductsImages), cancellationToken);

			color.AddImage(mapper.Map<Image>(request.Image));
			await apiRepository.SaveChangesAsync(cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Add an image to the color with code: {ColorCode} in single product with id: {Id}", color.ColorCode, request.ProductId);

			return CreatedAtRoute(
				GetByIdRequest.RouteName,
				routeValues: new { ProductId = singleProduct.Id.Id },
				mapper.Map<SingleProductDto>(singleProduct));
		}
	}
}
