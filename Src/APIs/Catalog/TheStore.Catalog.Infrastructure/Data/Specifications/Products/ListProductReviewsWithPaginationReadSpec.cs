using Ardalis.Specification;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects.Keys;

namespace TheStore.Catalog.Infrastructure.Data.Specifications.Products
{
	public class ListProductReviewsWithPaginationReadSpec : Specification<Product, ProductReview>
	{
		public ListProductReviewsWithPaginationReadSpec(ProductId productId, int page, int take)
		{
			Query
				.SelectMany(p => p.Reviews.Where(p => p.Published).Take(take).Skip((page - 1) * take))
				.Where(p => p.Id == productId)
				.AsNoTracking();
		}
	}
}