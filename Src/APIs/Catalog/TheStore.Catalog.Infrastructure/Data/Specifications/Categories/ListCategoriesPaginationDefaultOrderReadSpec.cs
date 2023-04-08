using Ardalis.Specification;
using TheStore.Catalog.Core.Aggregates.Categories;

namespace TheStore.Catalog.Infrastructure.Data.Specifications.Categories
{
	public class ListCategoriesPaginationDefaultOrderReadSpec : Specification<Category>
	{
		public ListCategoriesPaginationDefaultOrderReadSpec(int take, int page)
		{
			Query
				.OrderBy(c => c.Order)
				.Skip((page - 1) * take)
				.Take(take)
				.AsNoTracking();

			// Cache if it's the first page
			if (page == 1)
				Query.EnableCache(nameof(ListCategoriesPaginationDefaultOrderReadSpec), 1);
		}
	}
}