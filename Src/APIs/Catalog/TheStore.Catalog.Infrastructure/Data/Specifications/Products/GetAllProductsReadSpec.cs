using Ardalis.Specification;
using TheStore.Catalog.Core.Aggregates.Products;

namespace TheStore.Catalog.Infrastructure.Data.Specifications.Products
{
	public class GetAllProductsReadSpec : Specification<Product>
	{
		public GetAllProductsReadSpec()
		{
			Query
				.AsNoTracking();
		}
	}
}