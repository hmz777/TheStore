using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStore.Catalog.API.Domain.Categories;

namespace TheStore.Catalog.API.Data.Specifications.Categories
{
	public class ListCategoriesPaginationDefaultOrderReadSpec : Specification<Category>
	{
		public ListCategoriesPaginationDefaultOrderReadSpec(int take, int page)
		{
			Query
				.OrderBy(c => c.Order)
				.Skip((page - 1) * take)
				.Take(take)
				.AsNoTracking();

			// Cache if it's the first page
			if (page == 1)
				Query.EnableCache(nameof(ListCategoriesPaginationDefaultOrderReadSpec), 1);
		}
	}
}