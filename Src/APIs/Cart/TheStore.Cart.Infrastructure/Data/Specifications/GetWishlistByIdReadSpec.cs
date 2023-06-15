using Ardalis.Specification;
using TheStore.Cart.Core.Aggregates;

namespace TheStore.Cart.Infrastructure.Data.Specifications
{
	public class GetWishlistByIdReadSpec : Specification<Wishlist>,
		ISingleResultSpecification<Wishlist>
	{
		public GetWishlistByIdReadSpec(Guid wishlistId)
		{
			Query.Where(c => c.Id == wishlistId)
				 .AsNoTracking();
		}
	}
}