using Ardalis.Specification;
using TheStore.Catalog.Core.Aggregates.Products;

namespace TheStore.Catalog.Infrastructure.Data.Specifications.Products
{
	public class GetProductByIdentifierReadSpec : Specification<Product>,
		ISingleResultSpecification<Product>
	{
		public GetProductByIdentifierReadSpec(string identifier)
		{
			Query.Where(product => product.Identifier == identifier)
				 .Include(p => p.Variants)
				 .ThenInclude(v => v.Sepcifications)
				 .AsNoTracking();
		}
	}
}