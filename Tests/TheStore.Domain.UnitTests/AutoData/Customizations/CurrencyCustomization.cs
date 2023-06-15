using AutoFixture;
using TheStore.Catalog.Core.ValueObjects;

namespace TheStore.Catalog.Domain.UnitTests.AutoData.Customizations
{
	public class CurrencyCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Register(() => Currency.Usd);
		}
	}
}