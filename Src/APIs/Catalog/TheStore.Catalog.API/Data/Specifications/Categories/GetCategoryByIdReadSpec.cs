using Ardalis.Specification;
using TheStore.Catalog.API.Domain.Categories;
using TheStore.Catalog.Core.ValueObjects.Keys;

namespace TheStore.Catalog.API.Data.Specifications.Categories
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