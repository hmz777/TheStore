using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStore.Web.Domain.Orders;

namespace TheStore.Web.Domain.Products
{
	public class Product : BaseEntity
	{
		public ICollection<Order> Orders { get; set; }
	}
}
