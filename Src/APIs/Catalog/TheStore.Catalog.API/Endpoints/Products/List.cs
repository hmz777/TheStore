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
using TheStore.Catalog.Infrastructure.Helpers;
using TheStore.Requests;
using TheStore.Requests.Models.Products;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products
{
    public class List(
        IValidator<ListRequest> validator,
        IReadApiRepository<CatalogDbContext, Product> repository,
        IMapper mapper) : EndpointBaseAsync
        .WithRequest<ListRequest>
        .WithActionResult<Result<ProductsPaginatedResult>>
    {
        private readonly Serilog.ILogger log = Log.ForContext<List>();

        [HttpGet(ListRequest.RouteTemplate)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(
            Summary = "Lists single products",
            Description = "Lists single products with pagination using skip and take",
            OperationId = "Product.Single.List",
            Tags = ["Products"])]
        public async override Task<ActionResult<Result<ProductsPaginatedResult>>> HandleAsync(
            [FromQuery] ListRequest request,
            CancellationToken cancellationToken = default)
        {
            var validation = await validator.ValidateAsync(request, cancellationToken);
            if (validation.IsValid == false)
                return BadRequest(Result.Failure(validation.AsErrors(), ValidationMessages.InvalidQuery));

            var spec = new ListProductsPaginationCatalogDefaultOrderReadSpec(request.Take, request.Page);

            var products = (await repository
                .ListAsync(spec, cancellationToken))
                .Map<Product, ProductCatalogDtoRead>(mapper);

            var productscount = await repository.CountAsync(spec, cancellationToken);

            using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
                log.Information("List products with Page: {Page} and Take: {Take}", request.Page, request.Take, request.CorrelationId);

            return Result.Success(new ProductsPaginatedResult
            {
                Products = products ?? [],
                Count = productscount,
                PageNumber = request.Page
            });
        }
    }
}