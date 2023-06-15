using TheStore.Web.Domain.Orders;

namespace TheStore.Web.Domain.Products
{
	public class Product : BaseEntity
	{
		public ICollection<Order> Orders { get; set; }
	}
}
