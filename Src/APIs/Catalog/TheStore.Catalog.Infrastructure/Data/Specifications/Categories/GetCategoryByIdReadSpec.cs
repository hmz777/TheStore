using Ardalis.Specification;
using TheStore.Catalog.Core.Aggregates.Categories;
using TheStore.Catalog.Core.ValueObjects.Keys;

namespace TheStore.Catalog.Infrastructure.Data.Specifications.Categories
{
	public class GetCategoryByIdReadSpec : Specification<Category>, ISingleResultSpecification
	{
		public GetCategoryByIdReadSpec(CategoryId categoryId)
		{
			Query
				.Where(category => category.Id == categoryId)
				.AsNoTracking();
		}
	}
}