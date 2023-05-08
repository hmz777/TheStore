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
using TheStore.Catalog.Core.ValueObjects.Products;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.SingleProducts.Colors
{
	public class AddColor : EndpointBaseAsync
		.WithRequest<AddColorRequest>
		.WithActionResult
	{

		private readonly IValidator<AddColorRequest> validator;
		private readonly IApiRepository<CatalogDbContext, SingleProduct> apiRepository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<AddColor>();

		public AddColor(
			IValidator<AddColorRequest> validator,
			IApiRepository<CatalogDbContext, SingleProduct> apiRepository,
			IMapper mapper)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
			this.mapper = mapper;
		}

		[HttpPost(AddColorRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[SwaggerOperation(
		   Summary = "Adds a color to a single product",
		   Description = "Adds a color to a single product",
		   OperationId = "Product.Single.Color.Add",
		   Tags = new[] { "Products" })]
		public async override Task<ActionResult> HandleAsync(
			AddColorRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var singleProduct = await apiRepository
				.GetByIdAsync(new ProductId(request.ProductId), cancellationToken);

			if (singleProduct == null)
				return NotFound();

			if (singleProduct.ProductColors.Any(x => x.ColorCode == request.Color.ColorCode))
				return Conflict("Color already exists");

			singleProduct.AddColor(mapper.Map<ProductColor>(request.Color));
			await apiRepository.SaveChangesAsync(cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Add a color with code: {ColorCode} to single product with id: {Id}", request.ProductId, request.Color.ColorCode);

			return CreatedAtRoute(
				GetByIdRequest.RouteName,
				routeValues: new { ProductId = singleProduct.Id.Id },
				mapper.Map<SingleProductDto>(singleProduct));
		}
	}
}