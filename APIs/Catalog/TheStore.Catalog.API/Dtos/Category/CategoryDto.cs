using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStore.Catalog.API.Dtos.Category
{
	public class CategoryDto
	{
		public int Order { get; set; }
		public string Name { get; set; }

		public CategoryDto(int order, string name)
		{
			Order = order;
			Name = name;
		}
	}
}
