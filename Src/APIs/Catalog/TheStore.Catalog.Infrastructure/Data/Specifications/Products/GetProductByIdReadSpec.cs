using Ardalis.Specification;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects.Keys;

namespace TheStore.Catalog.Infrastructure.Data.Specifications.Products
{
	public class GetProductByIdReadSpec : Specification<Product>,
		ISingleResultSpecification<Product>
	{
		public GetProductByIdReadSpec(ProductId productId)
		{
			Query.Where(product => product.Id == productId)
				.AsNoTracking();
		}
	}
}