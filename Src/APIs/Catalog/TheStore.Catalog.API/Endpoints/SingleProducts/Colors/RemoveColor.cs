using Ardalis.ApiEndpoints;
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
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.SingleProducts.Colors
{
	public class RemoveColor : EndpointBaseAsync
		.WithRequest<RemoveColorRequest>
		.WithActionResult
	{

		private readonly IValidator<RemoveColorRequest> validator;
		private readonly IApiRepository<CatalogDbContext, SingleProduct> apiRepository;
		private readonly Serilog.ILogger log = Log.ForContext<RemoveColor>();

		public RemoveColor(
			IValidator<RemoveColorRequest> validator,
			IApiRepository<CatalogDbContext, SingleProduct> apiRepository)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
		}

		[HttpDelete(RemoveColorRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[SwaggerOperation(
		   Summary = "Removes a color from a single product",
		   Description = "Removes a color from a single product",
		   OperationId = "Product.Single.Color.Remove",
		   Tags = new[] { "Products" })]
		public async override Task<ActionResult> HandleAsync(
		[FromRoute] RemoveColorRequest request,
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

			singleProduct.RemoveColor(color);
			await apiRepository.SaveChangesAsync(cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Remove a color with code: {ColorCode} from single product with id: {Id}", color.ColorCode, request.ProductId);

			return NoContent();
		}
	}
}
