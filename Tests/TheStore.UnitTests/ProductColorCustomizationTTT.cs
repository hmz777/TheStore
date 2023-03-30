using AutoFixture;
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Products;

namespace TheStore.Domain.Tests
{
	public class ProductColorCustomizationTTT : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Register(() =>
			{
				return new ProductColor(fixture.Create<string>(), new List<Image>());
			});
		}
	}
}