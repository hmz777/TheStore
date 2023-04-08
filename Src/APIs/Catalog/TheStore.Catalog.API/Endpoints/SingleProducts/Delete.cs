﻿using Ardalis.ApiEndpoints;
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

namespace TheStore.Catalog.API.Endpoints.SingleProducts
{
	public class Delete : EndpointBaseAsync
		.WithRequest<DeleteRequest>
		.WithActionResult
	{
		private readonly IValidator<DeleteRequest> validator;
		private readonly IApiRepository<CatalogDbContext, SingleProduct> apiRepository;
		private readonly Serilog.ILogger log = Log.ForContext<Delete>();

		public Delete(
			IValidator<DeleteRequest> validator,
			IApiRepository<CatalogDbContext, SingleProduct> apiRepository)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
		}

		[HttpDelete(DeleteRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[SwaggerOperation(
		   Summary = "Deletes a single product",
		   Description = "Deletes a single product",
		   OperationId = "Product.Single.Delete",
		   Tags = new[] { "Products" })]

		public async override Task<ActionResult> HandleAsync(
		[FromRoute] DeleteRequest request,
			CancellationToken cancellationToken = default)
		{
			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Delete a single product with id: {Id}", request.ProductId);

			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var singleProduct = await apiRepository.GetByIdAsync(new ProductId(request.ProductId), cancellationToken);

			if (singleProduct == null)
			{
				return NotFound();
			}

			await apiRepository.ExecuteDeleteAsync<SingleProduct, ProductId>(singleProduct.Id, cancellationToken);

			return NoContent();
		}
	}
}