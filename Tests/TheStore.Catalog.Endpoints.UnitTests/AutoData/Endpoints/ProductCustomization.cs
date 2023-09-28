using AutoFixture;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Core.ValueObjects.Products;

namespace TheStore.Catalog.Endpoints.UnitTests.AutoData.Endpoints
{
	public class ProductCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Register(() =>
			{
				var singleProduct = new Product(
					new CategoryId(fixture.Create<int>()),
					fixture.Create<string>(),
					fixture.Create<string>(),
					fixture.Create<string>(),
					fixture.Create<string>(),
					fixture.Create<Money>(),
					fixture.CreateMany<ProductColor>().ToList());

				return singleProduct;
			});
		}
	}
}