using Ardalis.Specification;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects.Keys;

namespace TheStore.Catalog.Infrastructure.Data.Specifications.Products
{
	public class CheckProductExistsReadSpec : Specification<Product>
	{
		public CheckProductExistsReadSpec(ProductId id)
		{
			Query
				.Where(product => product.Id == id)
				.AsNoTracking();
		}
	}
}