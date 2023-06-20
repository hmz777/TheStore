using Ardalis.Specification;
using TheStore.Catalog.Core.Aggregates.Categories;
using TheStore.Catalog.Core.ValueObjects.Keys;

namespace TheStore.Catalog.Infrastructure.Data.Specifications.Categories
{
	public class CheckCategoryExistsReadSpec : Specification<Category>
	{
		public CheckCategoryExistsReadSpec(CategoryId id)
		{
			Query
				.Where(category => category.Id == id)
				.AsNoTracking();
		}
	}
}