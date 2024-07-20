using Ardalis.Specification;
using TheStore.Cart.Core.ValueObjects.Keys;

namespace TheStore.Cart.Infrastructure.Data.Specifications
{
	public class GetCartByBuyerIdSpec : Specification<Core.Aggregates.Cart>,
		ISingleResultSpecification<Core.Aggregates.Cart>
	{
		public GetCartByBuyerIdSpec(BuyerId buyerId)
		{
			Query
			  .Where(c => c.BuyerId == buyerId);
		}
	}
}