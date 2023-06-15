using Ardalis.Specification;

namespace TheStore.Cart.Infrastructure.Data.Specifications
{
	public class GetCartByIdReadSpec : Specification<Core.Aggregates.Cart>,
		ISingleResultSpecification<Core.Aggregates.Cart>
	{
		public GetCartByIdReadSpec(Guid cartId)
		{
			Query.Where(c => c.Id == cartId)
				 .AsNoTracking();
		}
	}
}