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

namespace TheStore.Catalog.API.Endpoints.AssembledProducts
{
	public class Create : EndpointBaseAsync
		.WithRequest<CreateAssembledRequest>
		.WithActionResult<AssembledProductDtoRead>
	{
		private readonly IValidator<CreateAssembledRequest> validator;
		private readonly IApiRepository<CatalogDbContext, AssembledProduct> apiRepository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<Create>();

		public Create(
			IValidator<CreateAssembledRequest> validator,
			IApiRepository<CatalogDbContext, AssembledProduct> apiRepository,
			IMapper mapper)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
			this.mapper = mapper;
		}

		[HttpPost(CreateAssembledRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[SwaggerOperation(
		   Summary = "Creates a single product",
		   Description = "Creates a single product",
		   OperationId = "Product.Single.Create",
		   Tags = new[] { "AssembledProducts" })]
		public async override Task<ActionResult<AssembledProductDtoRead>> HandleAsync(
			CreateAssembledRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var assembledProduct = await apiRepository
				.AddAsync(mapper.Map<AssembledProduct>(request.AssembledProduct), cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Create assembled product with name: {Name}", request.AssembledProduct.Name);

			return CreatedAtRoute(
				GetByIdentifierRequest.RouteName,
				routeValues: new { ProductId = assembledProduct.Id },
				mapper.Map<AssembledProductDtoRead>(assembledProduct));
		}
	}
}