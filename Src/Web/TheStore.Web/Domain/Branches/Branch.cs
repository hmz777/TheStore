using TheStore.Web.Domain.Orders;

namespace TheStore.Web.Domain.Branches
{
	public class Branch : BaseEntity
	{
		public ICollection<Order> Orders { get; set; }
	}
}
