using Ardalis.Specification;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects.Keys;

namespace TheStore.Catalog.Infrastructure.Data.Specifications.Products
{
	public class CheckAssembledProductExistsReadSpec : Specification<AssembledProduct>
	{
		public CheckAssembledProductExistsReadSpec(ProductId id)
		{
			Query
				.Where(product => product.Id == id)
				.AsNoTracking();
		}
	}
}