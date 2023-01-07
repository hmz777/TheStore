using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStore.Catalog.API.Domain.Categories;

namespace TheStore.Catalog.API.Data.Specifications.Categories
{
	public class GetCategoryByIdReadSpec : Specification<Category>, ISingleResultSpecification
	{
		public GetCategoryByIdReadSpec(int categoryId)
		{
			Query
				.Where(category => category.Id == categoryId)
				.AsNoTracking();
		}
	}
}