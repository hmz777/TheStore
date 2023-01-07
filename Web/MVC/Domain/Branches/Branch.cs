using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStore.Web.Domain.Orders;

namespace TheStore.Web.Domain.Branches
{
	public class Branch : BaseEntity
	{
		public ICollection<Order> Orders { get; set; }
	}
}
