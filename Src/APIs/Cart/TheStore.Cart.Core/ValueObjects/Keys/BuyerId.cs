using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Cart.Core.ValueObjects.Keys
{
	public class BuyerId : EntityId<Guid>
	{
		public BuyerId(Guid id) : base(id) { }
	}
}
