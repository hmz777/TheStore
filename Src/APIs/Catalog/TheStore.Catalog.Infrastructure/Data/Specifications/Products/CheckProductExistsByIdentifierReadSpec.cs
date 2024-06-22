using Ardalis.Specification;
using TheStore.Catalog.Core.Aggregates.Products;

namespace TheStore.Catalog.Infrastructure.Data.Specifications.Products
{
	public class CheckProductExistsByIdentifierReadSpec : Specification<Product>
	{
		public CheckProductExistsByIdentifierReadSpec(string identifier)
		{
			Query
				.Where(product => product.Identifier == identifier)
				.AsNoTracking();
		}
	}
}