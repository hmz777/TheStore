using AutoFixture;
using TheStore.Catalog.Core.ValueObjects;

namespace TheStore.Catalog.Domain.UnitTests.AutoData.Customizations
{
	public class MoneyCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Register(() =>
			{
				var rand = new Random();

				var money = new Money(rand.Next(0, 5000), Currency.Usd);
				return money;
			});
		}
	}
}
