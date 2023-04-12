using Ardalis.Specification;
using TheStore.Catalog.Core.Aggregates.Branches;

namespace TheStore.Catalog.Infrastructure.Data.Specifications.Branches
{
	public class ListBranchesPaginationDefaultOrderReadSpec : Specification<Branch>
	{
		public ListBranchesPaginationDefaultOrderReadSpec(int take, int page)
		{
			Query
				.OrderBy(c => c.Name)
				.Skip((page - 1) * take)
				.Take(take)
				.AsNoTracking();

			// Cache if it's the first page
			if (page == 1)
				Query.EnableCache(nameof(ListBranchesPaginationDefaultOrderReadSpec), 1);
		}
	}
}
