using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStore.Catalog.API.Domain.Images
{
	public enum ImageType : int
	{
		Category = 1,
		Subcategory = 2,
		Product = 3,
		Branch = 4,
		Other = 5
	}
}
