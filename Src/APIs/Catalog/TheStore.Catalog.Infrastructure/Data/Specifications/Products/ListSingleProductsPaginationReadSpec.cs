using Ardalis.Specification;
using TheStore.Catalog.Core.Aggregates.Products;

namespace TheStore.Catalog.Infrastructure.Data.Specifications.Products
{
	public class ListSingleProductsPaginationReadSpec : Specification<SingleProduct>
	{
		public ListSingleProductsPaginationReadSpec(int take, int page)
		{
			Query
				.OrderBy(x => x.Id)
				.Skip((page - 1) * take)
				.Take(take)
				.AsNoTracking();

			// Cache if it's the first page
			if (page == 1)
				Query.EnableCache(nameof(ListSingleProductsPaginationReadSpec), 1);
		}
	}
}