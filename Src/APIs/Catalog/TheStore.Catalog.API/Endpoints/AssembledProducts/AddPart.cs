using Ardalis.ApiEndpoints;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.AssembledProducts
{
	public class AddPart : EndpointBaseAsync
		.WithRequest<AddPartRequest>
		.WithActionResult
	{
		private readonly IValidator<AddPartRequest> validator;
		private readonly IApiRepository<CatalogDbContext, AssembledProduct> assembledProductRepository;
		private readonly IApiRepository<CatalogDbContext, SingleProduct> singleProductRepository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<AddPart>();

		public AddPart(
			IValidator<AddPartRequest> validator,
			IApiRepository<CatalogDbContext, AssembledProduct> assembledProductRepository,
			IApiRepository<CatalogDbContext, SingleProduct> singleProductRepository,
			IMapper mapper)
		{
			this.validator = validator;
			this.assembledProductRepository = assembledProductRepository;
			this.singleProductRepository = singleProductRepository;
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
		   Tags = new[] { "Products" })]
		public async override Task<ActionResult> HandleAsync(
			AddPartRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var partId = new ProductId(request.PartId);

			var part = await singleProductRepository
				.GetByIdAsync(partId, cancellationToken);

			if (part == null)
				return NotFound("Part not found");

			var assembledProduct = await assembledProductRepository
				.GetByIdAsync(new AssembledProductId(request.ProductId), cancellationToken);

			if (assembledProduct == null)
				return NotFound("Assembled product not found");

			if (assembledProduct.Parts.Contains(partId))
				return Conflict("Part already exists");

			assembledProduct.AddPart(partId);

			await assembledProductRepository.SaveChangesAsync(cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Add part with id: {PartId} to assembled product with id: {Id}", request.PartId, request.ProductId);

			return CreatedAtRoute(
			GetByIdRequest.RouteName,
				routeValues: new { ProductId = assembledProduct.Id.Id },
				mapper.Map<AssembledProductDto>(assembledProduct));
		}
	}
}