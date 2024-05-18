using Ardalis.Specification;
using TheStore.Catalog.Core.Aggregates.Products;

namespace TheStore.Catalog.Infrastructure.Data.Specifications.Products
{
	public class ListProductsPaginationCatalogDefaultOrderReadSpec : Specification<Product>
	{
		public ListProductsPaginationCatalogDefaultOrderReadSpec(int take, int page)
		{
			Query
				.Include(p => p.Variants.Where(v => v.Options.Published))
				.OrderBy(p => p.Name)
				.Where(p => p.Published)
				.Skip((page - 1) * take)
				.Take(take)
				.AsNoTracking();

			// Cache if it's the first page
			if (page == 1)
				Query.EnableCache(nameof(ListProductsPaginationCatalogDefaultOrderReadSpec), 1);
		}
	}
}