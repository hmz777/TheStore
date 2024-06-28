using Ardalis.Specification;
using TheStore.Catalog.Core.Aggregates.Products;

namespace TheStore.Catalog.Infrastructure.Data.Specifications.Products
{
	public class GetProductByIdentifierSpec : Specification<Product>,
		ISingleResultSpecification<Product>
	{
		public GetProductByIdentifierSpec(string identifier, bool readOnly = false)
		{
			Query.Where(product => product.Identifier == identifier)
				 .Include(p => p.Variants)
				 .ThenInclude(v => v.Sepcifications);

			if (readOnly)
			{
				Query
					.AsNoTracking();
			}
		}
	}
}