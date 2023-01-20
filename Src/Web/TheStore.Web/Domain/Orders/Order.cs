using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStore.Web.Domain;
using TheStore.Web.Domain.Branches;
using TheStore.Web.Domain.Products;

namespace TheStore.Web.Domain.Orders
{
	public class Order : BaseEntity
	{
		public string FullName { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		public ICollection<Product> Products { get; set; }
		public Branch Branch { get; set; }
	}
}
