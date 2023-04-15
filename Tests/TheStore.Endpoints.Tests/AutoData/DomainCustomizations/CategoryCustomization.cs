using AutoFixture;
using TheStore.Catalog.Core.Aggregates.Categories;

namespace TheStore.Endpoints.Tests.AutoData.DomainCustomizations
{
	public class CategoryCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Register(() =>
			{
				var rand = new Random();
				var category = new Category(rand.Next(), fixture.Create<string>(), false);

				return category;
			});
		}
	}
}
