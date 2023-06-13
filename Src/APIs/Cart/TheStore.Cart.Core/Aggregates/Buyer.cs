using TheStore.Cart.Core.ValueObjects.Keys;
using TheStore.SharedKernel.Entities;
using TheStore.SharedKernel.Interfaces;

namespace TheStore.Cart.Core.Aggregates
{
	public class Buyer : BaseEntity<BuyerId>, IAggregateRoot
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		// Ef Core
		private Buyer() { }

		public Buyer(BuyerId buyerId, string firstName, string lastName)
		{
			Id = buyerId;
			FirstName = firstName;
			LastName = lastName;
		}
	}
}