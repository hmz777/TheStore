using Ardalis.Specification;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects.Keys;

namespace TheStore.Catalog.Infrastructure.Data.Specifications.Products
{
	public class ListProductReviewsWithPaginationReadSpec : Specification<ProductReview>
	{
		public ListProductReviewsWithPaginationReadSpec(ProductId productId, int page, int take)
		{
			Query
				.Where(p => p.ProductId == productId)
				.Take(take).Skip((page - 1) * take)
				.AsNoTracking();
		}
	}
}