using AutoFixture;
using TheStore.Catalog.Core.Aggregates.Categories;
using TheStore.Catalog.Core.ValueObjects.Keys;

namespace TheStore.Endpoints.UnitTests.AutoData.Endpoints
{
	public class CategoryCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Register(() =>
			{
				var rand = new Random();
				var category = new Category(rand.Next(), fixture.Create<string>(), false);
				category.Id = new CategoryId(1);

				return category;
			});
		}
	}
}
