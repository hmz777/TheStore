﻿using Ardalis.ApiEndpoints;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.AutoMapper;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Catalog.Infrastructure.Data.Specifications.Products;
using TheStore.Requests;
using TheStore.Requests.Models.Products;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products
{
	public class GetByIdentifier : EndpointBaseAsync
		.WithRequest<GetByIdentifierRequest>
		.WithActionResult<ProductDetailsDtoRead>
	{
		private readonly IValidator<GetByIdentifierRequest> validator;
		private readonly IReadApiRepository<CatalogDbContext, Product> repository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<GetByIdentifier>();

		public GetByIdentifier(
			IValidator<GetByIdentifierRequest> validator,
			IReadApiRepository<CatalogDbContext, Product> repository,
			IMapper mapper)
		{
			this.validator = validator;
			this.repository = repository;
			this.mapper = mapper;
		}

		[HttpGet(GetByIdentifierRequest.RouteTemplate, Name = GetByIdentifierRequest.RouteName)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[SwaggerOperation(
			Summary = "Gets a single product by identifier",
			Description = "Gets a single product by identifier",
			OperationId = "Product.Single.GetByIdentifier",
			Tags = new[] { "Products" })]
		public async override Task<ActionResult<ProductDetailsDtoRead>> HandleAsync(
		[FromRoute] GetByIdentifierRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var product = (await repository
				.FirstOrDefaultAsync(new GetProductByIdentifierSpec(request.Identifier), cancellationToken))
				.Map<Product, ProductDetailsDtoRead>(mapper);

			if (product == null)
				return NotFound();

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Get a single product with Identifier: {Identifier}", request.Identifier);

			return product;
		}
	}
}