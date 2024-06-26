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
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Requests;
using TheStore.Requests.Models.Products;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products
{
	public class Create : EndpointBaseAsync
		.WithRequest<CreateRequest>
		.WithActionResult<ProductCatalogDtoRead>
	{
		private readonly IValidator<CreateRequest> validator;
		private readonly IApiRepository<CatalogDbContext, Product> apiRepository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<Create>();

		public Create(
			IValidator<CreateRequest> validator,
			IApiRepository<CatalogDbContext, Product> apiRepository,
			IMapper mapper)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
			this.mapper = mapper;
		}

		[HttpPost(CreateRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[SwaggerOperation(
		   Summary = "Creates a single product",
		   Description = "Creates a single product",
		   OperationId = "Product.Single.Create",
		   Tags = new[] { "Products" })]
		public async override Task<ActionResult<ProductCatalogDtoRead>> HandleAsync(
			CreateRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var singleProduct = await apiRepository.AddAsync(mapper.Map<Product>(request.Product), cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Create a single product with name: {Name}", request.Product.Name);

			return CreatedAtRoute(GetByIdentifierRequest.RouteName, routeValues: new { ProductId = singleProduct.Id.Id }, mapper.Map<ProductCatalogDtoRead>(singleProduct));
		}
	}
}