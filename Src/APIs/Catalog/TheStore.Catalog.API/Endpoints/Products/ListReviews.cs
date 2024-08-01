using Ardalis.ApiEndpoints;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.AutoMapper;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Catalog.Infrastructure.Data.Specifications.Products;
using TheStore.Catalog.Infrastructure.Helpers;
using TheStore.Requests.Models.Products;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products
{
    public class ListReviews(
        IValidator<ListReviewsRequest> validator,
        IReadApiRepository<CatalogDbContext, ProductReview> reviewRepository,
        IReadApiRepository<CatalogDbContext, Product> productRepository,
        IMapper mapper) : EndpointBaseAsync
        .WithRequest<ListReviewsRequest>
        .WithActionResult<Result<ProductReviewsPaginatedResult>>
    {
        [HttpGet(ListReviewsRequest.RouteTemplate)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(
            Summary = "Lists product reviews",
            Description = "Lists product reviews with pagination using skip and take",
            OperationId = "Product.Single.Reviews.List",
            Tags = ["Products"])]
        public async override Task<ActionResult<Result<ProductReviewsPaginatedResult>>> HandleAsync(
            ListReviewsRequest request,
            CancellationToken cancellationToken = default)
        {
            var validation = await validator.ValidateAsync(request, cancellationToken);
            if (validation.IsValid == false)
                return BadRequest(Result.Failure(validation.AsErrors(), ValidationMessages.InvalidQuery));

            var product = await productRepository
                .FirstOrDefaultAsync(new CheckProductExistsByIdentifierReadSpec(request.Identifier), cancellationToken);

            if (product == null)
                return NotFound(Result.Failure(ValidationMessages.ProductNotFound));

            var spec = new ListProductReviewsWithPaginationReadSpec(product.Id, request.Page, request.Take);

            var reviews = (await reviewRepository.ListAsync(spec, cancellationToken))
                        .Map<ProductReview, ProductReviewDto>(mapper);

            var reviewsTotalCount = await reviewRepository.CountAsync(spec, cancellationToken);

            return Result.Success(new ProductReviewsPaginatedResult
            {
                Reviews = reviews ?? [],
                Count = reviewsTotalCount,
                PageNumber = request.Page
            });
        }
    }
}