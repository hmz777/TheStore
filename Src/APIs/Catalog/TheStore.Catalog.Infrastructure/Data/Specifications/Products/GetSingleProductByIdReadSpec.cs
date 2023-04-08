using Ardalis.Specification;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects.Keys;

namespace TheStore.Catalog.Infrastructure.Data.Specifications.Products
{
	public class GetSingleProductByIdReadSpec : Specification<SingleProduct>, ISingleResultSpecification
	{
		public GetSingleProductByIdReadSpec(ProductId productId)
		{
			Query.Where(product => product.Id == productId)
				.AsNoTracking();
		}
	}
}