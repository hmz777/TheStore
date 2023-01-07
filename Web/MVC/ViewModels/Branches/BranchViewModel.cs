using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStore.Web.ViewModels.Branches
{
	public class BranchViewModel
	{
		public string Name { get; set; }
		public string Image { get; set; }
		public string Description { get; set; }
		public string Address { get; set; }

		public BranchViewModel(string name, string image, string description, string address)
		{
			Name = name;
			Image = image;
			Description = description;
			Address = address;
		}
	}
}
