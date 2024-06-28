using AutoFixture;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.TestHelpers.AutoData.Customizations
{
	public class ProductCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customize(new MultilanguageStringCustomization());
			fixture.Register(() =>
			{
				return new Product(
					new CategoryId(fixture.Create<int>()),
					fixture.Create<string>(),
					fixture.Create<string>(),
					fixture.Create<MultilanguageString>(),
					fixture.Create<MultilanguageString>(),
					false,
					fixture.CreateMany<ProductVariant>().ToList());
			});
		}
	}
}