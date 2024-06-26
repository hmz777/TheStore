﻿using Ardalis.ApiEndpoints;
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
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Requests;
using TheStore.Requests.Models.Products;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.AssembledProducts
{
	public class AddPart : EndpointBaseAsync
		.WithRequest<AddPartRequest>
		.WithActionResult
	{
		private readonly IValidator<AddPartRequest> validator;
		private readonly IApiRepository<CatalogDbContext, AssembledProduct> assembledProductRepository;
		private readonly IApiRepository<CatalogDbContext, Product> productRepository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<AddPart>();

		public AddPart(
			IValidator<AddPartRequest> validator,
			IApiRepository<CatalogDbContext, AssembledProduct> assembledProductRepository,
			IApiRepository<CatalogDbContext, Product> productRepository,
			IMapper mapper)
		{
			this.validator = validator;
			this.assembledProductRepository = assembledProductRepository;
			this.productRepository = productRepository;
			this.mapper = mapper;
		}

		[HttpPost(AddPartRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[SwaggerOperation(
		   Summary = "Adds a part to an assembled product",
		   Description = "Adds a part to an assembled product",
		   OperationId = "Product.Assembled.Part.Add",
		   Tags = new[] { "AssembledProducts" })]
		public async override Task<ActionResult> HandleAsync(
			AddPartRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var partId = new ProductId(request.PartId);

			var part = await productRepository
				.GetByIdAsync(partId, cancellationToken);

			if (part == null)
				return NotFound("Part not found");

			var assembledProduct = await assembledProductRepository
				.GetByIdAsync(request.ProductId, cancellationToken);

			if (assembledProduct == null)
				return NotFound("Assembled product not found");

			if (assembledProduct.Parts.TryGetValue(partId, out _))
				return Conflict("Part already exists");

			assembledProduct.AddPart(partId, request.Sku);

			await assembledProductRepository.SaveChangesAsync(cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Add part with id: {PartId} and SKU: {Sku} to assembled product with id: {Id}",
					request.PartId, request.Sku, request.ProductId);

			return CreatedAtRoute(
			GetByIdentifierRequest.RouteName,
				routeValues: new { ProductId = assembledProduct.Id },
				mapper.Map<AssembledProductDtoRead>(assembledProduct));
		}
	}
}