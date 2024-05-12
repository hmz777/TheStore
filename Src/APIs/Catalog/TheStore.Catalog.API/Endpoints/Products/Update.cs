using Ardalis.ApiEndpoints;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;
using TheStore.ApiCommon.Data.Helpers;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Requests;
using TheStore.Requests.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products
{
	public class Update : EndpointBaseAsync
		.WithRequest<UpdateRequest>
		.WithActionResult
	{
		private readonly IValidator<UpdateRequest> validator;
		private readonly IApiRepository<CatalogDbContext, Product> apiRepository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<Update>();

		public Update(
			IValidator<UpdateRequest> validator,
			IApiRepository<CatalogDbContext, Product> apiRepository,
			IMapper mapper)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
			this.mapper = mapper;
		}

		[HttpPut(UpdateRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[SwaggerOperation(
		   Summary = "Updates a single product",
		   Description = "Updates a single product",
		   OperationId = "Product.Single.Update",
		   Tags = new[] { "Products" })]
		public async override Task<ActionResult> HandleAsync(
			UpdateRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var product = await apiRepository.GetByIdAsync(new ProductId(request.ProductId), cancellationToken);

			if (product == null)
				return NotFound();

			await RepositoryHelpers.PropertyUpdateAsync(request.Product, product, mapper, apiRepository);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Update single product with id: {Id}", request.ProductId);

			return NoContent();
		}
	}
}