using AutoFixture;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Domain.UnitTests.AutoData.Customizations;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Catalog.Endpoints.UnitTests.AutoData.Endpoints
{
	public class ProductCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customize(new MultilanguageStringCustomization());
			fixture.Register(() =>
			{
				var singleProduct = new Product(
					new CategoryId(fixture.Create<int>()),
					fixture.Create<string>(),
					fixture.Create<MultilanguageString>(),
					fixture.Create<MultilanguageString>(),
					false,
					fixture.CreateMany<ProductVariant>().ToList());

				return singleProduct;
			});
		}
	}
}