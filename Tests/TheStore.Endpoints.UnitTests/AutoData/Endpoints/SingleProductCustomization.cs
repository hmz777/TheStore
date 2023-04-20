using AutoFixture;
using TheStore.Catalog.Core.Aggregates.Products;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.Catalog.Core.ValueObjects.Products;

namespace TheStore.Endpoints.UnitTests.AutoData.Endpoints
{
	public class SingleProductCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Register(() =>
			{
				var singleProduct = new SingleProduct(
					new CategoryId(fixture.Create<int>()),
					fixture.Create<string>(),
					fixture.Create<string>(),
					fixture.Create<string>(),
					fixture.Create<string>(),
					fixture.Create<Money>(),
					fixture.Create<InventoryRecord>(),
					fixture.CreateMany<ProductColor>().ToList());

				return singleProduct;
			});
		}
	}
}
