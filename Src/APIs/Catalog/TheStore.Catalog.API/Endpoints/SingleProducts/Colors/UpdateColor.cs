using Ardalis.ApiEndpoints;
using AutoMapper;
using FluentValidation;
using MediatR;
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
	public class UpdateColor : EndpointBaseAsync
		.WithRequest<UpdateColorRequest>
		.WithActionResult
	{
		private readonly IValidator<UpdateColorRequest> validator;
		private readonly IApiRepository<CatalogDbContext, SingleProduct> apiRepository;
		private readonly Serilog.ILogger log = Log.ForContext<UpdateColor>();

		public UpdateColor(
			IValidator<UpdateColorRequest> validator,
			IApiRepository<CatalogDbContext, SingleProduct> apiRepository)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
		}

		[HttpPut(UpdateColorRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[SwaggerOperation(
		   Summary = "Updates a color of a single product",
		   Description = "Updates a color of a single product",
		   OperationId = "Product.Single.Color.Update",
		   Tags = new[] { "Products" })]
		public async override Task<ActionResult> HandleAsync(
		    UpdateColorRequest request,
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

			color = new ProductColor(request.ColorCode, color.Images.ToList());
			await apiRepository.SaveChangesAsync(cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Update a color with code: {ColorCode} in single product with id: {Id}", color.ColorCode, request.ProductId);

			return NoContent();
		}
	}
}