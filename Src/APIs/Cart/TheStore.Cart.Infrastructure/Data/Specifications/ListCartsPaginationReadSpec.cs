using Ardalis.Specification;

namespace TheStore.Cart.Infrastructure.Data.Specifications
{
	public class ListCartsPaginationReadSpec : Specification<Core.Aggregates.Cart>
	{
        public ListCartsPaginationReadSpec(int take, int page)
		{
			Query
				.Skip((page - 1) * take)
				.Take(take)
				.AsNoTracking();

			// Cache if it's the first page
			if (page == 1)
				Query.EnableCache(nameof(ListCartsPaginationReadSpec), 1);
		}
    }
}