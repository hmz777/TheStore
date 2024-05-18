using Ardalis.ApiEndpoints;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.AutoMapper;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Catalog.Infrastructure.Data.Specifications.Products;
using TheStore.Requests.Models.Products;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Products
{
	public class ListReviews : EndpointBaseAsync
		.WithRequest<ListReviewsRequest>
		.WithActionResult<ProductReviewsPaginatedResult>
	{
		private readonly IValidator<ListReviewsRequest> validator;
		private readonly IReadApiRepository<CatalogDbContext, Product> repository;
		private readonly IMapper mapper;

		public ListReviews(
			IValidator<ListReviewsRequest> validator,
			IReadApiRepository<CatalogDbContext, Product> repository,
			IMapper mapper)
		{
			this.validator = validator;
			this.repository = repository;
			this.mapper = mapper;
		}

		[HttpGet(ListReviewsRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[SwaggerOperation(
			Summary = "Lists product reviews",
			Description = "Lists product reviews with pagination using skip and take",
			OperationId = "Product.Single.Reviews.List",
			Tags = ["Products"])]
		public async override Task<ActionResult<ProductReviewsPaginatedResult>> HandleAsync(
			ListReviewsRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var spec = new ListProductReviewsWithPaginationReadSpec(new ProductId(request.ProductId), request.Page, request.Take);

			var reviews = (await repository.ListAsync(spec, cancellationToken))
						.Map<ProductReview, ProductReviewDto>(mapper);

			var reviewsTotalCount = await repository.CountAsync(spec, cancellationToken);

			return new ProductReviewsPaginatedResult
			{
				Reviews = reviews ?? [],
				Count = reviewsTotalCount,
				PageNumber = request.Page
			};
		}
	}
}