using AutoFixture;
using TheStore.Catalog.Core.Aggregates.Categories;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.TestHelpers.AutoData.Customizations
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
