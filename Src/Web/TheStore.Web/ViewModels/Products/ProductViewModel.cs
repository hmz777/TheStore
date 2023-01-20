using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStore.Web.ViewModels.Products
{
	public class ProductViewModel
	{
		public string Name { get; set; }
		public string Color { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public string Image { get; set; }
	}
}
