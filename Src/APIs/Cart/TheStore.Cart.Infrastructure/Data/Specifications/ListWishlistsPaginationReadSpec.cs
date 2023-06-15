using Ardalis.Specification;
using TheStore.Cart.Core.Aggregates;

namespace TheStore.Cart.Infrastructure.Data.Specifications
{
	public class ListWishlistsPaginationReadSpec : Specification<Wishlist>
	{
		public ListWishlistsPaginationReadSpec(int take, int page)
		{
			Query
				.Skip((page - 1) * take)
				.Take(take)
				.AsNoTracking();

			// Cache if it's the first page
			if (page == 1)
				Query.EnableCache(nameof(ListWishlistsPaginationReadSpec), 1);
		}
	}
}