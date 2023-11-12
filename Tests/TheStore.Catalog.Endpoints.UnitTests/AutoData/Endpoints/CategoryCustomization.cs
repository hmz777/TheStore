using AutoFixture;
using TheStore.Catalog.Core.Aggregates.Categories;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Domain.UnitTests.AutoData.Customizations;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Catalog.Endpoints.UnitTests.AutoData.Endpoints
{
	public class CategoryCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customize(new MultilanguageStringCustomization());
			fixture.Register(() =>
			{
				var rand = new Random();
				var category = new Category(rand.Next(), fixture.Create<MultilanguageString>(), false)
				{
					Id = new CategoryId(1)
				};

				return category;
			});
		}
	}
}
